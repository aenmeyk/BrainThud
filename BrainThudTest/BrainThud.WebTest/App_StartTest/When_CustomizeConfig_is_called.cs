using System.Web.Http;
using BrainThud.Web.ActionFilters;
using BrainThud.Web.App_Start;
using FluentAssertions;
using NUnit.Framework;
using Newtonsoft.Json.Serialization;

namespace BrainThudTest.BrainThud.WebTest.App_StartTest
{
    [TestFixture]
    public class When_CustomizeConfig_is_called : Given_a_GlobalConfig_class
    {
        private HttpConfiguration config;

        public override void When()
        {
            this.config = new HttpConfiguration();
            GlobalConfig.CustomizeConfig(this.config);
        }

        [Test]
        public void Then_the_XmlFormatter_should_be_removed()
        {
            this.config.Formatters.XmlFormatter.Should().BeNull();
        }

        [Test]
        public void Then_the_Json_ContractResolver_should_be_CamelCasePropertyNamesContractResolver()
        {
            this.config.Formatters.JsonFormatter.SerializerSettings.ContractResolver.Should().BeOfType<CamelCasePropertyNamesContractResolver>();
        }

        [Test]
        public void Then_a_ValidationActionFilter_should_be_added_to_the_config()
        {
            this.config.Filters.Should().Contain(x => x.Instance.GetType() == typeof(ValidationActionFilter));
        }
    }
}