namespace AspNetCoreWebApi.Core.Configuration
{
    public static class Constants
    {
        public const string DbConnectionSecret = "aspNetCoreWebApiDatabaseSecret";
        //public const string DbConnectionSecret = "AspNetCoreWebApiDatabase";
        public const string JwtIssuerSecret = "jwtIssuerSecret";
        public const string JwtSigningKeySecret = "jwtSigningKeySecret";
        public const string JwtAudienceSecret = "jwtAudienceSecret";

        public const string AspNetCoreEnvironmentKey = "ASPNETCORE_ENVIRONMENT";
        public const string LoggingKey = "Logging";
        public const string KestrelKey = "Kestrel";
        public const string APiErrorMessage = "An unexpected error has occurred. Please check the web api logs.";

    }
}
