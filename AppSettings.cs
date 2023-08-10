namespace Muquirana
{
    public class AppSettings
    {
        public static string DataBaseName = "database2.db";
        public static string DatabaseDirectory = FileSystem.AppDataDirectory;
        public static string DatabasePath = Path.Combine(DatabaseDirectory, DataBaseName);
    }
}