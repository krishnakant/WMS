var POP_SCREEN_HEIGHT;
var TABLE_DISPLAY_HEIGHT;
var SCREEN_HEIGHT = fnScreenHeight();
if (SCREEN_HEIGHT > 500 && SCREEN_HEIGHT < 700)
	POP_SCREEN_HEIGHT = 600;
else if (SCREEN_HEIGHT > 700 && SCREEN_HEIGHT < 1000)
	POP_SCREEN_HEIGHT = 880;
else
	POP_SCREEN_HEIGHT = 600;

var SCREEN_WIDTH = fnScreenWidth();

//KEY CODES
var ENTER_KEY = 13;

function getEl() {
  var elements = new Array();

  for (var i = 0; i < arguments.length; i++) {
    var element = arguments[i];
    if (typeof element == 'string')
      element = document.getElementById(element);

    if (arguments.length == 1)
      return element;

    elements.push(element);
  }

  return elements;
}

function fnShowStatus(statusMsg){
	window.status='Service Head Quarters';
	return true;
}
document.onmouseover = fnShowStatus;

function fnShowStatusOnMenu(sMsg) {
    //window.status = sMsg ;
    //return true ;
}

// Function sets the page title and document title
function fnSetPageTitle(strPageTitle){
	try{
		getEl("divPageTitle").innerHTML=strPageTitle;
	}catch(e){}
	document.title="Service Head Quarters : " + strPageTitle;
}
/*
* Function display friendlier javascript error
*/
function fnShowJSError(e){
	fnAlert(SECTIONNAME, "",e.description);
}

/**
* Function to get attach event
* Usage : fnAttachEvent(window,'resize',fnBodyWidth,false)
*/
function fnAttachEvent(obj,evt,fnc,useCapture){
		if (!useCapture) useCapture=false;
		if (obj.addEventListener){
			obj.addEventListener(evt,fnc,useCapture);
			return true;
		} else if (obj.attachEvent) { 
			 return obj.attachEvent("on"+evt,fnc);		 
		}else{
			alert("Event Not Supported");
		}
	} 

/**
* Function called on window resize
* set the body width according to client screen width
*/
function fnBodyWidth() {
	var iBodyWidth=fnScreenWidth();
	if (iBodyWidth < 780){ // case if popup window width = 795
		document.getElementsByTagName("body").item(0).style.width='765px';//765px
	}else if (iBodyWidth < 995){
		document.getElementsByTagName("body").item(0).style.width='99%';
	}
	if (WINDOW_TYPE=="modal"){
		document.getElementsByTagName("body").item(0).style.width='96.5%';//750px
	}
	// Below is the case if popup window width = 900
	if (iBodyWidth > 780 && iBodyWidth < 900 && WINDOW_TYPE=="pop"){
		document.getElementsByTagName("body").item(0).style.width='872px';//872px
	}	
}

/**
* Function to get the user screen Height
*/
function fnScreenHeight(){
    winHeight = 0;
    if( typeof( window.innerWidth ) == 'number' ){
	   winHeight = window.innerHeight;
    }else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight )){
		winHeight = document.documentElement.clientHeight;
	}else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ){
		winHeight = document.body.clientHeight;
	}else{
		winHeight = screen.availHeight;
	}
    return winHeight;
}

/**
* Function to get the user screen Width
*/
function fnScreenWidth(){
    var winWidth = 0;
	if( typeof( window.innerWidth ) == 'number' ){
		winWidth = window.innerWidth;
	}else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight )){
		winWidth = document.documentElement.clientWidth;
	}else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ){
		winWidth = document.body.clientWidth;
	}else{
		winWidth = screen.availWidth;
	}
    return winWidth;
}

/**
* Hover on form buttons
*/
function fnChangeButtonHover(obj){
    obj.className='cssButtonHover';
}
/**
* Normal/Reset state on form buttons
*/
function fnChangeButtonNormal(obj){
    obj.className='cssButton';
}

/**
* Close Window
*/
function fnCloseWin(){
	window.close();	
}

/**
* Going Back
*/
function fnGoBack(){
	history.go(-1);
}

/**
* Redirect
*/
function fnRedirect(sUrl){
	location.href=sUrl;
}

/**
* Modal window
*/
function fnShowModalWindow(url,paramArr,wWidth,wHeight){
	// Variables used for centering the modal window
	var iTop = parseInt((screen.height-wHeight)/2);
	var iLeft = parseInt((screen.width-wWidth)/2);
	
	var commonFeature = "dialogTop: "+ iTop +"px; dialogLeft: "+ iLeft +"px; edge: Raised; center: Yes; help: No; resizable: No; status: No; unadorned : No;";
	if (wWidth==0 && wHeight!=0){
		sFeatures="dialogHeight: "+ wHeight +"px; dialogWidth: 785px; " + commonFeature; 
	}
	if (wHeight==0 && wWidth!=0){
		sFeatures="dialogHeight: 500px; dialogWidth: " + wWidth  + "px; " + commonFeature; 
	}
	if (wWidth==0 && wHeight==0){
		sFeatures="dialogHeight: 500px; dialogWidth: 785px;" + commonFeature; 
	}
	if (wWidth!=0 && wHeight!=0){
		sFeatures="dialogHeight: "+ wHeight +"px; dialogWidth: " + wWidth + "px; " + commonFeature; 
	}
	var returnValue = window.showModalDialog(url,paramArr,sFeatures);
	return returnValue;
}

/**
* POP Up window
*/
function fnShowPopUpWindow(url,winName,wWidth,wHeight){
	// Variables used for centering the popup window
	var iTop = parseInt((screen.height-wHeight)/2);
	var iLeft = parseInt((screen.width-wWidth)/2);
	
	sFeatures="toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=no,width="+wWidth+",height="+wHeight + ",left=" + iLeft + ",top=" + iTop;
	var returnValue = window.open(url,winName,sFeatures);
	return returnValue;
}

//This function is used to generate random alphabet characters from the given array
function fnGetRandomAlphabetChar(){
	var aIndex = Math.round(Math.random()*25);
	var alphabet = new Array('a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z');

	return alphabet[aIndex];
}	
	
//This function is used to generate random string with alphabets, numbers and special chars
function fnGenerateRandomString() {
	var iRandNum1 = Math.round(Math.random()*10);
	var iRandNum2 = Math.round(Math.random()*100);
	var iRandNum3 = Math.round(Math.random()*1000);
	var iRand = iRandNum3 + fnGetRandomAlphabetChar() + iRandNum1 + fnGetRandomAlphabetChar() + iRandNum2 + fnGetRandomAlphabetChar() + iRandNum1 + fnGetRandomAlphabetChar() + iRandNum2;

	return iRand;
}

//Checkes whether object is exist
function isExist(obj){
	return(typeof(obj)=="undefined")?false:true;	
}

function CallPrint( strid )
   {
        var prtContent = document.getElementById( strid );
        var WinPrint = window.open('', '','left=0,top=0,width=1000,height=600,toolbar=1,scrollbars=1,status=0');
        WinPrint.document.write( prtContent.innerHTML );
        WinPrint.document.close();
        WinPrint.focus();
        WinPrint.print();
        WinPrint.close();
 
   } 
   
   function CallExport( strid )
   {
  
        var prtContent = document.getElementById(strid);
        document.getElementById('ctl00_ContentPlaceHolder1_hdnExport').value=prtContent.innerHTML;
     
   } 
//******************************************************************************************************
// TABS FUNCTION
//******************************************************************************************************
/**
*Populates Tabs on active screen
*@param tab number
*@return deactivate all the tabs and activate clicked tab
*/

function fnChangeActiveTab(i) {
	for( j=0; j < tabCount; ++j ) {
		if( i==j ) { fnActivateTab(j); }
		else { fnDeactivateTab(j); }
	}
}
/**
*Activate selected Tabs on active screen
*/
function fnActivateTab(i) {
	getEl( "tab-left:"+i ).className="cssActiveTabLeft";
	getEl( "tab-mid:"+i ).className="cssActiveTabBg";
	getEl( "tab-right:"+i ).className="cssActiveTabRight";
	getEl( "tab-body:"+i ).style.display='block';	
}

/**
* Deactivate selected all tabs
*/
function fnDeactivateTab(i) {
	getEl( "tab-left:"+i ).className="cssInActiveTabLeft";
	getEl( "tab-mid:"+i ).className="cssInActiveTabBg cssCursor";
	getEl( "tab-right:"+i ).className="cssInActiveTabRight";
	getEl( "tab-body:"+i ).style.display='none';
}

 