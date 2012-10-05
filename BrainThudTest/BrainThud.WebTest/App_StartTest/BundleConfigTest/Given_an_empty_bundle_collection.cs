using System.Web.Optimization;
using BrainThudTest.Tools;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.App_StartTest.BundleConfigTest
{
    [TestFixture]
    public abstract class Given_an_empty_bundle_collection : Gwt
    {
        public override void Given()
        {
            this.BundleCollection = new BundleCollection();
        }

        protected BundleCollection BundleCollection { get; set; }
    }
}