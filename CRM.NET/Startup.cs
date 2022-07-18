using Autofac;
using CRM.NET.Quartz;
using IRepository;
using IService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Model;
using NSwag;
using Quartz;
using Repository;
using Service;
using System;
using System.Linq;
using System.Text;

namespace CRM.NET
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers()
                // 格式化Json数据 使用默认System.text.json
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;

                    // 默认处理Json循环引用 -- 工程中单向使用[JsonIgnore]处理，防止循环引用
                    //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                });

            // 使用SqlServer 默认生命周期为Scoped
            // 最新ef core不再支持Sqlserver2008分页模式 建议使用msql等数据库
            // Sqlserv2012之后 支持使用OFFSET和FETCH分页 ef core语句优化报错
            services.AddDbContext<CRMDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("CRMDB"));
            },ServiceLifetime.Scoped);

            // 配置AutoMapper
            services.AddAutoMapper(typeof(AutoMapperConfig));

            // 配置Quartz 注入Job
            services.AddSingleton<RegularWork>();
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
                var jobkey = new JobKey("RegularWork");
                q.AddJob<RegularWork>(jobkey, j => j.WithDescription("RegularWork"));
                q.AddTrigger(t => t
                    .WithIdentity("Trigger")
                    .ForJob(jobkey)
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromSeconds(20)).RepeatForever())
                    .WithDescription("RegularWork Trigger")
                );
            });
            services.AddQuartzHostedService(option => {
                option.WaitForJobsToComplete = true;
            });

            #region 配置CORS跨域
            services.AddCors(options =>
            {
                options.AddPolicy("any", options =>
                {
                    options.AllowAnyOrigin();
                    options.AllowAnyHeader();
                    options.AllowAnyMethod();
                    options.AllowAnyHeader();
                });
                options.AddPolicy("pro", options =>
                {
                    options.WithOrigins("http://localhost:8000", "http://127.0.0.1:8000");
                    options.AllowAnyHeader();
                    options.AllowAnyMethod();
                    options.AllowAnyHeader();
                });
            });
            #endregion


            #region JWT
            var SecurityKey = Configuration.GetSection("Token")["Secret"];
            var ValidAudience = Configuration.GetSection("Token")["Audience"];
            var ValidIssuer = Configuration.GetSection("Token")["Issuer"];
            services.AddAuthentication(
                options=> {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,//是否验证Issuer
                        ValidateAudience = true,//是否验证Audience
                        ValidateLifetime = true,//是否验证失效时间
                        ClockSkew = TimeSpan.FromSeconds(30),
                        ValidateIssuerSigningKey = true,//是否验证SecurityKey
                        ValidAudience = ValidAudience,//Audience
                        ValidIssuer = ValidIssuer,//Issuer，这两项和前面签发jwt的设置一致
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey))//拿到SecurityKey
                    };
                });
            #endregion

            #region NSwag + JWT
            services.AddOpenApiDocument(config =>
            {
                config.Title = "CRM.NET";
                config.Description = "CRM.NET项目API文档";
                config.Version = "1.0";

                //可以设置从注释文件加载，但是加载的内容可悲OpenApiTagAttribute特性覆盖
                config.UseControllerSummaryAsTagDescription = true;

                //定义JwtBearer认证
                config.AddSecurity("JwtBearer", Enumerable.Empty<string>(), new OpenApiSecurityScheme()
                {
                    Description = "直接在输入框中输入认证信息，不需要在开头添加Bearer",
                    Name = "Authorization",//jwt默认的参数名称
                    In = OpenApiSecurityApiKeyLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = OpenApiSecuritySchemeType.Http,
                    Scheme = "bearer"
                });
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // 后端跨域解决
                app.UseCors("any");
            }

            app.UseCors("pro");

            #region token验证
            app.UseAuthentication();
            #endregion

            #region NSwag UI
            app.UseOpenApi();
            app.UseSwaggerUi3();
            #endregion

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>));
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<SaleChanceRepository>().As<ISaleChanceRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SaleChanceService>().As<ISaleChanceService>().InstancePerLifetimeScope();
            builder.RegisterType<CusDevPlanRepository>().As<ICusDevPlanRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CusDevPlanService>().As<ICusDevPlanService>().InstancePerLifetimeScope();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
            builder.RegisterType<ModuleRepository>().As<IModuleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ModuleService>().As<IModuleService>().InstancePerLifetimeScope();
            builder.RegisterType<CusLossRepository>().As<ICusLossRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CusLossService>().As<ICusLossService>().InstancePerLifetimeScope();
            builder.RegisterType<CusReprieveRespository>().As<ICusReprieveRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CusReprieveService>().As<ICusReprieveService>().InstancePerLifetimeScope();
            builder.RegisterType<CusServerRepository>().As<ICusServerRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CusServerService>().As<ICusServerService>().InstancePerLifetimeScope();
            builder.RegisterType<TaskService>().As<ITaskService>().SingleInstance();
        }
    }
}
