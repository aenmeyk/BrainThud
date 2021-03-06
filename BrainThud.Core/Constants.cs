﻿using System;

namespace BrainThud.Core
{
    public static class Constants
    {
        public const string FEDERATION_CALLBACK_END_ID = "end";
    }

    public static class Urls
    {
        public const string LOCALHOST = "http://localhost:36877/";
//        public const string LOCALHOST = "http://127.0.0.1:81/";
        public const string BRAINTHUD = "http://www.brainthud.com/";
        public const string IDENTITY_PROVIDERS = @"https://brainthud.accesscontrol.windows.net/v2/metadata/IdentityProviders.js?protocol=wsfederation&realm={0}&reply_to=&context=rm%3d0%26id%3dpassive%26ru%3D%252Fhome%252F&request_id=&version=1.0&callback=ShowSigninPage";
    }

    public static class RouteNames
    {
        public const string API_QUIZ_CARDS = "ApiQuizCards";
        public const string API_CARD_DECK_CARDS = "ApiCardDeckCards";
        public const string API_QUIZ_RESULTS = "ApiQuizResults";
        public const string API_CARDS = "ApiCard";
        public const string API_CARDS_BATCH = "ApiCardsBatch";
        public const string API_CARD_DECKS = "ApiCardDecks";
        public const string API_DEFAULT = "ApiDefault";
        public const string DEFAULT = "Default";
        public const string DECK = "Deck";
        public const string CARD = "Card";
        public const string HOME = "Home";
        public const string SITEMAP = "Sitemap";
    }

    public static class Routes
    {
        public const string FEDERATION_CALLBACK = "/api/federationcallback/";
    }

    public static class BundlePaths
    {
        public const string JQUERY = "~/bundles/jquery";
        public const string JQUERY_UI = "~/bundles/jqueryui";
        public const string EXTERNAL_LIBS = "~/bundles/jsextlibs";
        public const string APP_LIBS = "~/bundles/jsapplibs";
        public const string MODERNIZR = "~/bundles/modernizr";
        public const string PUBLIC = "~/bundles/public";
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
        public const string CARD_DECK = "cd";
        public const string QUIZ_RESULT = "qr";
        public const string CONFIGURATION = "cfg";
    }

    public static class ConfigurationSettings
    {
        public const string AZURE_STORAGE = "DataConnectionString";
        public const int CONCURRENCY_VIOLATION_RETRIES = 10;
        public const int SEED_IDENTITIES = 200;
        public const int IDENTITY_QUEUE_VISIBILITY_TIMEOUT_SECONDS = 10;
        public static int SeedRefreshIntervalSeconds = 120;
        public const string TEST_PARTITION_KEY = "TestNameIdentifier-314";
        public const string DEV_PARTITION_KEY = "5dfwtubuqmmf0foup1nhorsbgt5yilyqwp6vj44knre-5219";
        public const string DEV_PARTITION_KEY_2 = "httpswwwgooglecomaccountso8ididaitoawn66whrug-vmzp4sx7ikz2px5njx5dbv2u-5220";
        public const int PARTITION_KEY_SLUG_LENGTH = 1024;
        public const int CARD_DECK_SLUG_LENGTH = 1024;
        public const int CARD_SLUG_LENGTH = 125;
        public const int PAGE_TITLE_LENGTH = 75;
        public const int SESSION_TOKEN_REISSUE_DURATION_MINUTES = 120;
    }

    public static class TypeValues
    {
        // ReSharper disable InconsistentNaming
        public static readonly DateTime MIN_SQL_DATETIME = new DateTime(1753, 1, 1);
        public static readonly DateTime MAX_SQL_DATETIME = new DateTime(9999, 12, 31);
        // ReSharper restore InconsistentNaming

        public const string MIN_SQL_DATETIME_STRING = "01/01/1753";
        public const string MAX_SQL_DATETIME_STRING = "12/31/9999";
    }

    public static class AzureErrorCodes
    {
        public const string CONCURRENCY_VIOLATION = "UpdateConditionNotSatisfied";
    }

    public static class HttpHeaders
    {
        public const string X_CLIENT_DATE = "X-Client-Date";
        public const string X_TEST = "X-Test";
    }
}