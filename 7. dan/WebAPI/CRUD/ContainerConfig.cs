using Autofac;
using Autofac.Integration.WebApi;
using Models;
using Models.Common;
using Repository;
using Repository.Common;
using Services;
using Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CRUD
{
    public class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<City>().As<ICity>();
            builder.RegisterType<Player>().As<IPlayer>();

            builder.RegisterType<CityService>().As<ICityService>();
            builder.RegisterType<PlayerService>().As<IPlayerService>();

            builder.RegisterType<CityRepository>().As<ICityRepository>();
            builder.RegisterType<PlayerRepository>().As<IPlayerRepository>();

            return builder.Build();
        }
    }
}