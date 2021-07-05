using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL.models
{
    public partial class CUSERS1DESKTOPDATABASEMARKETINMDFContext : DbContext
    {
        public CUSERS1DESKTOPDATABASEMARKETINMDFContext()
        {
        }

        public CUSERS1DESKTOPDATABASEMARKETINMDFContext(DbContextOptions<CUSERS1DESKTOPDATABASEMARKETINMDFContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Calls> Calls { get; set; }
        public virtual DbSet<Companies> Companies { get; set; }
        public virtual DbSet<Contribution> Contribution { get; set; }
        public virtual DbSet<PhoneNumbers> PhoneNumbers { get; set; }
        public virtual DbSet<Telephonist> Telephonist { get; set; }
        public virtual DbSet<TelephonistInCompanies> TelephonistInCompanies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\1\\Desktop\\DataBaseMarketin.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Calls>(entity =>
            {
                entity.Property(e => e.DateCall).HasColumnType("datetime");

                entity.Property(e => e.Done).HasColumnName("done");

                entity.Property(e => e.TimeCall).HasColumnType("datetime");

                entity.Property(e => e.TranscriptCall).HasMaxLength(100);

                entity.HasOne(d => d.IdPhoneNumberNavigation)
                    .WithMany(p => p.Calls)
                    .HasForeignKey(d => d.IdPhoneNumber)
                    .HasConstraintName("FK_Calls_ToTable");

                entity.HasOne(d => d.IdTelephonistNavigation)
                    .WithMany(p => p.Calls)
                    .HasForeignKey(d => d.IdTelephonist)
                    .HasConstraintName("FK_Calls_ToTable_1");
            });

            modelBuilder.Entity<Companies>(entity =>
            {
                entity.Property(e => e.Mail).HasMaxLength(25);

                entity.Property(e => e.ManagerName).HasMaxLength(20);

                entity.Property(e => e.ManagerPassword).HasMaxLength(15);

                entity.Property(e => e.ManagerTz).HasMaxLength(10);

                entity.Property(e => e.Name).HasMaxLength(20);
            });

            modelBuilder.Entity<Contribution>(entity =>
            {
                entity.ToTable("contribution");

                entity.Property(e => e.DateC)
                    .HasColumnName("dateC")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdPhone).HasColumnName("idPhone");

                entity.Property(e => e.IdTelephonistCompany).HasColumnName("idTelephonistCompany");

                entity.Property(e => e.SumContribution).HasColumnName("sum_contribution");

                entity.HasOne(d => d.IdPhoneNavigation)
                    .WithMany(p => p.Contribution)
                    .HasForeignKey(d => d.IdPhone)
                    .HasConstraintName("FK_contribution_ToTable_1");

                entity.HasOne(d => d.IdTelephonistCompanyNavigation)
                    .WithMany(p => p.Contribution)
                    .HasForeignKey(d => d.IdTelephonistCompany)
                    .HasConstraintName("FK_contribution_ToTable");
            });

            modelBuilder.Entity<PhoneNumbers>(entity =>
            {
                entity.ToTable("Phone_numbers");

                entity.Property(e => e.Address).HasMaxLength(30);

                entity.Property(e => e.City).HasMaxLength(30);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(30);

                entity.Property(e => e.LastName).HasMaxLength(30);

                entity.Property(e => e.Mail).HasMaxLength(30);

                entity.Property(e => e.Mikud)
                    .HasColumnName("mikud")
                    .HasMaxLength(10);

                entity.Property(e => e.Phone).HasMaxLength(10);

                entity.Property(e => e.Phone2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.PlaceWorking).HasMaxLength(20);

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Tz).HasMaxLength(10);

                entity.HasOne(d => d.IdCompaniesNavigation)
                    .WithMany(p => p.PhoneNumbers)
                    .HasForeignKey(d => d.IdCompanies)
                    .HasConstraintName("FK_Phone_numbers_ToTable");
            });

            modelBuilder.Entity<Telephonist>(entity =>
            {
                entity.ToTable("Telephonist‏");

                entity.Property(e => e.DateBirth).HasColumnType("datetime");

                entity.Property(e => e.Mail).HasMaxLength(25);

                entity.Property(e => e.Name).HasMaxLength(15);

                entity.Property(e => e.Telephone).HasMaxLength(10);

                entity.Property(e => e.Tz)
                    .HasColumnName("tz")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<TelephonistInCompanies>(entity =>
            {
                entity.Property(e => e.Password).HasMaxLength(20);

                entity.HasOne(d => d.IdCompanyNavigation)
                    .WithMany(p => p.TelephonistInCompanies)
                    .HasForeignKey(d => d.IdCompany)
                    .HasConstraintName("FK_TelephonistInCompanies_ToTable");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
