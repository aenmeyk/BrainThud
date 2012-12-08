
namespace BrainThudTest
{
    public abstract class GwtAzureEmulator : Gwt
    {
        public override void SubInitialize()
        {
            new AzureInitializer().Initialize();
            base.SubInitialize();
        }
    }
}