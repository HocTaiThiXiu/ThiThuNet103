using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ThiThuNet103.Models;

namespace ThiThuNet103.Models;

public partial class FinalNet103Context : DbContext
{
    public FinalNet103Context()
    {
    }

    public FinalNet103Context(DbContextOptions<FinalNet103Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Ca> Cas { get; set; }

    public virtual DbSet<Dongvat> Dongvats { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DYLISSS;Database=FINAL_NET103;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ca>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ca__3214EC27A26E0E95");

            entity.ToTable("Ca");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idca).HasColumnName("IDCa");
            entity.Property(e => e.TapTinh).HasMaxLength(100);
            entity.Property(e => e.Ten).HasMaxLength(100);
            entity.Property(e => e.ThucAn).HasMaxLength(100);

            entity.HasOne(d => d.IdcaNavigation).WithMany(p => p.InverseIdcaNavigation)
                .HasForeignKey(d => d.Idca)
                .HasConstraintName("FK_CA_DV");
        });

        modelBuilder.Entity<Dongvat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Dongvat__3214EC2715FA6E2F");

            entity.ToTable("Dongvat");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Noisong).HasMaxLength(100);
            entity.Property(e => e.TuoithoTb).HasColumnName("TuoithoTB");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<ThiThuNet103.Models.CaViewModel> CaViewModel { get; set; } = default!;
}
