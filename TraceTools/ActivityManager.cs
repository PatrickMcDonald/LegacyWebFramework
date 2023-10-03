using System.Collections.Generic;
using System.Diagnostics;
using System.Web;

namespace TraceTools
{
    public static class ActivityManager
    {
        public static void Init()
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;
        }

        public static void StartActivity()
        {
            if (Activity.Current == null)
            {
                var activity = new Activity("Trace Activity");

                var parentId = HttpContext.Current.Request.Headers["traceparent"];
                if (parentId != null)
                {
                    activity.SetParentId(parentId);
                }

                Activity.Current = activity.Start();
            }
        }

        public static void StopActivity()
        {
            Activity.Current = null;
        }

        public static void Log(string title, IList<object> result)
        {
            result.Add(new { Title = title, TraceId = GetTraceId() });
        }

        public static string GetTraceId() => Activity.Current?.Id;
    }
}
