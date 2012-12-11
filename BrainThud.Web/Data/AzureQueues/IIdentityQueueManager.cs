namespace BrainThud.Web.Data.AzureQueues
{
    public interface IIdentityQueueManager
    {
        void Seed();
        int GetNextIdentity();
    }
}