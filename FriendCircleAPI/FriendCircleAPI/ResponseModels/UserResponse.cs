namespace FriendCircleAPI.ResponseModels
{
    public class UserResponse
    {
        public string Message { get; set; }
        public UserResponseData Data { get; set; }
    }

    public class UserResponseData
    {
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}