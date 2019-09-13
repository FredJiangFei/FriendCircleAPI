namespace FriendCircle.RequestModels
{
    public class MomentResponse
    {
        public string Message { get; set; }
        public MomentResponseData[] Data { get; set; }
    }

    public class MomentResponseData
    {
        public string MomentId { get; set; }
        public string MomentUserId { get; set; }
        public string Content { get; set; }
    }
}