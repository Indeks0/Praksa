using Autofac;
using Repository;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<CityRepository>().As<ICityRepository>();
            builder.RegisterType<PlayerRepository>().As<IPlayerRepository>();
        }
    }
}