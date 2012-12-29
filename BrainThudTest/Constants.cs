using System;
using BrainThud.Web;

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
        public static readonly DateTime DATETIME = new DateTime(YEAR, MONTH, DAY);
        public static readonly string CARD_PARTITION_KEY = NAME_IDENTIFIER + '-' + USER_ID;
        public static readonly string CARD_ROW_KEY = CardRowTypes.CARD + '-' + CARD_ID;
        public static readonly string QUIZ_RESULT_ROW_KEY = CardRowTypes.QUIZ_RESULT + '-' + QUIZ_ID;
        public static readonly string CONFIGURATION_ROW_KEY = CardRowTypes.CONFIGURATION + '-' + CONFIGURATION_ID;
        // ReSharper restore InconsistentNaming

        public const int INT = 5;
        public const string STRING = "TestString";
        public const int USER_ID = 21;
        public const int CARD_ID = 583;
        public const int QUIZ_ID = 37;
        public const int QUIZ_RESULT_ID = 32;
        public const int CONFIGURATION_ID = 85;
        public const string PARTITION_KEY = "c86258da-9165-4e19-906a-4441bd298d71";
        public const string NAME_IDENTIFIER = "b979a21e36f841509fbbd1d722f80cff";
        public const string ROW_KEY = "6037e998-c399-4153-9353-00ae5e6ea1e9";
        public const string ERROR_KEY = "ErrorKey";
        public const string ERROR_MESSAGE = "Error Message";
        public const string ERROR_KEY_2 = "ErrorKey2";
        public const string ERROR_MESSAGE_2 = "Error Message 2";
        public const string VALID = "Valid";
        public const int YEAR = 2012;
        public const int MONTH = 7;
        public const int DAY = 21;
    }

    public static class TestUrls
    {
        public const string CARDS = "http://localhost/api/cards";
        public const string QUIZ_RESULTS = "http://localhost/api/quizzes/{0}/{1}/{2}/{3}/results";
        public const string LOCALHOST = "http://localhost/";
    }

    public static class ExceptionMessages
    {
        public const string AZURE_CONCURRENCY_VIOLATION = @"<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>
<error xmlns=""http://schemas.microsoft.com/ado/2007/08/dataservices/metadata"">
  <code>UpdateConditionNotSatisfied</code>
  <message xml:lang=""en-US"">The update condition specified in the request was not satisfied.
RequestId:9422ce64-90f0-43ca-a7e9-db19955fbec9
Time:2012-12-07T14:08:26.9275938Z</message>
</error>";
    }
}