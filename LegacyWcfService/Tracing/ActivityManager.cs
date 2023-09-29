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

                Activity.Current = activity.Start();
            }
        }

        public static void StopActivity()
        {
            GetActivity()?.Stop();
        }

        public static Activity GetActivity() => Activity.Current;

        public static string GetRequestId()
        {
            Activity activity = GetActivity();

            if (activity != null)
            {
                string activityId = activity.Id;
                return activityId;
            }

            return null;
        }
    }
}
