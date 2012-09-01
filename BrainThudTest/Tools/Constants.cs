using System;

namespace BrainThudTest.Tools
{
    public static class TestTypes
    {
        public const string INTEGRATION = "Integration";
    }

    public static class TestValues
    {
        // ReSharper disable InconsistentNaming
        public static readonly Guid GUID = Guid.Parse("16ffa7cb-53f7-4a1d-bf19-c60555c95fcf");
        // ReSharper restore InconsistentNaming

        public const string STRING = "TestString";
        public const string PARTITION_KEY = "c86258da-9165-4e19-906a-4441bd298d71";
        public const string ROW_KEY = "6037e998-c399-4153-9353-00ae5e6ea1e9";
    }
}