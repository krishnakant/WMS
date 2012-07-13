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
/// Summary description for ConfiguratorUI
/// </summary>
public class ConfiguratorUI
{
	public ConfiguratorUI()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    # region "variables"

    int mID;
    int mGroupID;
    string mModelGroupName;
    string mGroupName;
    string msource;
    string mCode;
    bool mIsNew;
    int mWarrantyPeriod;
    # endregion

    # region "properties"

    public bool IsNew
    {
        get
        {
            return mIsNew;
        }
        set
        {
            mIsNew = value;
        }
    }

    public int WarrantyPeriod
    {
        get
        {
            return mWarrantyPeriod;
        }
        set
        {
            mWarrantyPeriod = value;
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
    public string source
    {
        get
        {
            return msource;
        }
        set
        {
            msource = value;
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
    public string GroupName
    {
        get
        {
            return mGroupName;
        }
        set
        {
            mGroupName = value;
        }
    }
    public string ModelGroupName
    {
        get
        {
            return mModelGroupName;
        }
        set
        {
            mModelGroupName = value;
        }
    }



    # endregion
}
