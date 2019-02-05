using Myntra.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Myntra.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized = false;

        public InitializeSimpleMembershipAttribute()
        {
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<UsersContext>(null);

                try
                {
                    using (var dbContext = new UsersContext())
                    {
                        if (!dbContext.Database.Exists())
                        {
                            ((IObjectContextAdapter)dbContext).ObjectContext.CreateDatabase();
                        }
                    }

                    WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", true);
                }
                catch(Exception ex)
                {
                    throw new Exception("Asp net simple membership cannot be initialized using WebMatrix class library", ex);
                }
            }
        }
    }
}