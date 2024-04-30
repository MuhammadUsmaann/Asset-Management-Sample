namespace AssetManagement.Server.Infrastructure.Helpers
{
    public class WebAppSettings
    {
        public JWTOptions? JWT { get; set; }
    }

    public class JWTOptions
    {
        public string? ValidAudience { get; set; }
        public string? ValidIssuer { get; set; }
        public string? Secret { get; set; }
    }

}
