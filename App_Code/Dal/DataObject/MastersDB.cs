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
/// Summary description for MastersDB
/// </summary>
public class MastersDB
{
    public MastersDB()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    # region "variables"

    string mCode;
    string mDescription;
  
    # endregion

    # region "properties"
  
    public string Code
    {
        get
        {
            return mCode;
        }
        set
        {
            mCode = value;
        }
    }

    public string Description
    {
        get
        {
            return mDescription;
        }
        set
        {
            mDescription = value;
        }
    }

   
    # endregion

}
