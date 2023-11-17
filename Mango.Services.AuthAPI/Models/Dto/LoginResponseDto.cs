namespace Mango.Services.AuthAPI.Models.Dto
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public UserDto User { get; set; }

    }
}
