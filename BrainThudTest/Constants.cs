using System;
using BrainThud.Core;
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
        public static readonly DateTime CARD_QUIZ_DATE = new DateTime(2013, 3, 25);
        public static readonly string CONFIGURATION_ROW_KEY = CardRowTypes.CONFIGURATION + '-' + CONFIGURATION_ID;
        // ReSharper restore InconsistentNaming

        public const int INT = 5;
        public const string STRING = "TestString";
        public const string DATETIME_STRING = "2012-07-21T00:00:00-05:00";
        public const int USER_ID = 21;
        public const int CARD_ID = 583;
        public const int QUIZ_ID = 37;
        public const int QUIZ_RESULT_ID = 32;
        public const int CARD_QUIZ_LEVEL = 3;
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
        public const string IDENTITY_PROVIDER_RESPONSE = @"[{'Name':'Windows Live™ ID','LoginUrl':'https://login.live.com/login.srf?wa=wsignin1.0&wtrealm=https%3a%2f%2faccesscontrol.windows.net%2f&wreply=https%3a%2f%2fbrainthud.accesscontrol.windows.net%2fv2%2fwsfederation&wp=MBI_FED_SSL&wctx=cHI9d3NmZWRlcmF0aW9uJnJtPWh0dHAlM2ElMmYlMmZhdXRoZW50aWNhdGlvbi5icmFpbnRodWQuY29tJTJm0','LogoutUrl':'https://login.live.com/login.srf?wa=wsignout1.0','ImageUrl':'http://brainthud.blob.core.windows.net/site/hotmail.png','EmailAddressSuffixes':[]},{'Name':'Google','LoginUrl':'https://www.google.com/accounts/o8/ud?openid.ns=http%3a%2f%2fspecs.openid.net%2fauth%2f2.0&openid.mode=checkid_setup&openid.claimed_id=http%3a%2f%2fspecs.openid.net%2fauth%2f2.0%2fidentifier_select&openid.identity=http%3a%2f%2fspecs.openid.net%2fauth%2f2.0%2fidentifier_select&openid.realm=https%3a%2f%2fbrainthud.accesscontrol.windows.net%3a443%2fv2%2fopenid&openid.return_to=https%3a%2f%2fbrainthud.accesscontrol.windows.net%3a443%2fv2%2fopenid%3fcontext%3dcHI9d3NmZWRlcmF0aW9uJnJtPWh0dHAlM2ElMmYlMmZhdXRoZW50aWNhdGlvbi5icmFpbnRodWQuY29tJTJmJnByb3ZpZGVyPUdvb2dsZQ2&openid.ns.ax=http%3a%2f%2fopenid.net%2fsrv%2fax%2f1.0&openid.ax.mode=fetch_request&openid.ax.required=email%2cfullname%2cfirstname%2clastname&openid.ax.type.email=http%3a%2f%2faxschema.org%2fcontact%2femail&openid.ax.type.fullname=http%3a%2f%2faxschema.org%2fnamePerson&openid.ax.type.firstname=http%3a%2f%2faxschema.org%2fnamePerson%2ffirst&openid.ax.type.lastname=http%3a%2f%2faxschema.org%2fnamePerson%2flast','LogoutUrl':'','ImageUrl':'http://brainthud.blob.core.windows.net/site/google.png','EmailAddressSuffixes':[]},{'Name':'Yahoo!','LoginUrl':'https://open.login.yahooapis.com/openid/op/auth?openid.ns=http%3a%2f%2fspecs.openid.net%2fauth%2f2.0&openid.mode=checkid_setup&openid.claimed_id=http%3a%2f%2fspecs.openid.net%2fauth%2f2.0%2fidentifier_select&openid.identity=http%3a%2f%2fspecs.openid.net%2fauth%2f2.0%2fidentifier_select&openid.realm=https%3a%2f%2fbrainthud.accesscontrol.windows.net%3a443%2fv2%2fopenid&openid.return_to=https%3a%2f%2fbrainthud.accesscontrol.windows.net%3a443%2fv2%2fopenid%3fcontext%3dcHI9d3NmZWRlcmF0aW9uJnJtPWh0dHAlM2ElMmYlMmZhdXRoZW50aWNhdGlvbi5icmFpbnRodWQuY29tJTJmJnByb3ZpZGVyPVlhaG9vIQ2&openid.ns.ax=http%3a%2f%2fopenid.net%2fsrv%2fax%2f1.0&openid.ax.mode=fetch_request&openid.ax.required=email%2cfullname%2cfirstname%2clastname&openid.ax.type.email=http%3a%2f%2faxschema.org%2fcontact%2femail&openid.ax.type.fullname=http%3a%2f%2faxschema.org%2fnamePerson&openid.ax.type.firstname=http%3a%2f%2faxschema.org%2fnamePerson%2ffirst&openid.ax.type.lastname=http%3a%2f%2faxschema.org%2fnamePerson%2flast','LogoutUrl':'','ImageUrl':'http://brainthud.blob.core.windows.net/site/yahoo.png','EmailAddressSuffixes':[]},{'Name':'Facebook','LoginUrl':'https://www.facebook.com/dialog/oauth?client_id=421178057963963&redirect_uri=https%3a%2f%2fbrainthud.accesscontrol.windows.net%2fv2%2ffacebook%3fcx%3dcHI9d3NmZWRlcmF0aW9uJnJtPWh0dHAlM2ElMmYlMmZhdXRoZW50aWNhdGlvbi5icmFpbnRodWQuY29tJTJmJmlwPUZhY2Vib29rLTQyMTE3ODA1Nzk2Mzk2Mw2&scope=email','LogoutUrl':'','ImageUrl':'http://brainthud.blob.core.windows.net/site/facebook.png','EmailAddressSuffixes':[]}]";
        public const string FED_AUTH_COOKIE_KEY = "FedAuth";
        public const string FED_AUTH_COOKIE_VALUE = "DA4LTQ4ZmQtOWMzMi05MmJmYzQ0YWY4MTgtMDdFMUJDRTk3NzU1N0VBMTE3NDA1NUEyOTBC";
        public const string FED_AUTH_1_COOKIE_KEY = "FedAuth1";
        public const string FED_AUTH_1_COOKIE_VALUE = "VjdXJpdHlDb250ZXh0VG9rZW4gc";
        public const string FED_AUTH_TOKEN = FED_AUTH_COOKIE_KEY + "=" + FED_AUTH_COOKIE_VALUE + ";" + FED_AUTH_1_COOKIE_KEY + "=" + FED_AUTH_1_COOKIE_VALUE + ";";
    }

    public static class TestUrls
    {
        public const string CARDS = "http://localhost/api/cards";
        public const string CONFIG = "http://localhost/api/config";
        public const string QUIZ_RESULTS = "http://localhost/api/quiz-results/{0}/{1}/{2}/{3}/{4}";
        public const string LOCALHOST = "http://localhost/";
        public const string FEDERATION_CALLBACK = "http://localhost/api/federationcallback/";
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