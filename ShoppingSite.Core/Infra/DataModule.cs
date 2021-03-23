using Autofac;
using ShoppingSite.Models;
using ShoppingSite.RepositoryInfra;
using ShoppingSite.RepositoryService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.Infra
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(_ => new ApplicationDbContext()).As(typeof(DbContext)).InstancePerRequest();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            base.Load(builder);
        }

    }
}
