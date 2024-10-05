namespace Blog_DevIO.API.Configurations
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public int Expiration { get; set; } = 1;
        public string Issuer { get; set; } = "BlogDevIO";
        public string Audience { get; set; } = "Api";
    }
}
