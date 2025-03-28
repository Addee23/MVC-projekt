using Microsoft.Data.SqlClient; //för SqlConnectionStringBuilder
using Microsoft.EntityFrameworkCore; //för AddDbContext, UseSqlServer metod
using Microsoft.Extensions.DependencyInjection; //för IServiceCollection
using Microsoft.Extensions.Options;
using Northwind.DataContext.SqlServer; //för NorthwindDatabaseContextLogger
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.EntityModels
{
    public static class NorthwindContextExtensions
    {
        /// <summary>
        /// Extension method för att lägga till NorthwindDatabaseContext i Dependency Injection Container
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="connectionString">Läggs till för att override default</param>
        /// <returns>En IServiceCollection som kan användas för att lägga till mer tjänster </returns>
        public static IServiceCollection AddNorthwindContext(
            this IServiceCollection services, //typen som extends
            string? connectionString = null)
        {
            if (connectionString is null)
            {
                SqlConnectionStringBuilder builder = new();

                builder.DataSource = "(localdb)\\MSSQLLocalDB"; //ServerName/InstanceName sqllocaldb info
                builder.InitialCatalog = "NorthwindDatabase";
                builder.TrustServerCertificate = true;
                builder.MultipleActiveResultSets = true;

                //visar timeout i 3 sekunder, default är 15 sekunder
                builder.ConnectTimeout = 3;

                //om ni vill använda Windows Authentication
                builder.IntegratedSecurity = true;

                //om ni vill använda SQL Server Authentication
                //builder.UserID = Environment.GetEnvironmentVariable("MY_SQL_USR");
                //builder.Password = Environment.GetEnvironmentVariable("MY_SQL_PWD");

                connectionString = builder.ConnectionString;
            }
            services.AddDbContext<NorthwindDatabaseContext>(options =>
            {
                options.UseSqlServer(connectionString);

                options.LogTo(NorthwindContextLogger.WriteLine,
                new[] { Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuting });
            },
            //Registrera NorthwindDatabaseContext med service lifetime Transient för att undvika problem med DbContext concurrency i Blazor Server projects
            contextLifetime: ServiceLifetime.Transient,
            optionsLifetime: ServiceLifetime.Transient);

            return services;
        }
    }
}
