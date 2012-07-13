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
/// Summary description for SalesDB
/// </summary>
public class SalesDB
{
    public SalesDB()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    # region "variables"

    int mID;
    int mSno;
    int mInvoiceNo;
    DateTime mDate;
    string mDealer_Code;
    string mDealer_Name;
    string mBlank;
    string mModel_Code;
    int mQuantity;
    double mSalesAmount;
    int mDiscount;
    double mSPLDIS;
    int mExciseDuty;
    int mEdu_Cess;
    int mHR_ECess;
    int mLSPD;
    int mMSPSD;
    int mDHC;
    double mTaxable;
    double mCST;
    double mLST;
    int mSurch;
    double mEntityTot;
    int mDely_Chgs;
    double mFreight;
    double mNet_Amount;
    int mCost;
    string mSOff;
    DateTime mFromDate;
    DateTime mToDate;
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

    public int Sno
    {
        get
        {
            return mSno;
        }
        set
        {
            mSno = value;
        }
    }

    public int InvoiceNo
    {
        get
        {
            return mInvoiceNo;
        }
        set
        {
            mInvoiceNo = value;
        }
    }

    public DateTime Date
    {
        get
        {
            return mDate;
        }
        set
        {
            mDate = value;
        }
    }

    public string Dealer_Code
    {
        get
        {
            return mDealer_Code;
        }
        set
        {
            mDealer_Code = value;
        }
    }

    public string Dealer_Name
    {
        get
        {
            return mDealer_Name;
        }
        set
        {
            mDealer_Name = value;
        }
    }

    public string Blank
    {
        get
        {
            return mBlank;
        }
        set
        {
            mBlank = value;
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

    public int Quantity
    {
        get
        {
            return mQuantity;
        }
        set
        {
            mQuantity = value;
        }
    }

    public double SalesAmount
    {
        get
        {
            return mSalesAmount;
        }
        set
        {
            mSalesAmount = value;
        }
    }

    public int Discount
    {
        get
        {
            return mDiscount;
        }
        set
        {
            mDiscount = value;
        }
    }

    public double SPLDIS
    {
        get
        {
            return mSPLDIS;
        }
        set
        {
            mSPLDIS = value;
        }
    }

    public int ExciseDuty
    {
        get
        {
            return mExciseDuty;
        }
        set
        {
            mExciseDuty = value;
        }
    }

    public int Edu_Cess
    {
        get
        {
            return mEdu_Cess;
        }
        set
        {
            mEdu_Cess = value;
        }
    }

    public int HR_ECess
    {
        get
        {
            return mHR_ECess;
        }
        set
        {
            mHR_ECess = value;
        }
    }

    public int LSPD
    {
        get
        {
            return mLSPD;
        }
        set
        {
            mLSPD = value;
        }
    }

    public int MSPSD
    {
        get
        {
            return mMSPSD;
        }
        set
        {
            mMSPSD = value;
        }
    }

    public int DHC
    {
        get
        {
            return mDHC;
        }
        set
        {
            mDHC = value;
        }
    }

    public double Taxable
    {
        get
        {
            return mTaxable;
        }
        set
        {
            mTaxable = value;
        }
    }

    public double CST
    {
        get
        {
            return mCST;
        }
        set
        {
            mCST = value;
        }
    }

    public double LST
    {
        get
        {
            return mLST;
        }
        set
        {
            mLST = value;
        }
    }

    public int Surch
    {
        get
        {
            return mSurch;
        }
        set
        {
            mSurch = value;
        }
    }

    public double EntityTot
    {
        get
        {
            return mEntityTot;
        }
        set
        {
            mEntityTot = value;
        }
    }

    public int Dely_Chgs
    {
        get
        {
            return mDely_Chgs;
        }
        set
        {
            mDely_Chgs = value;
        }
    }

    public double Freight
    {
        get
        {
            return mFreight;
        }
        set
        {
            mFreight = value;
        }
    }

    public double Net_Amount
    {
        get
        {
            return mNet_Amount;
        }
        set
        {
            mNet_Amount = value;
        }
    }

    public int Cost
    {
        get
        {
            return mCost;
        }
        set
        {
            mCost = value;
        }
    }

    public string SOff
    {
        get
        {
            return mSOff;
        }
        set
        {
            mSOff = value;
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

    #endregion
}
