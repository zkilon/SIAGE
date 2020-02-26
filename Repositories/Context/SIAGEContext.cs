using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Models.Entities;

namespace Repositories.Context
{
	public class SIAGEContext : DbContext
	{
		public SIAGEContext(DbContextOptions<SIAGEContext> options)
			: base(options)
		{

		}

		public DbSet<Usuario> Usuarios { get; set; }
	}

	public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SIAGEContext>
	{
		public SIAGEContext CreateDbContext(string[] args)
		{
			IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + " /appsettings.json").Build();
			var builder = new DbContextOptionsBuilder<SIAGEContext>();
			var connectionString = configuration.GetConnectionString("SIAGEContext");
			builder.UseSqlServer(connectionString);
			return new SIAGEContext(builder.Options);
		}
	}
}
