namespace LibraryManagementTask.Settings
{
    public class JWTSettings
    {
        public const string SectionName = "JWT";

        public string Key { get; init; }
        public string Issuer { get; init; }
        public double ExpirationInMinutes { get; init; }
    }
}
