namespace Cellsynt.Sms
{
    public class SendResult
    {
        internal SendResult(string[] trackingIds)
        {
            TrackingIds = trackingIds;
        }

        public string[] TrackingIds { get; }
    }
}
