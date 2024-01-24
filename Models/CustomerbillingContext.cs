using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Stock_Invoice.Models;

public partial class CustomerbillingContext : DbContext
{
    public CustomerbillingContext()
    {
    }

    public CustomerbillingContext(DbContextOptions<CustomerbillingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customerdetail> Customerdetails { get; set; }

    public virtual DbSet<ProductItem> ProductItems { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=10.1.193.167;Initial Catalog=customerbilling;Persist Security Info=False;User ID=sa;Password=sql@123;Pooling=False;Multiple Active Result Sets=False;Encrypt=False;Trust Server Certificate=False;Command Timeout=0");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customerdetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("customerdetails");

            entity.HasIndex(e => e.Userid, "UQ__customer__CBA1B256451B2DE5").IsUnique();

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<ProductItem>(entity =>
        {
            entity.HasKey(e => e.PId).HasName("pk_id");

            entity.ToTable("productItems");

            entity.Property(e => e.PId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("p_id");
            entity.Property(e => e.Cgst)
                .HasColumnType("money")
                .HasColumnName("cgst");
            entity.Property(e => e.ExpDate)
                .HasColumnType("datetime")
                .HasColumnName("Exp_Date");
            entity.Property(e => e.PName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("p_name");
            entity.Property(e => e.PPrice)
                .HasColumnType("money")
                .HasColumnName("p_price");
            entity.Property(e => e.PQuantity).HasColumnName("p_quantity");
            entity.Property(e => e.Sgst)
                .HasColumnType("money")
                .HasColumnName("sgst");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("userDetails");

            entity.Property(e => e.Pwd)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("pwd");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
