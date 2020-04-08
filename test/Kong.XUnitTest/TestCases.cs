using System;

namespace Kong.XUnitTest
{
    public class TestCases
    {
        public const string SERVICE = "Kong.Test.Service";
        public static Guid SERVICE_ID = Guid.Parse("33eac6dc-88fd-44cc-bcb2-4bb9be74d0ac");

        public const string ROUTE = "Kong.Test";
        public static Guid ROUTE_ID = Guid.Parse("2de59a8a-79f9-4f98-a990-ad14b388eac1");

        public const string PLUGIN_NAME = "rate-limiting";
        public static Guid PLUGIN_ID = Guid.Parse("7052893c-4376-4749-adb3-b2e940fd8e42");

        public const string UPSTREAM_NAME = "Kong.Test";
        public static Guid UPSTREAM_ID = Guid.Parse("9908636e-9665-410e-a4b6-eb4c679ef2fc");

        public const string TARGET = "172.16.10.227:80";
        public const string TAGS = "user-level";
        public const string SNI = "Kong.Test.SNI";
        public static Guid CERTIFICATE = Guid.Parse("a0927e20-5217-4dbd-8fc2-0e61c51cdc3e");

        public const string CONSUMER_USERNAME = "my-username";
        public static Guid CONSUMER_ID = Guid.Parse("ee082b6d-423e-4ea4-8c4a-22cce5ce1d14");
    }
}
