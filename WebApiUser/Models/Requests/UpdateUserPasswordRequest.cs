namespace WebApiUser.Models.Requests
{
    public class UpdateUserPasswordRequest
    {
        public int Id { get; set; }
        public string Password { get; set; }
    }
}
