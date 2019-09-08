namespace Cellsynt.Sms
{
    /// <summary>
    /// Result of a sent SMS
    /// </summary>
    public class SendResult
    {
        internal SendResult(string[] trackingIds)
        {
            TrackingIds = trackingIds;
        }

        /// <summary>
        /// The tracking ID's, one per sent message
        /// </summary>
        public string[] TrackingIds { get; }
    }
}
