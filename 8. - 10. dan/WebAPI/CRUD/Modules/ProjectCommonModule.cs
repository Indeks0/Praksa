using Autofac;
using Project.Common.Interfaces;
using Project.Common.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD.Modules
{
    public class ProjectCommonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<Filtering>().As<IFiltering>();
            builder.RegisterType<Sorting>().As<ISorting>();
            builder.RegisterType<Paging>().As<IPaging>();
            builder.RegisterType<CustomDBQuery>().As<ICustomDBQuery>();
        }
    }
}