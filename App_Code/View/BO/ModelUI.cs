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
/// Summary description for ModelUI
/// </summary>
public class ModelUI
{
    public ModelUI()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    # region "variables"

    int mCode;                  //Product Code
    string mModelCode;          //Group Name
    int mGroupID;               //GroupID
    string mDescription;
    int mIsActive;
    int mInReport;
    int mIsGroup;
    int mStatusID;
    DateTime mEffectDate;
    string mMaterial;
    int mModelCategoryID;
    int mModelSpecialID;
    int mClutchTypeID;
    int mMappingID;


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
    public int Code
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

    public string ModelCode
    {
        get
        {
            return mModelCode;
        }
        set
        {
            mModelCode = value;
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

    public int ModelCategoryID
    {
        get
        {
            return mModelCategoryID;
        }
        set
        {
            mModelCategoryID = value;
        }
    }

    public int ModelSpecialID
    {
        get
        {
            return mModelSpecialID;
        }
        set
        {
            mModelSpecialID = value;
        }
    }

    public int ClutchTypeID
    {
        get
        {
            return mClutchTypeID;
        }
        set
        {
            mClutchTypeID = value;
        }
    }

    public int MappingID
    {
        get
        {
            return mMappingID;
        }
        set
        {
            mMappingID = value;
        }
    }

    # endregion
}
