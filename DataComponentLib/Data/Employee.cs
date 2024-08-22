using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataComponentLib.Data
{
    [Table("tblEmployee")]
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }
        [Required]
        public string EmpName { get; set; } = string.Empty;
        [Required]
        public string EmpAddress { get; set; } = string.Empty;
        [Required]
        public int EmpSalary { get; set; }
    }

    public class EmpDbContext : DbContext
    {
        public EmpDbContext()
        {
            
        }
        public EmpDbContext(DbContextOptions<EmpDbContext>options) : base(options)
        {
                
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory()) // Set the base path to current directory
                        .AddJsonFile("dbSettings.json") // Add the appsettings.json file
                        .Build();
                //U should make sure the dbSettings.json file is placed in the executing Directory of the Application. 
                string connectionString = configuration.GetConnectionString("myConnection")!;
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
