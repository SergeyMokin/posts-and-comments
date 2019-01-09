using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PostsAndComments.Controllers;
using PostsAndComments.Filters;
using PostsAndCommentsInterfaces.Controllers;
using PostsAndCommentsInterfaces.Repositories;
using PostsAndCommentsInterfaces.Services;
using PostsAndCommentsModels;
using PostsAndCommentsModels.DatabaseModels;
using PostsAndCommentsRepositories.DBContext;
using PostsAndCommentsRepositories.Repositories;
using PostsAndCommentsServices.Services;

namespace PostsAndComments
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Specifies whether the publisher will validate when validating the token.
                        ValidateIssuer = false,
                        // Will the token consumer be validated.
                        ValidateAudience = false,
                        // Will the lifetime be validated.
                        ValidateLifetime = true,
                        // Set the security key.
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        // Validate the security key.
                        ValidateIssuerSigningKey = true
                    };
                });

            services.AddMvc(options =>
                options.Filters.Add(new ControllerExceptionFilterAttribute()));

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("MyPolicy"));
            });

            BuildAppSettingsProvider();

            RegisterDependencyInjection(services);
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseCors("MyPolicy");

            app.UseMvc();
        }

        private void RegisterDependencyInjection(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IRepository<Image>, Repository<Image>>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Comment>, Repository<Comment>>();
            services.AddScoped<IRepository<Like>, Repository<Like>>();
            services.AddScoped<IRepository<Post>, Repository<Post>>();

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IPostService, PostService>();

            services.AddScoped<IUserController, UserController>();
            services.AddScoped<ICommentController, CommentController>();
            services.AddScoped<IPostController, PostController>();

            services.AddDbContext<PacDbContext>(options =>
                options.UseSqlServer(AppSettings.DbConnectionString));
        }

        private void BuildAppSettingsProvider()
        {
            AppSettings.DbConnectionString = Configuration["AppSettings:DbConnectionString"];
            AppSettings.SenderMail = Configuration["AppSettings:SenderMail"];
            AppSettings.SenderPass = Configuration["AppSettings:SenderPass"];
        }
    }
}
