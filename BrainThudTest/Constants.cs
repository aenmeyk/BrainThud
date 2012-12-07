using System;

namespace BrainThudTest
{
    public static class TestTypes
    {
        public const string INTEGRATION = "Integration";
        public const string LONG_RUNNING = "LongRunning";
    }

    public static class TestValues
    {
        // ReSharper disable InconsistentNaming
        public static readonly Guid GUID = Guid.Parse("16ffa7cb-53f7-4a1d-bf19-c60555c95fcf");
        public static readonly DateTime DATETIME = new DateTime(2012, 1, 1);
        // ReSharper restore InconsistentNaming

        public const int INT = 5;
        public const string STRING = "TestString";
        public const string PARTITION_KEY = "c86258da-9165-4e19-906a-4441bd298d71";
        public const string NAME_IDENTIFIER = "b979a21e36f841509fbbd1d722f80cff";
        public const string ROW_KEY = "6037e998-c399-4153-9353-00ae5e6ea1e9";
        public const string ERROR_KEY = "ErrorKey";
        public const string ERROR_MESSAGE = "Error Message";
        public const string ERROR_KEY_2 = "ErrorKey2";
        public const string ERROR_MESSAGE_2 = "Error Message 2";
        public const string VALID = "Valid";
    }

    public static class TestUrls
    {
        public const string CARDS = "http://localhost/api/cards";
        public const string QUIZ_RESULTS = "http://localhost/api/quizzes/{0}/{1}/{2}/results";
        public const string LOCALHOST = "http://localhost/";
    }
}