using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Blog.Code.AuthHelper.OverWrite;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace Blog.Code
{
    /// <summary>
    /// Startup类
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// Configuration 属性
        /// </summary>
        public IConfiguration Configuration { get; }


        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// 中间件注入
        /// </summary>
        /// <param name="services"></param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

          //  BaseDBConfig.ConnectionString = Configuration.GetSection("AppSettings:SqlServiceConnection").Value;

            #region  Swagger 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Version = "v.0.1.0",
                    Title = "Blod.Core.API",
                    Description = "框架说明文档",
                    TermsOfService = "None",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact
                    {
                        Name = "Blog.Core",
                        Email = "Blog.Core@xxx.com",
                        Url = "https://www.baidu.com"
                    }
                });

                #region swagger 读取 xml信息

                //获取当前程序执行环境路径
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                //拼接成程序类注释xml文件路径
                var xmlPath = Path.Combine(basePath, "Blog.Code.xml");
                //includeControllerXmlComments  是否保留控制器说明
                c.IncludeXmlComments(xmlPath, true);
                var xmlModelPath = Path.Combine(basePath, "Blog.Core.Model.xml");
                c.IncludeXmlComments(xmlModelPath);
                #endregion

                #region Token绑定到ConfigureServices
                //添加header验证信息
                var security = new Dictionary<string, IEnumerable<string>> { { "Blog.Core", new string[] { } }, };
                c.AddSecurityRequirement(security);
                c.AddSecurityDefinition("Blog.Core", new ApiKeyScheme
                {
                    Description = "JWT授权（数据将在请求头中进行传输）直接在下框中输入{token}\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In="header",//jwt默认存放Authorization信息的位置（请求头中）
                    Type = "apiKey"
                });
                #endregion

            });
            #endregion

            #region Token服务注册  
            //缓存
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });
            //认证
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
                options.AddPolicy("AdminOrClient", policy => policy.RequireRole("AdminOrClient").Build());
            });

            #endregion

            #region AutoFac
            //实例化 AutoFac 容器
            var builder = new ContainerBuilder();
            //注册要通过反射创建的组建
            //builder.RegisterType<AdvertisementServices>().As<IAdvertisementServices>();
            //  var assemblysService = Assembly.Load("Service");
            var basePath1 = PlatformServices.Default.Application.ApplicationBasePath;//获取项目路径
            var serviceDllFile = Path.Combine(basePath1, "Service.dll");//获取注入项目绝对路径
            var assemblysService = Assembly.LoadFile(serviceDllFile);
            //指定已扫描程序集中的类型注册为提供所有其实现的接口。
            builder.RegisterAssemblyTypes(assemblysService).AsImplementedInterfaces();

            var repositoryDllFile = Path.Combine(basePath1, "Repository.dll");//获取注入项目绝对路径
            var assemblysRepository = Assembly.LoadFile(repositoryDllFile);

            //var assemblysRepository = Assembly.Load("Repository");
            builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces();

            //将services 填充 Autofac 容器生成器
            builder.Populate(services);
            //使用已进行的组件登记创建新容器
            var ApplicationContainer = builder.Build();
            #endregion
            // 第三方IOC 接管 core内置DI容器
            return new AutofacServiceProvider(ApplicationContainer);
        }


        /// <summary>
        ///  This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        ///  请求具体处理方式
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                #region Swagger
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                    //c.RoutePrefix = ""; //设置访问该文件的路由前缀，设置为空，表示直接访问该文件
                });
                #endregion
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseMiddleware<JwtTokenAuth>();


            app.UseMvc();
        }
    }
}
