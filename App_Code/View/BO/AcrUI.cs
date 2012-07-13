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
/// Summary description for AcrUI
/// </summary>
public class AcrUI
{
    public AcrUI()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    # region "variables"

    int mID;
    string mDLR_REF;
    decimal mWCDOCNO;
    Int64 mTRACTOR_NO;
    string mENGINE_NO;
    DateTime mINS_DATE;
    DateTime mDEF_DATE;
    DateTime mREP_DATE;
    int mHMR;
    string mDLR_CO;
    string mDEALER_NAME;
    string mREG;
    DateTime mCR_DATE;
    string mITEM_CODE;
    string mDESCRIPTION;
    int mQUANTITY;
    string mCOST;
    int mDEF;
    decimal mNDP;
    decimal mVALUE;
    int mCVOICE;
    decimal mOUTLV;
    string mDT;
    int mCUL_CODE;
    double mBLANK;
    double mCR_AMOUNT;
    double mAUTH_AMOUNT;
    double mDIFF;
    int mProduction_Month;
    string mModel_Code;
    string mHMR_Range;
    string mProduction_Month_Year;
    DateTime mFromDate;
    DateTime mToDate;
    int mMonthID;
    int mYearID;
    string mQuarter;
    int mIsEngine;
    string mEngine;
    int mModelMappingID;

    # endregion

    # region "properties"


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

    public decimal WCDOCNO
    {
        get
        {
            return mWCDOCNO;
        }
        set
        {
            mWCDOCNO = value;
        }
    }

    public int IsEngine
    {
        get
        {
            return mIsEngine;
        }
        set
        {
            mIsEngine = value;
        }
    }

    public string Engine
    {
        get
        {
            return mEngine;
        }
        set
        {
            mEngine = value;
        }
    }


    public Int64 TRACTOR_NO
    {
        get
        {
            return mTRACTOR_NO;
        }
        set
        {
            mTRACTOR_NO = value;
        }
    }

    public string ENGINE_NO
    {
        get
        {
            return mENGINE_NO;
        }
        set
        {
            mENGINE_NO = value;
        }
    }

    public string DLR_REF
    {
        get
        {
            return mDLR_REF;
        }
        set
        {
            mDLR_REF = value;
        }
    }


    public DateTime INS_DATE
    {
        get
        {
            return mINS_DATE;
        }
        set
        {
            mINS_DATE = value;
        }
    }

    public DateTime DEF_DATE
    {
        get
        {
            return mDEF_DATE;
        }
        set
        {
            mDEF_DATE = value;
        }
    }

    public DateTime REP_DATE
    {
        get
        {
            return mREP_DATE;
        }
        set
        {
            mREP_DATE = value;
        }
    }

    public int HMR
    {
        get
        {
            return mHMR;
        }
        set
        {
            mHMR = value;
        }
    }

    public string DLR_CO
    {
        get
        {
            return mDLR_CO;
        }
        set
        {
            mDLR_CO = value;
        }
    }

    public string DEALER_NAME
    {
        get
        {
            return mDEALER_NAME;
        }
        set
        {
            mDEALER_NAME = value;
        }
    }

    public string REG
    {
        get
        {
            return mREG;
        }
        set
        {
            mREG = value;
        }
    }

    public DateTime CR_DATE
    {
        get
        {
            return mCR_DATE;
        }
        set
        {
            mCR_DATE = value;
        }
    }

    public string ITEM_CODE
    {
        get
        {
            return mITEM_CODE;
        }
        set
        {
            mITEM_CODE = value;
        }
    }

    public string DESCRIPTION
    {
        get
        {
            return mDESCRIPTION;
        }
        set
        {
            mDESCRIPTION = value;
        }
    }

    public int QUANTITY
    {
        get
        {
            return mQUANTITY;
        }
        set
        {
            mQUANTITY = value;
        }
    }

    public string COST
    {
        get
        {
            return mCOST;
        }
        set
        {
            mCOST = value;
        }
    }

    public int DEF
    {
        get
        {
            return mDEF;
        }
        set
        {
            mDEF = value;
        }
    }

    public decimal NDP
    {
        get
        {
            return mNDP;
        }
        set
        {
            mNDP = value;
        }
    }

    public decimal VALUE
    {
        get
        {
            return mVALUE;
        }
        set
        {
            mVALUE = value;
        }
    }

    public int CVOICE
    {
        get
        {
            return mCVOICE;
        }
        set
        {
            mCVOICE = value;
        }
    }

    public decimal OUTLV
    {
        get
        {
            return mOUTLV;
        }
        set
        {
            mOUTLV = value;
        }
    }

    public string DT
    {
        get
        {
            return mDT;
        }
        set
        {
            mDT = value;
        }
    }

    public int CUL_CODE
    {
        get
        {
            return mCUL_CODE;
        }
        set
        {
            mCUL_CODE = value;
        }
    }

    public double BLANK
    {
        get
        {
            return mBLANK;
        }
        set
        {
            mBLANK = value;
        }
    }

    public double CR_AMOUNT
    {
        get
        {
            return mCR_AMOUNT;
        }
        set
        {
            mCR_AMOUNT = value;
        }
    }

    public double AUTH_AMOUNT
    {
        get
        {
            return mAUTH_AMOUNT;
        }
        set
        {
            mAUTH_AMOUNT = value;
        }
    }

    public double DIFF
    {
        get
        {
            return mDIFF;
        }
        set
        {
            mDIFF = value;
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
