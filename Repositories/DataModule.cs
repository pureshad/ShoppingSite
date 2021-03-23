using Autofac;
using Autofac.Core;
using ShoppingSite.Models;
using System;

namespace Repositories
{
    internal class DataModule : Module
    {
        private readonly string _connStr;

        public DataModule(string connStr)
        {
            _connStr = connStr ?? throw new ArgumentNullException(nameof(connStr));
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new ApplicationDbContext(_connStr)).InstancePerRequest();
            base.Load(builder);
        }
    }
}