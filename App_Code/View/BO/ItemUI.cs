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
/// Summary description for ItemUI
/// </summary>
public class ItemUI
{
    public ItemUI()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    # region "variables"

    int mItemCode;
    string mItemGroupName;
    int mGroupID;
    string mDescription;
    int mIsActive;
    int mInReport;
    int mIsGroup;
    int mStatusID;
    DateTime mEffectDate;


    # endregion

    # region "properties"


    public int ItemCode
    {
        get
        {
            return mItemCode;
        }
        set
        {
            mItemCode = value;
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

    public string ItemGroupName
    {
        get
        {
            return mItemGroupName;
        }
        set
        {
            mItemGroupName = value;
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

    public int IsActive
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

    public int IsGroup
    {
        get
        {
            return mIsGroup;
        }
        set
        {
            mIsGroup = value;
        }
    }

    public int InReport
    {
        get
        {
            return mInReport;
        }
        set
        {
            mInReport = value;
        }
    }

    public DateTime EffectDate
    {
        get
        {
            return mEffectDate;
        }
        set
        {
            mEffectDate = value;
        }
    }

    public int StatusID
    {
        get
        {
            return mStatusID;
        }
        set
        {
            mStatusID = value;
        }
    }
    # endregion
}
