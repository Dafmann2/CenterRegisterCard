using System;
using System.Collections.Generic;
using CenterRegisterCard.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CenterRegisterCard.DAL
{
    public partial class CenterRegisterCardContext : DbContext
    {
        public CenterRegisterCardContext()
        {
        }

        public CenterRegisterCardContext(DbContextOptions<CenterRegisterCardContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Beneficiary> Beneficiaries { get; set; }
        public virtual DbSet<CategoryBeneficiary> CategoryBeneficiaries { get; set; }
        public virtual DbSet<CategorySocialCard> CategorySocialCards { get; set; }
        public virtual DbSet<ChatTechnicalSupport> ChatTechnicalSupports { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventUpdate> EventUpdates { get; set; }
        public virtual DbSet<SocialCard> SocialCards { get; set; }
        public virtual DbSet<SocialCardStatus> SocialCardStatuses { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserStatus> UserStatuses { get; set; }

        public static User userAccount { get; set; }
        public static User userEmailChangePassword { get; set; }
        public static Employee employeeAccount { get; set; }
        public static string userAccountreg { get; set; }
        public static User userActiveView { get; set; }
        public static SocialCard socialCard { get; set; }
        public static List<CategoryBeneficiary> listselect { get; set; }
        public static List<SelectListItem> listcategory = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Студент"},
                new SelectListItem { Value = "2", Text = "Пенсионер"},
                new SelectListItem { Value = "3", Text = "Инвалид" }
            };
        public static CategoryBeneficiary categoryBeneficiary { get; set; }
        public static CategoryBeneficiary categoryBeneficiaryreg { get; set; }

        public static string filename { get; set; }
        public static string EmailMessageRecovery { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("server=localhost;port=5432;userid=postgres;database=CenterRegisterCard;password=1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beneficiary>(entity =>
            {
                entity.ToTable("Beneficiary");

                entity.Property(e => e.BeneficiaryId)
                    .ValueGeneratedNever()
                    .HasColumnName("BeneficiaryID");

                entity.Property(e => e.BeneficiaryServices)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<CategoryBeneficiary>(entity =>
            {
                entity.ToTable("CategoryBeneficiary");

                entity.Property(e => e.CategoryBeneficiaryId)
                    .ValueGeneratedNever()
                    .HasColumnName("CategoryBeneficiaryID");

                entity.Property(e => e.BeneficiaryId).HasColumnName("BeneficiaryID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.HasOne(d => d.Beneficiary)
                    .WithMany(p => p.CategoryBeneficiaries)
                    .HasForeignKey(d => d.BeneficiaryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CategoryBeneficiaryFK");
            });

            modelBuilder.Entity<CategorySocialCard>(entity =>
            {
                entity.HasKey(e => e.IdCategory)
                    .HasName("CategorySocialCard_pkey");

                entity.ToTable("CategorySocialCard");

                entity.Property(e => e.IdCategory)
                    .ValueGeneratedNever()
                    .HasColumnName("idCategory");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<ChatTechnicalSupport>(entity =>
            {
                entity.HasKey(e => e.ChatId)
                    .HasName("ChatTechnicalSupport_pkey");

                entity.ToTable("ChatTechnicalSupport");

                entity.Property(e => e.ChatId)
                    .ValueGeneratedNever()
                    .HasColumnName("ChatID");

                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("EmployeeID");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ChatTechnicalSupports)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EmployeeIDPassportFK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ChatTechnicalSupports)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserIDPassportFK");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.PassportSeriesNumber)
                    .HasName("Employee_pkey");

                entity.Property(e => e.PassportSeriesNumber).HasMaxLength(10);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Patronymic)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Surename)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event");

                entity.Property(e => e.EventId)
                    .ValueGeneratedNever()
                    .HasColumnName("EventID");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.DescriptionTitle).HasColumnType("character varying");

                entity.Property(e => e.ImageEvent)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<EventUpdate>(entity =>
            {
                entity.ToTable("EventUpdate");

                entity.Property(e => e.EventUpdateId)
                    .ValueGeneratedNever()
                    .HasColumnName("EventUpdateID");

                entity.Property(e => e.EmployeePassport)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.HasOne(d => d.EmployeePassportNavigation)
                    .WithMany(p => p.EventUpdates)
                    .HasForeignKey(d => d.EmployeePassport)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Employee");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventUpdates)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Event");
            });

            modelBuilder.Entity<SocialCard>(entity =>
            {
                entity.HasKey(e => e.NumberCard)
                    .HasName("CardSocial_pkey");

                entity.Property(e => e.NumberCard).HasColumnType("character varying");

                entity.Property(e => e.CategorySocialCardId).HasColumnName("CategorySocialCardID");

                entity.Property(e => e.Cvc).HasColumnName("CVC");

                entity.Property(e => e.PassportSeriesNumberUser)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.StatusCardId).HasColumnName("StatusCardID");

                entity.HasOne(d => d.CategorySocialCard)
                    .WithMany(p => p.SocialCards)
                    .HasForeignKey(d => d.CategorySocialCardId)
                    .HasConstraintName("CategorySocialCard");

                entity.HasOne(d => d.PassportSeriesNumberUserNavigation)
                    .WithMany(p => p.SocialCards)
                    .HasForeignKey(d => d.PassportSeriesNumberUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserFK");

                entity.HasOne(d => d.StatusCard)
                    .WithMany(p => p.SocialCards)
                    .HasForeignKey(d => d.StatusCardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("StatusID");
            });

            modelBuilder.Entity<SocialCardStatus>(entity =>
            {
                entity.ToTable("SocialCardStatus");

                entity.Property(e => e.SocialCardStatusId)
                    .ValueGeneratedNever()
                    .HasColumnName("SocialCardStatusID");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.PassportSeriesNumber)
                    .HasName("Users_pkey");

                entity.Property(e => e.PassportSeriesNumber).HasMaxLength(10);

                entity.Property(e => e.CategoryBeneficiaryId).HasColumnName("CategoryBeneficiaryID");

                entity.Property(e => e.DocumentImgName).HasColumnType("character varying");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Inn)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("INN");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Patronymic)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.UserStatusId).HasColumnName("UserStatusID");

                entity.HasOne(d => d.CategoryBeneficiary)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CategoryBeneficiaryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("BeneficiaryFK");

                entity.HasOne(d => d.UserStatus)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserStatus");
            });

            modelBuilder.Entity<UserStatus>(entity =>
            {
                entity.HasKey(e => e.IduserStatus)
                    .HasName("UserStatus_pkey");

                entity.ToTable("UserStatus");

                entity.Property(e => e.IduserStatus)
                    .ValueGeneratedNever()
                    .HasColumnName("IDUserStatus");

                entity.Property(e => e.NameStatus)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
