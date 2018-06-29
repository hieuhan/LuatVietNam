$( document ).ready(function() {
$('#dropbox .dropcontent .dropsub .tieloto:odd').addClass('odd');
$('#dropbox .dropcontent .dropsub .tieloto:even').addClass('even');
$('#dropbox > .dropcontent > .tieloto > .tie-drop > .moredrop').click(function() {
  $('#dropbox .tieloto').removeClass('active');
  $(this).closest('tieloto').addClass('active'); 
  //var checkElement = $(this).next();
  var checkElement = $(this).closest('.tieloto').find('.dropsub').first();
  if(checkElement.length){
  if((checkElement.is('.dropsub')) && (checkElement.is(':visible'))) {
    $(this).closest('.tieloto').removeClass('active');
    checkElement.slideUp('normal');
  }
  if((checkElement.is('.dropsub')) && (!checkElement.is(':visible'))) {
    $('#dropbox .dropcontent .dropsub:visible').slideUp('normal');
    checkElement.slideDown('normal');
  }
  }
  if($(this).closest('.tieloto').find('.dropsub').children().length == 0) {
    return true;
  } else {
    return false;   
  }     
});
});