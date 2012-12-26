namespace BrainThud.Web.Data.AzureQueues
{
    public interface IIdentityQueueManager
    {
        int GetNextIdentity();
    }
}