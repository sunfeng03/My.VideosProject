using Autofac;
using Autofac.Integration.Mvc;
using My.Videos.BLL;
using My.Videos.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace My.VideosProject.Engines
{
    public class AutoFacBootStrapper
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            #region MVC
            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            #endregion

            builder.RegisterType<LogDService>().As<ILogDService>().InstancePerLifetimeScope();
            builder.RegisterType<MenuService>().As<IMenuService>().InstancePerLifetimeScope();
            builder.RegisterType<MenuRelationRoleService>().As<IMenuRelationRoleService>().InstancePerLifetimeScope();
            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<UserRelationMenuService>().As<IUserRelationMenuService>().InstancePerLifetimeScope();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            EngineContainerFactory.InitializeEngineContainerFactory(new EngineContainer(container));
        }
    }
}