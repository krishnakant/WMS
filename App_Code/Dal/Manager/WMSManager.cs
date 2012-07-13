using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient; 

/// <summary>
/// Summary description for WMSManager
/// </summary>
public class WMSManager
{
    static WMSManager instance = null;
    public WMSManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static WMSManager Instance
    {
        get
        {
            if (instance == null)
                instance = new WMSManager();
            return instance;
        }
    }

   
  
}
