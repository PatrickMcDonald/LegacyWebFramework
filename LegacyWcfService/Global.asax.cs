using System;

namespace LegacyWcfService
{
    public class Global : System.Web.HttpApplication
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Application_Start(object sender, EventArgs e)
        {
            log.Info("LegacyWcfService Application started.");
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            log.Info("Session_Start");
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            log.Info("Application_BeginRequest");
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            log.Info("Application_EndRequest");
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            log.Info("Application_AuthenticateRequest");
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            log.Info("Application_Error");
        }

        protected void Session_End(object sender, EventArgs e)
        {
            log.Info("Session_End");
        }

        protected void Application_End(object sender, EventArgs e)
        {
            log.Info("Application_End");
        }
    }
}
