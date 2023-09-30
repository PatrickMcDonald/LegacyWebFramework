using System.Diagnostics;
using System.Web;

namespace LegacyWcfService.Tracing
{
    public static class ActivityManager
    {
        public static void StartActivity()
        {
            if (Activity.Current == null)
            {
                var activity = new Activity("Default activity");

                var parentIdFromHeaders = HttpContext.Current?.Request.Headers["traceparent"];

                if (!string.IsNullOrEmpty(parentIdFromHeaders))
                {
                    activity.SetParentId(parentIdFromHeaders);
                }

                activity.Start();

                Activity.Current = activity;

                // Also store the activity on the HttpContext
                HttpContext.Current?.Items.Add("Activity", activity);
            }
        }

        public static void StopActivity()
        {
            GetActivity()?.Stop();
        }

        public static Activity GetActivity() => Activity.Current ?? HttpContext.Current?.Items["Activity"] as Activity;

        public static string GetRequestId()
        {
            Activity activity = GetActivity();

            return activity?.Id;
        }
    }
}
