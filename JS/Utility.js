 //function for confirmation With ChangedColor on deleting the row or Grid view


//function stoperror()
//{
//  return true; 
//}
//window.onerror=stoperror
 function checkException()
   {
    
       var check=confirm('there are some Exception do you want to see exception');
       
        if(check)
        {
          window.location.href='/WMS/View/Forms/Exceptions/Exception.aspx';
        }
   }
        function fnDateComparison(pFromDate, pToDate){
	var retFlag = false;
	
	  var oFromDate =parseInt(pFromDate.substring(6,10)+pFromDate.substring(3,5)+pFromDate.substring(0,2),10)
        var oToDate =parseInt(pToDate.substring(6,10)+pToDate.substring(3,5)+pToDate.substring(0,2),10)
		if (oToDate >= oFromDate) {			
			retFlag = true;
		}			
	return retFlag;	
}
  function isValidDate(objSource, objArgs) 		// To validate an entered date.
    {
  
    var a;
    var dt;
    var tempdt;
     var sepchar;
    a = new Array(12);
    a[0] = a[2]= a[4]  = a[6]= a[7]= a[9]= a[11]= 31;
    a[3] = a[5]= a[8]  = a[10]= 30;
    a[1]=28;

     
    tempdt=objArgs.Value;
    dt=tempdt.split("/");

    if (dt.length != 3) 
    {
    dt=tempdt.split(".");
     if (dt.length != 3) 
         {
	    dt=tempdt.split("-");	
	    if (dt.length != 3) 
   	        {
	         objArgs.IsValid=false;
		     return ; 
	        }	

         }
    }

    dt[0] = parseInt(dt[0],10);
    dt[1] = parseInt(dt[1],10);
    dt[2] = parseInt(dt[2],10);


    if (isNaN(dt[0]) || isNaN(dt[1]) || isNaN(dt[2]))
    {
    objArgs.IsValid=false;
    return ; 
    }


    if ( (dt[2] % 400 == 0 ) || ( dt[2] % 4 == 0 && dt[2] %100 != 0)) 
    	
    a[1]=29;

    else
	    a[1] =28;


    if( dt[0] > a[dt[1] - 1 ] || ( dt[0] < 1 ) || (dt[1] < 1 || dt[1] > 12 ) ||( dt[2] < 1900 ) || dt[2] > 9999 )
    {
    objArgs.IsValid=false;
    return ; 


    }
    else
    {
    objArgs.IsValid=true;
    return ; 
    }
    }
    
    
  function ValidDate(objSource, objArgs) 		// To validate an entered date.
  {  
        var a;
        var dt;
        var tempdt;
        var sepchar;
        a = new Array(12);
        a[0]= a[2]= a[4]= a[6]= a[7]= a[9]= a[11]= 31;
        a[3]= a[5]= a[8]= a[10]= 30;
        a[1]=28;
         
        tempdt=objArgs.Value;
        dt=tempdt.split("/");
        if (dt.length != 3) 
        {           
            objArgs.IsValid=false;
            return ;
        }   

        dt[0] = Number(dt[0]);
        dt[1] = Number(dt[1]);
        dt[2] = Number(dt[2]);

        if (isNaN(dt[0]) || isNaN(dt[1]) || isNaN(dt[2]))
        {
            objArgs.IsValid=false;
            return ; 
        }

        if ( (dt[2] % 400 == 0 ) || ( dt[2] % 4 == 0 && dt[2] %100 != 0)) 
    	    a[1]=29;
        else
	        a[1] =28;
        if( dt[0] > a[dt[1] - 1 ] || ( dt[1] < 1 ) || (dt[1] < 1 || dt[1] > 12 ) ||( dt[2] < 1900 ) || dt[2] > 9999 )
//        if( dt[1] > a[dt[0] - 1 ] || ( dt[1] < 1 ) || (dt[0] < 1 || dt[0] > 12 ) ||( dt[2] < 1900 ) || dt[2] > 9999 )
    {
        objArgs.IsValid=false;
        return ; 


        }
        else
        {
            objArgs.IsValid=true;
            return ; 
        }
   }
   function CheckValidDateFormat(obj) 		// To validate an entered date.
  {  
        var a;
        var dt;
        var tempdt;
        var sepchar;
        a = new Array(12);
        a[0]= a[2]= a[4]= a[6]= a[7]= a[9]= a[11]= 31;
        a[3]= a[5]= a[8]= a[10]= 30;
        a[1]=28;
         
        tempdt=obj.value;
        dt=tempdt.split("/");
        if (dt.length != 3) 
        {           
            return false;
            return ;
        }   

        dt[0] = Number(dt[0]);
        dt[1] = Number(dt[1]);
        dt[2] = Number(dt[2]);

        if (isNaN(dt[0]) || isNaN(dt[1]) || isNaN(dt[2]))
        {
            return false;
            return ; 
        }

        if ( (dt[2] % 400 == 0 ) || ( dt[2] % 4 == 0 && dt[2] %100 != 0)) 
    	    a[1]=29;
        else
	        a[1] =28;
        if( dt[0] > a[dt[1] - 1 ] || ( dt[1] < 1 ) || (dt[1] < 1 || dt[1] > 12 ) ||( dt[2] < 1900 ) || dt[2] > 9999 )
//        if( dt[1] > a[dt[0] - 1 ] || ( dt[1] < 1 ) || (dt[0] < 1 || dt[0] > 12 ) ||( dt[2] < 1900 ) || dt[2] > 9999 )
    {
        return false;
        return ; 


        }
        else
        {
            return true;
            return ; 
        }
   } 
    function DateCompareValidation(source, arguments){
 
      var checkDate=CheckDateValidation();
       if (checkDate)
        {
          arguments.IsValid=true;
        }
      else
         arguments.IsValid=false;
   }
   
 
     function CheckDateValidation(from,to)
	 {
	        //var startDate = document.getElementById('ctl00_cphWWT_txtFromDate').value;
            //var endDate = document.getElementById('ctl00_cphWWT_txtToDate').value ;
            var startDate = document.getElementById(from).value;
            var endDate = document.getElementById(to).value
            if (startDate == "")
            {
                return false ;
            }
            else
            {
                if (endDate == "")
                {
                    return false ;
                }
                else
                {
                 
                   var date1 =parseInt(startDate.substring(6,10)+startDate.substring(3,5)+startDate.substring(0,2),10)
                   var date2 =parseInt(endDate.substring(6,10)+endDate.substring(3,5)+endDate.substring(0,2),10)
                    if(date1 > date2 )
                    {
                      return false ;
                    }
                    else
                    {
                       return true ;
                    }
	          }
	      }
	 }
	 
	 
	 function CheckDateValidation1()
       {
       
            var startDate = document.getElementById('ctl00_ContentPlaceHolder1_txtFromDate').value;
            var endDate = document.getElementById('ctl00_ContentPlaceHolder1_txtToDate').value ;
         
            if (startDate == "")
            {
                return false ;
            }
            else
            {
                if (endDate == "")
                {
                    return false ;
                }
                else
                {            
                    var startdt;
                    startdt=startDate.split("/");
                    if (startdt.length !=null) 
                    {
                    if (startdt.length != 3) 
                    {
                        return false ;
                    }
                    }
                    else
                    {
                          return false ;
                    }
                    
                    startdt[0]=Number(startdt[0]);
                    if (startdt[0]<10)
                    {
                        startdt[0]='0'+startdt[0];
                    }
                    startdt[1]=Number(startdt[1]);
                    if (startdt[1]<10)
                    {
                        startdt[1]='0'+startdt[1];
                    }
                
                    
                    var enddt;
                   
                     enddt=endDate.split("/");
                     if(enddt.length!=null)
                     {
                     if (enddt.length != 3) 
                     {
                        return false ;
                     }
                      }
                      else
                      {
                           return false ;
                      }
                    enddt[0]=Number(enddt[0]);
                    if (enddt[0]<10)
                    {                 
                        enddt[0]='0'+enddt[0];
                    }              
                    
                    enddt[1]=Number(enddt[1]);             
                    if (enddt[1]<10)
                    {                 
                        enddt[1]='0'+enddt[1];
                    } 
                    
                   
                    
                    var date1 = new Date(startdt[2],startdt[1],startdt[0]);
                    var date2 = new Date(enddt[2],enddt[1],enddt[0]);
                    if(date1 > date2 )
                    {
                        
                        return false ;
                    }
                    else
                    {
                        return true;
                    }
                  
                }
            }
        }
   
   
     function isClientValidDate(objArgs) 		// To validate an entered date.
  {  

        var a;
        var dt;
        var tempdt;
        var sepchar;
        a = new Array(12);
        a[0]= a[2]= a[4]= a[6]= a[7]= a[9]= a[11]= 31;
        a[3]= a[5]= a[8]= a[10]= 30;
        a[1]=28;
         
        tempdt=objArgs.value;
        dt=tempdt.split("/");
        if (dt.length != 3) 
        {           
           
            return false;
        }   

        dt[0] = parseInt(dt[0],10);
        dt[1] = parseInt(dt[1],10);
        dt[2] = parseInt(dt[2],10);

        if (isNaN(dt[0]) || isNaN(dt[1]) || isNaN(dt[2]))
        {
           return false;
        }

        if ( (dt[2] % 400 == 0 ) || ( dt[2] % 4 == 0 && dt[2] %100 != 0)) 
    	    a[1]=29;
        else
	        a[1] =28;

        if( dt[0] > a[dt[1] - 1 ] || ( dt[0] < 1 ) || (dt[1] < 1 || dt[1] > 12 ) ||( dt[2] < 1900 ) || dt[2] > 9999 )
        {
           return false;
        }
        else
        {
           return true;
        }
   } 

 function FillAjaxCombo(strcombo,strhiddentextbox,bannerCommon)
  {
     try
	  {		
	     if(bannerCommon.ParseResult())
	     {
	       
	       var str=""  ;
	       var ddlCombo = strcombo;
           while(ddlCombo.length>0)
	       {			
   		      for (m = 0; m < ddlCombo.length; m++) 
		      {
		        ddlCombo.options[m]=null;
		      }
		   }
		   for (i = 0; i < bannerCommon.resultSet.length; i++) 
		   {
	          var cols=bannerCommon.resultSet[i];
		      var count = cols[0];
 			  var k=1;
    		  var l=2;
	    	  for(j=0;j<count;j++)
			  {
			    var newOption = new Option(cols[k], cols[l]);
                ddlCombo.options[j] = newOption;
  			    if(strhiddentextbox.value==cols[l])
                {
                  ddlCombo.options[j].selected=true;
                }
  			    k+=2;
			    l+=2;
               }
	         }
	       }
	     
	   	}
        catch(e){}
        
        
  }
  function stoperror()
{ return true; 
}
window.onerror=stoperror
        function setLabelText(ID,Text)
        {
           document.getElementById('ctl00_ContentPlaceHolder1_lblMessage').innerHTML =Text ;
           setTimeout("setLabelText('ctl00_ContentPlaceHolder1_lblMessage','')",3000);
        }
        
    function MailToInfo(btnid)
    {
  
        var myStringtest = btnid;
        var mySplitResulttest = myStringtest.split("_");
        var IDtest = mySplitResulttest[0]+'_'+mySplitResulttest[1]+'_'+mySplitResulttest[2]+'_'+mySplitResulttest[3];
        IDtest=IDtest+'_hdnEmail'; 
        var Email=document.getElementById(IDtest).value;
        window.location='mailto:'+Email;
        return false;
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
        //prtContent = ReplaceTag(prtContent.innerHTML);
       
        document.getElementById('ctl00_ContentPlaceHolder1_hdnExport').value=prtContent.innerHTML;
     
   }
   
   function ReplaceTag(printtext)
{
//Remove TextArea Tag by ''
var re = /(<TEXTAREA([^>]+)>)/gi;
printtext=printtext.replace(re,'');
//Remove Link  Tag by ''

re =/(<A([^>]+)>)/gi;
printtext=printtext.replace(re,'');

printtext=printtext.replace(/<\/TEXTAREA>/g,'');
printtext=printtext.replace(/<\/A>/g,'');
//Remove IMG Tag by ''
re =/(<IMG ([^>]+)>)/gi;
printtext=printtext.replace(re,'');
re =/(<asp:Image ([^>]+)>)/gi;
printtext=printtext.replace(re,'');
re =/(<SPAN ([^>]+)>)/gi;
printtext=printtext.replace(re,'');
printtext=printtext.replace(/<\/SPAN>/g,'');
re =/(<INPUT CLASS=BORDER ([^>]+)>)/gi;
printtext=printtext.replace(re,'');
//Remove Date validation  Error Msg by ''
printtext=printtext.replace(/\(dd-mm-yyyy\)/g,'');
re =/(<TR style="CURSOR: ([^>]+)>)/gi;
printtext=printtext.replace(re,'<TR>');
printtext=printtext.replace(/Actual date must be less than Today/g,'\'');
printtext=printtext.replace(/''s Date/g,'');

printtext=printtext.replace(/View <\/TD>/g,'<\/TD>');
//Set All TD Value To Middle
printtext=printtext.replace(/<TD align=middle/g,'<TD');

printtext=printtext.replace(/<TD/g,'<TD align=middle');
//Replace IMages for Pages Navigation on Grid
re=/(<INPUT style="BORDER-TOP-WIDTH: ([^>]+)>)/gi;
printtext=printtext.replace(re,'');
re=/(<SELECT ([^>]+)>)/gi;
printtext=printtext.replace(re,'');
printtext=printtext.replace(/<\/SELECT>/g,'');

return printtext;
} 
   
   
   var SIPL= {};

SIPL.Allow ={
		onlyFloat: function(pValue){
		var isDecimalExist=false;
		if(event.shiftKey || event.altKey) return false;
	
		if (pValue.indexOf(".")!=-1){
			isDecimalExist=true;
		}
		if ((event.keyCode >=48  && event.keyCode <=57) || (event.keyCode >=96  && event.keyCode <=105) || (event.keyCode >=35  && event.keyCode <=37) || event.keyCode ==8 || event.keyCode ==39 || event.keyCode ==46 || event.keyCode ==9  || event.keyCode ==190 || event.keyCode ==110){
			if(isDecimalExist && (event.keyCode ==190 || event.keyCode ==110)){
				return false;
			}else{
				return true;
			}
		}else{
			return false;
		}
	},
	
	onlySignedFloat: function(pValue){
		var isDecimalExist=false;
		if(event.ctrlKey || event.altKey) return false;

		if (pValue.indexOf(".")!=-1){
			isDecimalExist=true;
		}
		if ((event.keyCode >=48  && event.keyCode <=57) || (event.keyCode >=96  && event.keyCode <=105) || (event.keyCode >=35  && event.keyCode <=37) || event.keyCode ==8 || event.keyCode ==39 || event.keyCode ==46 || event.keyCode ==9  || event.keyCode ==190 || event.keyCode ==110 || event.keyCode ==189  || event.keyCode ==109 || event.keyCode ==188){
			if(isDecimalExist && (event.keyCode ==190 || event.keyCode ==110)){
				return false;
			}else{
				return true;
			}
		}else{
			return false;
		}
	},
	
	onlyInteger: function(pValue){
		if(event.shiftKey || event.ctrlKey || event.altKey) return false;	
		if ((event.keyCode >=48  && event.keyCode <=57) || (event.keyCode >=96  && event.keyCode <=105) || (event.keyCode >=35  && event.keyCode <=37) || event.keyCode ==8 || event.keyCode ==39 || event.keyCode ==46 || event.keyCode ==9){
			return true
		}else{
			return false
		  }
	},
	
	onlyCurrency: function(pValue){
		var isDecimalExist=false; 
		if(event.ctrlKey || event.altKey) return false;
	
		if (pValue.indexOf(".")!=-1){
			isDecimalExist=true;
		}
		if ((event.keyCode >=48  && event.keyCode <=57) || (event.keyCode >=96  && event.keyCode <=105) || (event.keyCode >=35  && event.keyCode <=37) || event.keyCode ==8 || event.keyCode ==39 || event.keyCode ==46 || event.keyCode ==9  || event.keyCode ==190 || event.keyCode ==110 || event.keyCode ==188){
			if(isDecimalExist && (event.keyCode ==190 || event.keyCode ==110)){
				return false;
			}else{
				return true;
			}
		}else{
			return false;
		}
	},
	
	onlyDate: function(pValue){
		if(event.shiftKey || event.ctrlKey || event.altKey) return false;	
		if ((event.keyCode >=48  && event.keyCode <=57) || (event.keyCode >=96  && event.keyCode <=105) || (event.keyCode >=35  && event.keyCode <=37) || event.keyCode ==8 || event.keyCode ==39 || event.keyCode ==46 || event.keyCode ==9 || event.keyCode ==191 || event.keyCode ==111){
			return true
		}else{
			return false
		}
	}
}
 function CheckFileExtension(id)
    {       
   
        var FileExtension = /^.+\.(xls|doc|rtf|pdf|txt|htm|html)$/;
        var fileName=document.getElementById(id).value;
        if(!FileExtension.test(fileName.toLowerCase()))
        {   
            var who=document.getElementsByName(id)[0];
            var who2= who.cloneNode(false);
            who2.onchange= who.onchange;
            who.parentNode.replaceChild(who2,who);
            alert("xls, doc,pdf,txt,htm,html and rtf  file only!");
       }   
    }
    
    function CheckXlsFileExtension(id)
    {
        var FileExtension = /^.+\.(xls|XLS)$/;
        var fileName=document.getElementById(id).value;
        if(!FileExtension.test(fileName.toLowerCase()))
        {   
            var who=document.getElementsByName(id)[0];
            var who2= who.cloneNode(false);
            who2.onchange= who.onchange;
            who.parentNode.replaceChild(who2,who);
            alert("xls file only!");
       }   
    }
  function checkEmptyDate(id)
  {
  
        var TDate = document.getElementById(id).value;
        if (TDate == "")
        {
            alert('Please Select Date')
            return false ;
        }
        else
        {
           return  true;
        }

   }
     function showMassage(id)
        {
            el = document.getElementById(id);
            el.style.visibility = (el.style.visibility == "visible") ? "hidden" : "visible";
        }
     function getLargeImage(img1)
    {
     
        document.getElementById('divtest').innerHTML= '' +
       			'<div id="popupControls" style="border-color:#000000;width:100%;height:100%;">' +
					'<img src="'+img1+'" border="1"  border-color="#000000" id="pop1CloseBox" />' +
				'<span ><center><input type="button" class="cssButton" value="Close" onclick="javascript:closeModelDiv();" /></center> <span></div> ' ;
     
        showMassage('divtest');
    }
     function closeModelDiv()
    {
       document.getElementById('divtest').innerHTML= '';
       showMassage('divtest');
    }
    
     function SetAllCheckBoxes(cbControl, id)
    {   
        var state =  document.getElementById(id).checked; 
        var chkBoxList = document.getElementById(cbControl);
        var chkBoxCount= chkBoxList.getElementsByTagName("input");
        for(var i=0;i<chkBoxCount.length;i++)
        {
            chkBoxCount[i].checked = state;
        }
       
        return false; 
    }
    
    function getCheckBoxStatus(tbControl)
    {   
    
        var status =  0; 
        var chkBoxList = document.getElementById(tbControl);
        if (chkBoxList==null)
        {
        
        return false;
        }
        else
        {
        var chkBoxCount= chkBoxList.getElementsByTagName("input");
        
        for(var i=0;i<chkBoxCount.length;i++)
        {
            if(chkBoxCount[i].checked)
            {
               status=1;
               break;            
            }
        }
         if(status==1){return true; }
         else { if(status==0){return false; } }
         
        }
       
    }
    
    function CloseWindow()
     {
     window.close();
     return false;
     }