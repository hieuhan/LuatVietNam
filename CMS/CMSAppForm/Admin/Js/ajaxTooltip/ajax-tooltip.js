
var offsetxpoint = -60; //Customize x offset of tooltip
var offsetypoint = 20; //Customize y offset of tooltip
var ie = document.all;
var ns6 = document.getElementById && !document.all;
var enabletip = false;
var curGX = 0;
var curGY= 0;
var tipobj; 

function ietruebody()
{
	return (document.compatMode && document.compatMode != "BackCompat")? document.documentElement : document.body;
}

// Ajax Show Tooltip
function ShowTooltip(externalFile,tip_type)
{	
	try
	{
	if((document.readyState == "complete") || (!ie))
	{		
		if(!document.getElementById('Tooltips'))
		{
			try
			{		
				/* Create tooltip content div */
				tipobj = document.createElement('div');  
				tipobj.id = 'Tooltips';		
				tipobj.style.display='block';
				tipobj.style.position = 'absolute';
				document.body.appendChild(tipobj);
			}
			catch(e)
			{	
				return;		
			}
		}		
		if(tip_type == "sms"){
		    tipobj.className = 'ajaxtooltipSMS'; 
		}
		else if (tip_type == "news"){
			tipobj.className = 'ajaxtooltipNews';
		}
		else if (tip_type == "tt"){
			tipobj.className = 'ajaxtooltip';
		}
		 //var domain= document.domain ;
		 //externalFile = "http://" + domain + externalFile;
		 //alert(externalFile);
		ajax_loadContent('Tooltips',externalFile);	
		
		enabletip = true;
	}
	}
	catch(ex)
	{	
		return;		
	}
}
 
// Positioning Tooltip
function PositionTooltip(e)
{
	if (enabletip)
	{
		var curX = (ns6) ? e.pageX : event.clientX+ietruebody().scrollLeft;
		var curY = (ns6) ? e.pageY : event.clientY+ietruebody().scrollTop;
		curGX = (ns6) ? e.pageX : event.clientX+ietruebody().scrollLeft;
		curGY = (ns6) ? e.pageY : event.clientY+ietruebody().scrollTop;
		
		//Find out how close the mouse is to the corner of the window
		var rightedge  = ie&&!window.opera ? ietruebody().clientWidth-event.clientX-offsetxpoint : window.innerWidth-e.clientX-offsetxpoint-20;
		var bottomedge = ie&&!window.opera ? ietruebody().clientHeight-event.clientY-offsetypoint : window.innerHeight-e.clientY-offsetypoint-20;

		var leftedge = (offsetxpoint<0) ? offsetxpoint*(-1) : -1000;

		//SMS - if the horizontal distance isn't enough to accomodate the width of the context menu
		if (rightedge < tipobj.offsetWidth)
		{
			//move the horizontal position of the menu to the left by it's width
			tipobj.style.left = ie ? ietruebody().scrollLeft + event.clientX-tipobj.offsetWidth + "px" : window.pageXOffset + e.clientX - tipobj.offsetWidth + "px";
		}
		else if (curX < leftedge)
		{
			tipobj.style.left = "5px";
		}
		else
		{
			//position the horizontal position of the menu where the mouse is positioned
			tipobj.style.left = curX + offsetxpoint + "px";
		} 

		//SMS - same concept with the vertical position
		if (bottomedge < tipobj.offsetHeight)
		{
			tipobj.style.top = ie ? ietruebody().scrollTop + event.clientY-tipobj.offsetHeight - offsetypoint + "px" : window.pageYOffset + e.clientY - tipobj.offsetHeight - offsetypoint + "px";
		}
		else
		{
			tipobj.style.top = curY + offsetypoint + "px";
		}
		 	
			
		tipobj.style.visibility = "visible"; 
	}
	else{
		curGX = (ns6) ? e.pageX : event.clientX+ietruebody().scrollLeft;
		curGY = (ns6) ? e.pageY : event.clientY+ietruebody().scrollTop;
	}
}

// Hide Tooltip
function HideTooltip()
{
	if ((ns6||ie) && (tipobj))
	{
		enabletip = false;
		
		tipobj.style.visibility = "hidden";
		tipobj.style.left = "-1000px";
		tipobj.style.backgroundColor = '';
		tipobj.style.width = '';
	}
}

document.onmousemove = PositionTooltip;