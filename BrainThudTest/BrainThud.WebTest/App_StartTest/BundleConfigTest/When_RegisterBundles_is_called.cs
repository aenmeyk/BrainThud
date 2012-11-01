using System.IO;
using System.Linq;
using System.Web.Optimization;
using BrainThud.Web;
using BrainThud.Web.App_Start;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.App_StartTest.BundleConfigTest
{
    [TestFixture]
    public class When_RegisterBundles_is_called : Given_an_empty_bundle_collection
    {
        public override void When()
        {
            BundleTable.MapPathMethod = s => Path.GetTempPath();
            BundleConfig.RegisterBundles(this.BundleCollection);
        }

        [Test]
        public void Then_modernizr_should_be_added_as_a_bundle()
        {
            this.BundleCollection.Should().Contain(x => x.Path == BundlePaths.MODERNIZR);
        }

        [Test]
        public void Then_jquery_should_be_added_as_a_bundle()
        {
            this.BundleCollection.Should().Contain(x => x.Path == BundlePaths.JQUERY);
        }

        [Test]
        public void Then_the_jquery_bundle_should_use_a_cdn()
        {
            var bundle = this.BundleCollection.First(x => x.Path == BundlePaths.JQUERY);
            bundle.CdnPath.Should().NotBeBlank();
        }

        [Test]
        public void Then_jquery_ui_should_be_added_as_a_bundle()
        {
            this.BundleCollection.Should().Contain(x => x.Path == BundlePaths.JQUERY_UI);
        }

        [Test]
        public void Then_the_jquery_ui_bundle_should_use_a_cdn()
        {
            var bundle = this.BundleCollection.First(x => x.Path == BundlePaths.JQUERY_UI);
            bundle.CdnPath.Should().NotBeBlank();
        }

        [Test]
        public void Then_a_bundle_should_be_created_for_external_libraries()
        {
            this.BundleCollection.Should().Contain(x => x.Path == BundlePaths.EXTERNAL_LIBS);
        }

        [Test]
        public void Then_a_bundle_should_be_created_for_application_libraries()
        {
            this.BundleCollection.Should().Contain(x => x.Path == BundlePaths.APP_LIBS);
        }

        [Test]
        public void Then_a_bundle_should_be_created_for_css()
        {
            this.BundleCollection.Should().Contain(x => x.Path == BundlePaths.CSS);
        }
    }
}