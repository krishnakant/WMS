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
/// Summary description for MonthOpenCloseUI
/// </summary>
public class MonthOpenCloseUI
{
    public MonthOpenCloseUI()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    # region "variables"

    int mMonthID;
    int mYearID;
    int mStatus;


    # endregion

    # region "properties"


    public int MonthID
    {
        get
        {
            return mMonthID;
        }
        set
        {
            mMonthID = value;
        }
    }

    public int YearID
    {
        get
        {
            return mYearID;
        }
        set
        {
            mYearID = value;
        }
    }

    public int Status
    {
        get
        {
            return mStatus;
        }
        set
        {
            mStatus = value;
        }
    }
   

    # endregion



}
