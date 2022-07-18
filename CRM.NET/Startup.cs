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
                // ��ʽ��Json���� ʹ��Ĭ��System.text.json
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;

                    // Ĭ�ϴ���Jsonѭ������ -- �����е���ʹ��[JsonIgnore]������ֹѭ������
                    //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                });

            // ʹ��SqlServer Ĭ����������ΪScoped
            // ����ef core����֧��Sqlserver2008��ҳģʽ ����ʹ��msql�����ݿ�
            // Sqlserv2012֮�� ֧��ʹ��OFFSET��FETCH��ҳ ef core����Ż�����
            services.AddDbContext<CRMDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("CRMDB"));
            },ServiceLifetime.Scoped);

            // ����AutoMapper
            services.AddAutoMapper(typeof(AutoMapperConfig));

            // ����Quartz ע��Job
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

            #region ����CORS����
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
                        ValidateIssuer = true,//�Ƿ���֤Issuer
                        ValidateAudience = true,//�Ƿ���֤Audience
                        ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
                        ClockSkew = TimeSpan.FromSeconds(30),
                        ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                        ValidAudience = ValidAudience,//Audience
                        ValidIssuer = ValidIssuer,//Issuer���������ǰ��ǩ��jwt������һ��
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey))//�õ�SecurityKey
                    };
                });
            #endregion

            #region NSwag + JWT
            services.AddOpenApiDocument(config =>
            {
                config.Title = "CRM.NET";
                config.Description = "CRM.NET��ĿAPI�ĵ�";
                config.Version = "1.0";

                //�������ô�ע���ļ����أ����Ǽ��ص����ݿɱ�OpenApiTagAttribute���Ը���
                config.UseControllerSummaryAsTagDescription = true;

                //����JwtBearer��֤
                config.AddSecurity("JwtBearer", Enumerable.Empty<string>(), new OpenApiSecurityScheme()
                {
                    Description = "ֱ�����������������֤��Ϣ������Ҫ�ڿ�ͷ���Bearer",
                    Name = "Authorization",//jwtĬ�ϵĲ�������
                    In = OpenApiSecurityApiKeyLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
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
                // ��˿�����
                app.UseCors("any");
            }

            app.UseCors("pro");

            #region token��֤
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
