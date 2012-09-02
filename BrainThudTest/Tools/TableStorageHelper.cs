using System.Collections.Generic;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThudTest.Tools
{
    public static class TableStorageHelper
    {
        public static void ClearTable<T>(TableServiceContext tableServiceContext)
        {
            foreach (var nugget in tableServiceContext.CreateQuery<T>(typeof(T).Name))
            {
                tableServiceContext.DeleteObject(nugget);
            }

            tableServiceContext.SaveChangesWithRetries();
        }
    }
}