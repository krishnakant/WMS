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
/// Summary description for CulpritUI
/// </summary>
public class CulpritUI
{
    public CulpritUI()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    # region "variables"
    int mStatusID;
    int mCulpritCode;
    string mCulpritGroupName;
    int mGroupID;
    string mDescription;
    int mIsActive;
    int mInReport;
    int mIsGroup;
    DateTime mEffectDate;

    # endregion

    # region "properties"
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

    public int CulpritCode
    {
        get
        {
            return mCulpritCode;
        }
        set
        {
            mCulpritCode = value;
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

    public string CulpritGroupName
    {
        get
        {
            return mCulpritGroupName;
        }
        set
        {
            mCulpritGroupName = value;
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
    # endregion
}
