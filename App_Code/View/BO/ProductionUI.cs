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
/// Summary description for ProductionUI
/// </summary>
public class ProductionUI
{
	public ProductionUI()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    # region "variables"

    int mID;
    int mS;
    string mMaterial;
    string mSerialNo;
    string mPlnt;
    string mSLoc;
    string mDescription;
    int mProduction_Month;
    string mProduction_Month_Year;
    string mModel_Code;
    int mModelMappingID;
    DateTime mFromDate;
    DateTime mToDate;
    int mMonthID;
    int mYearID;
    string mQuarter; 


    # endregion

    # region "properties"

    public DateTime FromDate
    {
        get
        {
            return mFromDate;
        }
        set
        {
            mFromDate = value;
        }
    }

    public DateTime ToDate
    {
        get
        {
            return mToDate;
        }
        set
        {
            mToDate = value;
        }
    }
    public int ID
    {
        get
        {
            return mID;
        }
        set
        {
            mID = value;
        }
    }

    public int S
    {
        get
        {
            return mS;
        }
        set
        {
            mS = value;
        }
    }

    public string Material
    {
        get
        {
            return mMaterial;
        }
        set
        {
            mMaterial = value;
        }
    }

    public string SerialNo
    {
        get
        {
            return mSerialNo;
        }
        set
        {
            mSerialNo = value;
        }
    }

    public string Plnt
    {
        get
        {
            return mPlnt;
        }
        set
        {
            mPlnt = value;
        }
    }

    public string SLoc
    {
        get
        {
            return mSLoc;
        }
        set
        {
            mSLoc = value;
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

    public int Production_Month
    {
        get
        {
            return mProduction_Month;
        }
        set
        {
            mProduction_Month = value;
        }
    }

    public string Production_Month_Year
    {
        get
        {
            return mProduction_Month_Year;
        }
        set
        {
            mProduction_Month_Year = value;
        }
    }


    public string Model_Code
    {
        get
        {
            return mModel_Code;
        }
        set
        {
            mModel_Code = value;
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

    public string Quarter
    {
        get
        {
            return mQuarter;
        }
        set
        {
            mQuarter = value;
        }
    }


    public int ModelMappingID
    {
        get
        {
            return mModelMappingID;
        }
        set
        {
            mModelMappingID = value;
        }
    }
    # endregion

}
