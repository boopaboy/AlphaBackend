namespace Authentication.Services.Models
{
    public class LoginResult
    {
        public bool Succeeded { get; set; }
        public string? Error { get; set; }
        public string? Token { get; set; }


    }
}
