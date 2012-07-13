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
/// Summary description for BudgetDB
/// </summary>
public class BudgetDB
{
    public BudgetDB()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    # region "variables"


    string mFinancialYear;
    int mQuarterID;
    double mBudget;
    int mModelGroupID;
    int mModelCategoryID;
    int mModelClutchID;
    int mModelSpecialID;

    # endregion

    # region "properties"

    public int QuarterID
    {
        get
        {
            return mQuarterID;
        }
        set
        {
            mQuarterID = value;
        }
    }

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

    public int ModelClutchID
    {
        get
        {
            return mModelClutchID;
        }
        set
        {
            mModelClutchID = value;
        }
    }

    public int ModelSpecialID
    {
        get
        {
            return mModelSpecialID;
        }
        set
        {
            mModelSpecialID = value;
        }
    }

    public int ModelGroupID
    {
        get
        {
            return mModelGroupID;
        }
        set
        {
            mModelGroupID = value;
        }
    }

    public double Budget
    {
        get
        {
            return mBudget;
        }
        set
        {
            mBudget = value;
        }
    }

    public string FinancialYear
    {
        get
        {
            return mFinancialYear;
        }
        set
        {
            mFinancialYear = value;
        }
    }



    # endregion
}
