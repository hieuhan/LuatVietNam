﻿/**
 * @license Copyright (c) 2003-2017, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

( function() {
	function addCombo( editor, comboName, styleType, lang, entries, defaultLabel, styleDefinition, order ) {
		var config = editor.config,
			style = new CKEDITOR.style( styleDefinition );

		// Gets the list of fonts from the settings.
		var names = entries.split( ';' ),
			values = [];

		// Create style objects for all fonts.
		var styles = {};
		for ( var i = 0; i < names.length; i++ ) {
			var parts = names[ i ];

			if ( parts ) {
				parts = parts.split( '/' );

				var vars = {},
					name = names[ i ] = parts[ 0 ];

				vars[ styleType ] = values[ i ] = parts[ 1 ] || name;

				styles[ name ] = new CKEDITOR.style( styleDefinition, vars );
				styles[ name ]._.definition.name = name;
			} else {
				names.splice( i--, 1 );
			}
		}
		console.log('editor.ui.addRichCombo');
		editor.ui.addRichCombo( comboName, {
			label: lang.label,
			title: lang.panelTitle,
			toolbar: 'listTable,' + order,
			contentTransformations: [
				[
					{
						element: 'font',
						check: 'span',
						left: function( element ) {
							return !!element.attributes.class;
						},
						right: function( element ) {
							var sizes = [
								'', // Non-existent size "0"
								'x-small',
								'small',
								'medium',
								'large',
								'x-large',
								'xx-large',
								'48px' // Closest value to what size="7" might mean.
							];

							element.name = 'span';

						    if ( element.attributes.size ) {
								element.styles[ 'font-size' ] = sizes[ element.attributes.size ];
								delete element.attributes.size;
							}
						}
					}
				]
			],
			panel: {
				css: [ CKEDITOR.skin.getPath( 'editor' ) ].concat( config.contentsCss ),
				multiSelect: false,
				attributes: { 'aria-label': lang.panelTitle }
			},

			init: function() {
				this.startGroup( lang.panelTitle );

				for ( var i = 0; i < names.length; i++ ) {
					var name = names[ i ];

					// Add the tag entry to the panel list.
					this.add( name, styles[ name ].buildPreview(), name );
				}
			},

			onClick: function( value ) {
				editor.focus();
				editor.fire( 'saveSnapshot' );

				var previousValue = this.getValue(),
					style = styles[ value ];
				
				// When applying one style over another, first remove the previous one (#12403).
				// NOTE: This is only a temporary fix. It will be moved to the styles system (#12687).
				if ( previousValue && value != previousValue ) {
					var previousStyle = styles[ previousValue ],
						range = editor.getSelection().getRanges()[ 0 ];
					
					// If the range is collapsed we can't simply use the editor.removeStyle method
					// because it will remove the entire element and we want to split it instead.
					if ( range.collapsed ) {
						var path = editor.elementPath(),
							// Find the style element.
							matching = path.contains( function( el ) {
								return previousStyle.checkElementRemovable( el );
							} );

						if ( matching ) {
							var startBoundary = range.checkBoundaryOfElement( matching, CKEDITOR.START ),
								endBoundary = range.checkBoundaryOfElement( matching, CKEDITOR.END ),
								node, bm;

							// If we are at both boundaries it means that the element is empty.
							// Remove it but in a way that we won't lose other empty inline elements inside it.
							// Example: <p>x<span style="font-size:48px"><em>[]</em></span>x</p>
							// Result: <p>x<em>[]</em>x</p>
							if ( startBoundary && endBoundary ) {
								bm = range.createBookmark();
								// Replace the element with its children (TODO element.replaceWithChildren).
								while ( ( node = matching.getFirst() ) ) {
									node.insertBefore( matching );
								}
								matching.remove();
								range.moveToBookmark( bm );

							// If we are at the boundary of the style element, move out and copy nested styles/elements.
							} else if ( startBoundary || endBoundary ) {
								range.moveToPosition( matching, startBoundary ? CKEDITOR.POSITION_BEFORE_START : CKEDITOR.POSITION_AFTER_END );
								cloneSubtreeIntoRange( range, path.elements.slice(), matching );
							} else {
								// Split the element and clone the elements that were in the path
								// (between the startContainer and the matching element)
								// into the new place.
								range.splitElement( matching );
								range.moveToPosition( matching, CKEDITOR.POSITION_AFTER_END );
								cloneSubtreeIntoRange( range, path.elements.slice(), matching );
							}

							editor.getSelection().selectRanges( [ range ] );
						}
					} else {
						editor.removeStyle( previousStyle );
					}
				}

				editor[ previousValue == value ? 'removeStyle' : 'applyStyle' ]( style );

				editor.fire( 'saveSnapshot' );
			},

			onRender: function() {
				editor.on( 'selectionChange', function( ev ) {
					var currentValue = this.getValue();

					var elementPath = ev.data.path,
						elements = elementPath.elements;

					// For each element into the elements path.
					for ( var i = 0, element; i < elements.length; i++ ) {
						element = elements[ i ];

						// Check if the element is removable by any of
						// the styles.
						for ( var value in styles ) {
							if ( styles[ value ].checkElementMatch( element, true, editor ) ) {
								if ( value != currentValue )
									this.setValue( value );
								return;
							}
						}
					}

					// If no styles match, just empty it.
					this.setValue( '', defaultLabel );
				}, this );
			},

			refresh: function() {
				if ( !editor.activeFilter.check( style ) )
					this.setState( CKEDITOR.TRISTATE_DISABLED );
			}
		} );
	}

	// Clones the subtree between subtreeStart (exclusive) and the
	// leaf (inclusive) and inserts it into the range.
	//
	// @param range
	// @param {CKEDITOR.dom.element[]} elements Elements path in the standard order: leaf -> root.
	// @param {CKEDITOR.dom.element/null} substreeStart The start of the subtree.
	// If null, then the leaf belongs to the subtree.
	function cloneSubtreeIntoRange( range, elements, subtreeStart ) {
		var current = elements.pop();
		if ( !current ) {
			return;
		}
		// Rewind the elements array up to the subtreeStart and then start the real cloning.
		if ( subtreeStart ) {
			return cloneSubtreeIntoRange( range, elements, current.equals( subtreeStart ) ? null : subtreeStart );
		}

		var clone = current.clone();
		range.insertNode( clone );
		range.moveToPosition( clone, CKEDITOR.POSITION_AFTER_START );

		cloneSubtreeIntoRange( range, elements );
	}

	CKEDITOR.plugins.add('listtable', {
		requires: 'richcombo',
		// jscs:disable maximumLineLength
		lang: 'en,vi', // %REMOVE_LINE_CORE%
		// jscs:enable maximumLineLength
		init: function( editor ) {
		    var config = editor.config;
		    console.log('ListTable');
		    addCombo(editor, 'ListTable', 'size', editor.lang.listtable.fontSize, config.listSize_sizes, config.listSize_defaultLabel, config.listSize_style, 86);
		}
	} );
} )();

/**
 * The list of fonts names to be displayed in the Font combo in the toolbar.
 * Entries are separated by semi-colons (`';'`), while it's possible to have more
 * than one font for each entry, in the HTML way (separated by comma).
 *
 * A display name may be optionally defined by prefixing the entries with the
 * name and the slash character. For example, `'Arial/Arial, Helvetica, sans-serif'`
 * will be displayed as `'Arial'` in the list, but will be outputted as
 * `'Arial, Helvetica, sans-serif'`.
 *
 *		config.font_names =
 *			'Arial/Arial, Helvetica, sans-serif;' +
 *			'Times New Roman/Times New Roman, Times, serif;' +
 *			'Verdana';
 *
 *		config.font_names = 'Arial;Times New Roman;Verdana';
 *
 * @cfg {String} [font_names=see source]
 * @member CKEDITOR.config
 */

/**
 * The text to be displayed in the Font combo is none of the available values
 * matches the current cursor position or text selection.
 *
 *		// If the default site font is Arial, we may making it more explicit to the end user.
 *		config.font_defaultLabel = 'Arial';
 *
 * @cfg {String} [font_defaultLabel='']
 * @member CKEDITOR.config
 */
CKEDITOR.config.list_defaultLabel = '';

/**
 * The style definition to be used to apply the font in the text.
 *
 *		// This is actually the default value for it.
 *		config.font_style = {
 *			element:		'span',
 *			styles:			{ 'font-family': '#(family)' },
 *			overrides:		[ { element: 'font', attributes: { 'face': null } } ]
 *     };
 *
 * @cfg {Object} [font_style=see example]
 * @member CKEDITOR.config
 */

/**
 * The list of fonts size to be displayed in the Font Size combo in the
 * toolbar. Entries are separated by semi-colons (`';'`).
 *
 * Any kind of "CSS like" size can be used, like `'12px'`, `'2.3em'`, `'130%'`,
 * `'larger'` or `'x-small'`.
 *
 * A display name may be optionally defined by prefixing the entries with the
 * name and the slash character. For example, `'Bigger Font/14px'` will be
 * displayed as `'Bigger Font'` in the list, but will be outputted as `'14px'`.
 *
 *		config.fontSize_sizes = '16/16px;24/24px;48/48px;';
 *
 *		config.fontSize_sizes = '12px;2.3em;130%;larger;x-small';
 *
 *		config.fontSize_sizes = '12 Pixels/12px;Big/2.3em;30 Percent More/130%;Bigger/larger;Very Small/x-small';
 *
 * @cfg {String} [fontSize_sizes=see source]
 * @member CKEDITOR.config
 */
CKEDITOR.config.listSize_sizes = 'Mục lục 1/demuc1;Mục lục 2/demuc2;Mục lục 3/demuc3;Mục lục 4/demuc4;Mục lục 5/demuc5';
CKEDITOR.config.listtable_sizes = 'Mục lục 1/32;Mục lục 2/28;Mục lục 3/26;Mục lục 4/24;Mục lục 5/20';

/**
 * The text to be displayed in the Font Size combo is none of the available
 * values matches the current cursor position or text selection.
 *
 *		// If the default site font size is 12px, we may making it more explicit to the end user.
 *		config.fontSize_defaultLabel = '12px';
 *
 * @cfg {String} [fontSize_defaultLabel='']
 * @member CKEDITOR.config
 */
CKEDITOR.config.listSize_defaultLabel = '';

/**
 * The style definition to be used to apply the font size in the text.
 *
 *		// This is actually the default value for it.
 *		config.fontSize_style = {
 *			element:		'span',
 *			styles:			{ 'font-size': '#(size)' },
 *			overrides:		[ { element: 'font', attributes: { 'size': null } } ]
 *		};
 *
 * @cfg {Object} [fontSize_style=see example]
 * @member CKEDITOR.config
 */
CKEDITOR.config.listSize_style = {
	element: 'span',
	attributes: { 'class': '#(size)' },
	overrides: [ {
		element: 'font', attributes: { 'size': null }
	} ]
};
