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
/// Summary description for RoleUI
/// </summary>
public class RoleUI
{
	public RoleUI()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    # region "variables"

    int mRoleID;

    string mRole;

    bool mIsActive;
   
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
    public string Role
    {
        get
        {
            return mRole;
        }
        set
        {
            mRole = value;
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
