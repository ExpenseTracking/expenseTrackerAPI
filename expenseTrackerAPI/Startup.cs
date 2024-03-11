using expenseTrackerAPI.Repositories;
using expenseTrackerAPI.Services;
using Microsoft.OpenApi.Models;

namespace expenseTrackerAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var conn = Configuration.GetValue<string>("ConnectionStrings:SqlConnection");

            services.AddControllers();
            // services.AddResponseCaching();
            services.AddSwaggerGen();
            
            // add JWT Auth

            // add CORS

            // add common logger

            // add metrics factory

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IUserRolesService, UserRolesService>();
            services.AddScoped<ITransactionTypeService, TransactionTypeService>();
            services.AddScoped<IIncomeService, IncomeService>();
            services.AddSingleton<IUserRepository>(p => new UserRepository(conn));
            services.AddSingleton<IExpenseRepository>(p => new ExpenseRepository(conn));
            services.AddSingleton<IUserRolesRepository>(p => new UserRolesRepository(conn));
            services.AddSingleton<ITransactionTypeRepository>(p => new TransactionTypeRepository(conn));
            services.AddSingleton<IIncomeRepository>(p => new IncomeRepository(conn));
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Expense Tracker API",
                    Version = "v1",
                    Description = "Group project for CSCI 4700/5700 with Dr.Sarkar"  
                });
                // add security
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "expenseTrackerAPI v1"));
            }

            app.UseHttpsRedirection();
            // app.UseResponseCaching();
            app.UseRouting();
            // app.UseAuthentication();
            // app.UseAuthorization();
            // app.UseCors("CORSPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}