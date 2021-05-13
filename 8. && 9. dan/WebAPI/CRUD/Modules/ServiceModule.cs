using Autofac;
using Services;
using Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<CityService>().As<ICityService>();
            builder.RegisterType<PlayerService>().As<IPlayerService>();
        }
    }
}