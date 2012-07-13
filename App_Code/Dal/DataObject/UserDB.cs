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
/// Summary description for UserDB
/// </summary>
public class UserDB
{
	public UserDB()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    # region "variables"
    int mUserID;
    int mUserTypeID;
    string mFullName;
    string mEmployeeCode;
    int mRoleID;
    string mLoginID;
    string mPassword;
    string mConfirmPassword;
    string mPermanentAddress;
    string mCurrentAddress;
    bool mIsActive;
    string mEmailID;
    string mPhoneNo;
    string mMobileNo;
    int mFormID;
    int mModuleID;
    int mLevelID;
    int mcRegionID;
    int mdRegionID;
    int mDealerID;
    int mReportsToID;
    DateTime mDateOfJoing;
    private int mSuccess;
    private int mId;
    private int mCheckID;


    # endregion
    # region "properties"

    public int DealerID
    {
        get
        {
            return mDealerID;
        }
        set
        {
            mDealerID = value;
        }
    }

    public int UserTypeID
    {
        get
        {
            return mUserTypeID;
        }
        set
        {
            mUserTypeID = value;
        }
    }


    public int dRegionID
    {
        get
        {
            return mdRegionID;
        }
        set
        {
            mdRegionID = value;
        }
    }
    public int cRegionID
    {
        get
        {
            return mcRegionID;
        }
        set
        {
            mcRegionID = value;
        }
    }

    public int LevelID
    {
        get
        {
            return mLevelID;
        }
        set
        {
            mLevelID = value;
        }
    }
    public int ModuleID
    {
        get
        {
            return mModuleID;
        }
        set
        {
            mModuleID = value;
        }
    }
    public int FormID
    {
        get
        {
            return mFormID;
        }
        set
        {
            mFormID = value;
        }
    }
    public int UserID
    {
        get
        {
            return mUserID;
        }
        set
        {
            mUserID = value;
        }
    }

    public int CheckID
    {
        get
        {
            return mCheckID;
        }
        set
        {
            mCheckID = value;
        }
    }
    public string FullName
    {
        get
        {
            return mFullName;
        }
        set
        {
            mFullName = value;
        }
    }
    public string EmployeeCode
    {
        get
        {
            return mEmployeeCode;
        }
        set
        {
            mEmployeeCode = value;
        }
    }
    public int RoleID
    {
        get
        {
            return mRoleID;
        }
        set
        {
            mRoleID = value;
        }
    }
    public string LoginID
    {
        get
        {
            return mLoginID;
        }
        set
        {
            mLoginID = value;
        }
    }
    public string Password
    {
        get
        {
            return mPassword;
        }
        set
        {
            mPassword = value;
        }
    }
    public string ConfirmPassword
    {
        get
        {
            return mConfirmPassword;
        }
        set
        {
            mConfirmPassword = value;
        }
    }
    public string PermanentAddress
    {
        get
        {
            return mPermanentAddress;
        }
        set
        {
            mPermanentAddress = value;
        }
    }

    public string CurrentAddress
    {
        get
        {
            return mCurrentAddress;
        }
        set
        {
            mCurrentAddress = value;
        }
    }
    public bool IsActive
    {
        get
        {
            return mIsActive;
        }
        set
        {
            mIsActive = value;
        }
    }

    public string EmailID
    {
        get
        {
            return mEmailID;
        }
        set
        {
            mEmailID = value;
        }
    }

    public int Id
    {
        get
        {
            return mId;
        }
        set
        {
            mId = value;
        }
    }

    public string PhoneNo
    {
        get
        {
            return mPhoneNo;
        }
        set
        {
            mPhoneNo = value;
        }
    }
    public string MobileNo
    {
        get
        {
            return mMobileNo;
        }
        set
        {
            mMobileNo = value;
        }
    }
    public DateTime DateOfJoing
    {
        get
        {
            return mDateOfJoing;
        }
        set
        {
            mDateOfJoing = value;
        }
    }
    public int Success
    {
        get
        {
            return mSuccess;
        }
        set
        {
            mSuccess = value;
        }
    }

    public int ReportsToID
    {
        get
        {
            return mReportsToID;
        }
        set
        {
            mReportsToID = value;
        }
    }
    # endregion
}
