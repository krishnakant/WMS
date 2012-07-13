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
/// Summary description for DealerDB
/// </summary>
public class DealerDB
{
	public DealerDB()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    # region "variables"

    int mDealerID;

    string mDealer;
    string mCode;
    string mCity;
   string mInstallerName;
    bool mIsActive;
    bool mIsOperatingDealer;
    int mRegionID;
    private int mId;
    private int mCheckID;

    # endregion

    # region "properties"
    public bool IsOperatingDealer
    {
        get
        {
            return mIsOperatingDealer;
        }
        set
        {
            mIsOperatingDealer = value;
        }
    }
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
    public int RegionID
    {
        get
        {
            return mRegionID;
        }
        set
        {
            mRegionID = value;
        }
    }

    public string City
    {
        get
        {
            return mCity;
        }
        set
        {
            mCity = value;
        }
    }

    public string InstallerName
    {
        get
        {
            return mInstallerName;
        }
        set
        {
            mInstallerName = value;
        }
    }

    public string Dealer
    {
        get
        {
            return mDealer;
        }
        set
        {
            mDealer = value;
        }
    }

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

    # endregion
}
