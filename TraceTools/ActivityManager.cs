using System;
using System.Collections.Generic;
using System.Threading;

namespace TraceTools
{
    public static class ActivityManager
    {
        private static readonly AsyncLocal<string> TraceId = new AsyncLocal<string>();

        public static void StartActivity()
        {
            TraceId.Value = Guid.NewGuid().ToString();
        }

        public static void StopActivity()
        {
            TraceId.Value = null;
        }

        public static void Log(string title, IList<object> result)
        {
            result.Add(new { Title = title, TraceId = TraceId.Value });
        }

        public static string GetTraceId() => TraceId.Value;
    }
}
