using Autofac;
using Autofac.Integration.Mvc;
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

            //告诉Autofac框架，将来要创建的控制器类存放在哪个程序集 (UI),从当前运行的bin目录下加载程序集
            Assembly controllerAss = Assembly.Load("My.VideosProject");
            builder.RegisterControllers(controllerAss);

            //注入BLL，UI中使用
            //告诉autofac框架注册数据仓储层所在程序集中的所有类的对象实例
            Assembly respAss = Assembly.Load("My.Videos.DAL");
            //以接口形式保存被创建类的对象实例
            builder.RegisterTypes(respAss.GetTypes()).AsImplementedInterfaces();

            //告诉autofac框架注册业务逻辑层所在程序集中的所有类的对象实例
            Assembly serpAss = Assembly.Load("My.Videos.BLL");
            //以接口形式保存被创建类的对象实例
            builder.RegisterTypes(serpAss.GetTypes()).AsImplementedInterfaces();

            //builder.RegisterType<LogDService>().As<ILogDService>().InstancePerLifetimeScope();
            //builder.RegisterType<MenuService>().As<IMenuService>().InstancePerLifetimeScope();
            //builder.RegisterType<MenuRelationRoleService>().As<IMenuRelationRoleService>().InstancePerLifetimeScope();
            //builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
            //builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            //builder.RegisterType<UserRelationMenuService>().As<IUserRelationMenuService>().InstancePerLifetimeScope();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //EngineContainerFactory.InitializeEngineContainerFactory(new EngineContainer(container));
        }
    }
}