using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SimpelBlog.Logging;
using SimpelBlog.Model;
using SimpelBlog.Services;
using SimpleBlog.Services;

namespace WebApp
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

            services.AddControllers().AddNewtonsoftJson();
            services.AddControllersWithViews();
            services.RegisterDataService(Configuration);
            //DI

            services.AddScoped<IValidation,LoginService>();
            services.AddScoped<ISimpelLogger,SimpelLogger>();
            services.AddScoped<IMarkdown,MarkdownService>();
            services.AddScoped<IPost,PostService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<UserManager<User>,UserManager<User>>();
            services.AddIdentity<User,IdentityRole<Guid>>()
                .AddEntityFrameworkStores<BlogContext>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                        {
                            options.RequireHttpsMetadata = false;
                            options.SaveToken = true;
                            options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = Configuration["Jwt:Issuer"],
                            ValidAudience = Configuration["Jwt:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
                            ClockSkew = TimeSpan.Zero
                        };
                        options.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = context =>
                            {
                                context.Token = context.Request.Cookies["Auth"];
                                return Task.CompletedTask;
                            },
                        };
                        });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider,IUserService userService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                    {
                    endpoints.MapControllerRoute(
                            name: "default",
                            pattern: "{controller=Home}/{action=Index}/{id?}");
                    });
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<BlogContext>();
                context.Database.EnsureCreated();
                var roles = Configuration.GetSection("initRoles:Roles").Get<List<string>>();
                var roleManager =  serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
                foreach(var roleName in roles)
                {
                    if(!roleManager.RoleExistsAsync(roleName).Result)
                    {
                        var roleToCreate = new IdentityRole<Guid>();
                        roleToCreate.Name = roleName;
                        roleManager.CreateAsync(roleToCreate).Wait();

                    }
                }
            }

            if(userService.GetAllUsers().Count == 0)
            {
            
                var userToCreate = new User 
                {
                    UserName = "Admin"
                };
                userService.CreateUser(userToCreate, Configuration["Admin"]);
                var user = userService.GetUserByUsername("Admin");
                userService.AddRolesToUser(user, new List<string>(){"Admin"});
                Configuration["Admin"] = "lol";
            }

        }
    }
}
