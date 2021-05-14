using Autofac;
using Models;
using Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD.Modules
{
    public class ModelsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<City>().As<ICity>();
            builder.RegisterType<Player>().As<IPlayer>();
        }
    }
}