using System;
using System.Web;

namespace TraceTools
{
    internal class TraceModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(BeginRequest);
            context.EndRequest += new EventHandler(EndRequest);
        }

        private void BeginRequest(object sender, EventArgs e)
        {
            ActivityManager.StartActivity();
            HttpContext.Current.Response.AddHeader("X-TraceId", ActivityManager.GetTraceId());
        }

        private void EndRequest(object sender, EventArgs e)
        {
            ActivityManager.StopActivity();
        }

        public void Dispose()
        {
        }
    }
}
