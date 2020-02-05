using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CurrencyCsvParser
{
	public class BittrexCurrencyDbContext : DbContext
	{
		public DbSet<Currency> CurrencyDatas { get; set; }

		public BittrexCurrencyDbContext()
		{
			 Database.EnsureCreated();
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(
				@"Server=(localdb)\mssqllocaldb;Database=BittrexCurrency;Trusted_Connection=True;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Currency>().Property(x => x.BuyPrice).HasColumnType("decimal(16,8)");
			modelBuilder.Entity<Currency>().Property(x => x.SellPrice).HasColumnType("decimal(16,8)");
			modelBuilder.Entity<Currency>().Property(x => x.Low).HasColumnType("decimal(16,8)");
			modelBuilder.Entity<Currency>().Property(x => x.High).HasColumnType("decimal(16,8)");
			modelBuilder.Entity<Currency>().Property(x => x.VolumeBtc).HasColumnType("decimal(16,8)");
			modelBuilder.Entity<Currency>().Property(x => x.VolumeCurrency).HasColumnType("decimal(16,8)");
			
		}
				
		
	}
}
