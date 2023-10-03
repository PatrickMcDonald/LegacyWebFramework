namespace TraceTools
{
    public class TraceIdentifierSource
    {
        public override string ToString() => ActivityManager.GetTraceId();
    }
}
