/*
// Free for any type of use so long as original notice remains unchanged.
// Report errors to feedback@ashishware.com
// Copyrights 2007, Ashish Patil , ashishware.com
*/

function AJAXGraph(divArray)
{
var values;
var divs;

this.PlotValue = function (newval)
{
    var i;
   
    for(i=0;i<values.length-1;i++)
    {values[i]=values[i+1];}
    values[i]=newval;
        
    for(i=0;i<divs.length;i++)
    {document.getElementById(divs[i]).style.height = values[i]+ 'px';}

}

function Init(_divArray)
{ 
    var x;
    values  = new Array(_divArray.length);
    divs = _divArray;
    for(x=0;x<values.length;x++)
    values[x]=0;    
}

Init(divArray);

}
