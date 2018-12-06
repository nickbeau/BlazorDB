﻿using System.Threading.Tasks;
using BlazorDB.Storage;

namespace BlazorDB
{
    public class StorageContext : IStorageContext
    {
        protected IStorageManager StorageManager { get; set; }
        public async Task LogToConsole()
        {
            await Logger.StartContextType(GetType(), false);
            var storageSets = StorageManagerUtil.GetStorageSets(GetType());
            foreach (var prop in storageSets)
            {
                var storageSet = prop.GetValue(this);
                var method = storageSet.GetType().GetMethod("LogToConsole");
                method.Invoke(storageSet, new object[]{});
            }
            Logger.EndGroup();
        }

        public Task<int> SaveChanges()
        {
            return StorageManager.SaveContextToLocalStorage(this);
        }

        public Task Initialize()
        {
            return StorageManager.LoadContextFromLocalStorage(this);
        }
    }
}