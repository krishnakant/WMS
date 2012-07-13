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
/// Summary description for PriviledgeUI
/// </summary>
public class PriviledgeUI
{
	public PriviledgeUI()
	{
		//
		// TODO: Add constructor logic here
		//
	}




    # region "variables"

    int mPrivilegesID;
    int mRoleID;
    int mFormID;

    bool mviewing;

    private int mId;
    private int mCheckID;

    # endregion

    # region "properties"
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
    public int PrivilegesID
    {
        get
        {
            return mPrivilegesID;
        }
        set
        {
            mPrivilegesID = value;
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
public bool viewing
    {
        get
        {
            return mviewing;
        }
        set
        {
            mviewing = value;
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