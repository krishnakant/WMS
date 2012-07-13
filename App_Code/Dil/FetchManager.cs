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
/// Summary description for FetchManager
/// </summary>
namespace WMS.DIL
{
    public class FetchManager
    {
        public FetchManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region getAcrManager
        public static AcrManager getAcrManager()
        {
            return AcrManager.Instance;
        }
        #endregion

        #region getAcrManager
        public static SalesManager getSalesManager()
        {
            return SalesManager.Instance;
        }
        #endregion

        #region getAcrManager
        public static ProductionManager getProductionManager()
        {
            return ProductionManager.Instance;
        }
        #endregion

        #region getAcrManager
        public static MastersManager getMastersManager()
        {
            return MastersManager.Instance;
        }
        #endregion
        #region getConfiguratorManager
        public static ConfiguratorManager getConfiguratorManager()
        {
            return ConfiguratorManager.Instance;
        }
        #endregion
        #region getRoleManager
        public static RoleManager getRoleManager()
        {
            return RoleManager.Instance;
        }
  #endregion
        #region getUserManager
        public static UserManager getUserManager()
        {
            return UserManager.Instance;
        }
         #endregion
        #region getDealerManager
        public static DealerManager getDealerManager()
        {
            return DealerManager.Instance;
        }
         #endregion
        #region getPriviledgeManager
        public static PriviledgeManager getPriviledgeManager()
        {
            return PriviledgeManager.Instance;
        }
         #endregion
    }

}