using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for DateFormat
/// </summary>
public class DateFormat
{
    public string ConvertDateFormat(string strDate)
    {
        string strDateTemp = "";
        string[] strDateArray = strDate.Split('/');
        if (strDateArray.Length == 3)
        {
            if (strDateArray[1].Length == 1)
            {
                strDateArray[1] = "0" + strDateArray[1];
            }
            if (strDateArray[0].Length == 1)
            {
                strDateArray[0] = "0" + strDateArray[0];
            }
            strDateTemp = strDateArray[1] + '/' + strDateArray[0] + '/' + strDateArray[2];
        }
        else
        {
            strDateTemp = "01/01/1900";
        }
        return strDateTemp;
    }
   
}
