using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebChat.Entity.v1.models;

namespace Entity.v1.models
{
    public partial class ChartContext : DbContext
    {
        public ChartContext()
        {
        }

        public ChartContext(DbContextOptions<ChartContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserLogin> UserLogin { get; set; }

        public virtual DbSet<VoteInfo> VoteInfo { get; set; }
        public virtual DbSet<VoteDetail> VoteDetail { get; set; }
        public virtual DbSet<UserVote> UserVote { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDb;database=Chart;AttachDbFilename=C:\\data\\chart.mdf;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.LoginName)
                    .HasColumnName("loginName")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MobilePhone)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.UserName).HasMaxLength(128);
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.LoginStatus).HasMaxLength(128);

                entity.Property(e => e.LoginTime).HasColumnType("datetime");
            });


            modelBuilder.Entity<VoteInfo>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.CreateUserName).IsUnicode(true).HasMaxLength(128);
                entity.Property(e => e.Title).IsUnicode(true).HasMaxLength(128);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");
            });


            modelBuilder.Entity<VoteDetail>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ItemTitle).HasMaxLength(128);
            });


            modelBuilder.Entity<UserVote>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");
            });
        }
    }
}
