using Autofac;
using Autofac.Integration.WebApi;
using Praksa.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Praksa.Service;
using Praksa.Repository;
using Praksa.Repository.Common;
namespace Praksa.WebApi.App_Start
{
    public class DIConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<FootballClubService>().As<IFootballClubService>();
            builder.RegisterType<PlayerService>().As<IPlayerService>();
            builder.RegisterType<FootballClubRepository>().As<IFootballClubRepository>();
            builder.RegisterType<PlayerRepository>().As<IPlayerRepository>();
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}