namespace Identity.Infrastructure.Auth
{
    public class JwtTokenGenerator
    {
        public string GenerateToken(string email, string role)
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}
