namespace BrainThud.Web
{
    public static class Hosts
    {
//        public const string LOCALHOST = "http://localhost:36877/";
        public const string LOCALHOST = "http://127.0.0.1:81/";
        public const string BRAINTHUD = "http://www.brainthud.com/";
    }

    public static class RouteNames
    {
        public const string API_QUIZZES = "ApiQuizzes";
        public const string API_QUIZ_RESULTS = "ApiQuizResults";
        public const string API_CARDS = "ApiCards";
        public const string API_DEFAULT = "ApiDefault";
        public const string DEFAULT = "Default";
    }

    public static class BundlePaths
    {
        public const string JQUERY = "~/bundles/jquery";
        public const string JQUERY_UI = "~/bundles/jqueryui";
        public const string EXTERNAL_LIBS = "~/bundles/jsextlibs";
        public const string APP_LIBS = "~/bundles/jsapplibs";
        public const string MODERNIZR = "~/bundles/modernizr";
        public const string CSS = "~/Content/css";
        public const string LESS = "~/Content/less";
    }

    public static class ScriptPaths
    {
        public const string MAIN_JS = "~/Scripts/main.js";
        public const string REQUIRE_JS = "~/Scripts/require.js";
    }

    public static class Keys
    {
        public const string MASTER = "master";
        public const string CONFIGURATION = "configuration";
    }

    public static class EntityNames
    {
        public const string CONFIGURATION = "configuration";
    }

    public static class NameIdentifiers
    {
        public const string MASTER = "master";
    }

    public static class AzureTableNames
    {
        public const string CARD = "card";
        public const string CONFIGURATION = "configuration";
    }

    public static class AzureQueueNames
    {
        public const string IDENTITY = "identity";
    }

    public static class CardRowTypes
    {
        public const string CARD = "c";
        public const string RESULT = "r";
        public const string QUIZ_RESULT = "qr";
        public const string CONFIGURATION = "cfg";
    }

    public static class ConfigurationSettings
    {
        public const string AZURE_STORAGE = "DataConnectionString";
        public const int CONCURRENCY_VIOLATION_RETRIES = 10;
        public const int SEED_IDENTITIES = 20;
        public const int IDENTITY_QUEUE_VISIBILITY_TIMEOUT_SECONDS = 10;
        public static int SeedRefreshIntervalSeconds = 60;

    }

    public static class TypeValues
    {
        public const string MIN_SQL_DATETIME = "01/01/1753";
        public const string MAX_SQL_DATETIME = "12/31/9999";
    }

    public static class AzureErrorCodes
    {
        public const string CONCURRENCY_VIOLATION = "UpdateConditionNotSatisfied";
    }


}