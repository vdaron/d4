﻿using System;
using System.Threading.Tasks;
using d4.Core.Kernel.Interfaces;
using d4.Sample.Domain.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace d4.Sample.Infrastructure.EFCore
{
    public class SampleContext : DbContext, IUnitOfWork
    {
        public DbSet<Project> Projects { get; set; }

        public SampleContext(DbContextOptions<SampleContext> options) : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var eb = modelBuilder.Entity<Project>();
            eb.UsePropertyAccessMode(PropertyAccessMode.PreferProperty)
                .ToTable("Projects");
            eb.HasKey(p => p.Id);
            eb
                .Property(n => n.Name)
                .HasColumnName("Name")
                .HasConversion(
                    x => x.Value, 
                    x => new ProjectName(x));

        }

        public async Task Commit()
        {
            await SaveChangesAsync();
        }
    }
}