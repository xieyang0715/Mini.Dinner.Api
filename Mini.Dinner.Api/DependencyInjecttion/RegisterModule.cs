using Autofac;
using Autofac.Core.Registration;
using Microsoft.AspNetCore.Mvc;
using Mini.Dinner.Api.Controllers;
using Mini.Dinner.Services.Common.Impl;
using Mini.Dinner.Services.Common.Interface;
using System.Reflection;

namespace Mini.Dinner.Api.DependencyInjecttion
{
    public class RegisterModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //注入服务
            RegisterTypes(builder, "Mini.Dinner.Services.Common", "Mini.Dinner.Services.Common.Impl", "Service");
            // 注入数据
            RegisterRepository(builder);
            //注入接口
            RegisterController(builder);
        }

        /// <summary>
        /// 添加服务注入类
        /// </summary>
        /// <param name="builder">IOC容器创建者</param>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="startWithStr">待注入类名称限制</param>
        private void RegisterTypes(ContainerBuilder builder, string assemblyName, string startWithStr, string endWith)
        {
            var assembly = Assembly.Load(assemblyName);
            builder.RegisterAssemblyTypes(assembly).Where(t =>
            {
                if (t.Namespace == null || t.Name == null)
                {
                    return false;
                }
                return t.Namespace.StartsWith(startWithStr) && t.Name.EndsWith(endWith);
            }).AsImplementedInterfaces().PropertiesAutowired();
        }
        /// <summary>
        /// 添加仓储和Action类注入
        /// </summary>
        /// <param name="builder">IOC容器创建者</param>
        private void RegisterRepository(ContainerBuilder builder)
        {
            var assembly = Assembly.Load("Mini.Dinner.Dal.Impl");
            builder.RegisterAssemblyTypes(assembly).Where(t =>
            {
                if (t.Namespace == null || t.Name == null)
                {
                    return false;
                }

                if (!(t.Namespace.StartsWith("Mini.Dinner.Dal.Impl.Repositories") && t.Name.EndsWith("Repository")))
                {
                    return false;
                }

                return t.GetInterface("IRepository") != null;
            }).AsImplementedInterfaces().PropertiesAutowired();
        }

        private void RegisterController(ContainerBuilder builder)
        {
            Type[] controllersTypesInAssembly = typeof(Program).Assembly.GetExportedTypes()
               .Where(type => typeof(ControllerBase).IsAssignableFrom(type)).ToArray();
            builder.RegisterTypes(controllersTypesInAssembly).PropertiesAutowired();
        }
    }
}
