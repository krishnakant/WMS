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
/// Summary description for ReworkCostUI
/// </summary>
public class ReworkCostUI
{
    public ReworkCostUI()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    # region "variables"

  
    int mGroupID;               //GroupID
    int mMonthID;
    int mYearID;
    double mReworkCost_I_Year;
    double mReworkCost_II_Year;
    int mModelCategoryID;
    string mHMR_Range;

    # endregion

    # region "properties"


    public int ModelCategoryID
    {
        get
        {
            return mModelCategoryID;
        }
        set
        {
            mModelCategoryID = value;
        }
    }

    public string HMR_Range
    {
        get
        {
            return mHMR_Range;
        }
        set
        {
            mHMR_Range = value;
        }
    }

   
  

    public int GroupID
    {
        get
        {
            return mGroupID;
        }
        set
        {
            mGroupID = value;
        }
    }

   
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


    public double ReworkCost_I_Year
    {
        get
        {
            return mReworkCost_I_Year;
        }
        set
        {
            mReworkCost_I_Year = value;
        }
    }

    public double ReworkCost_II_Year
    {
        get
        {
            return mReworkCost_II_Year;
        }
        set
        {
            mReworkCost_II_Year = value;
        }
    }

    # endregion
}
