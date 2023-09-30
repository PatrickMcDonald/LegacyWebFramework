namespace LegacyWcfService.Tracing
{
    public class TraceIdHelper
    {
        public override string ToString() => ActivityManager.GetRequestId();
    }
}
