﻿namespace BrainThud.Web
{
    public static class Hosts
    {
        public const string LOCALHOST = "http://localhost:36877/";
        public const string BRAINTHUD = "http://www.brainthud.com/";
    }

    public static class RouteNames
    {
        public const string API_QUIZZES = "ApiQuizzes";
        public const string API_QUIZ_RESULTS = "ApiQuizResults";
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

    public static class PartitionKeys
    {
        public const string MASTER = "master";
    }

    public static class EntityNames
    {
        public const string CONFIGURATION = "configuration";
    }

    public static class EntitySetNames
    {
        public const string CARD = "Card";
    }

    public static class CardRowTypes
    {
        public const string CARD = "c";
        public const string RESULT = "r";
    }

    public static class ConfigurationSettings
    {
        public const string AZURE_STORAGE = "DataConnectionString";
    }

    public static class TypeValues
    {
        public const string MIN_SQL_DATETIME = "01/01/1753";
        public const string MAX_SQL_DATETIME = "12/31/9999";
    }
}