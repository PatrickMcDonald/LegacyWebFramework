using System;
using System.Diagnostics;
using System.Web;

namespace LegacyWcfService.Tracing
{
    public class TracingModule : IHttpModule
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Init(HttpApplication context)
        {
            log.Info("BEGIN TracingModule.Init");

            Activity.DefaultIdFormat = ActivityIdFormat.W3C;
            log4net.GlobalContext.Properties["activity"] = new TraceIdHelper();

            context.BeginRequest += new EventHandler(BeginRequest);
            context.EndRequest += new EventHandler(EndRequest);

            log.Info("END TracingModule.Init");
        }

        public void Dispose()
        {
        }

        private static void BeginRequest(object sender, EventArgs e)
        {
            log.Info("BEGIN TracingModule.BeginRequest");

            ActivityManager.StartActivity();

            log.Info("END TracingModule.BeginRequest");
        }

        private static void EndRequest(object sender, EventArgs e)
        {
            log.Info("BEGIN TracingModule.EndRequest");

            ActivityManager.StopActivity();

            log.Info("END TracingModule.EndRequest");
        }
    }
}
