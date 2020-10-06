using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyTasks.Database;
using MyTasks.Models;
using MyTasks.Repositories;
using MyTasks.Repositories.Contracts;

namespace MyTasks
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
            services.AddDbContext<MyTasksContext>(o => o.UseSqlite("Data Source=Database\\MyTasks.db"));
            services.AddMvc(o => o.EnableEndpointRouting = false);
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddIdentityCore<ApplicationUser>().AddEntityFrameworkStores<MyTasksContext>();
            services.AddControllers();
            services.AddOptions();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseMvc();
            app.UseAuthorization();
            app.UseAuthentication();
        }
    }
}
