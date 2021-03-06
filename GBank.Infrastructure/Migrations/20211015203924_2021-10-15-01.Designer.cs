// <auto-generated />
using System;
using GBank.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GBank.Infrastructure.Migrations
{
    [DbContext(typeof(GBankDbContext))]
    [Migration("20211015203924_2021-10-15-01")]
    partial class _2021101501
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BillUser", b =>
                {
                    b.Property<int>("BillsID")
                        .HasColumnType("int");

                    b.Property<int>("UsersID")
                        .HasColumnType("int");

                    b.HasKey("BillsID", "UsersID");

                    b.HasIndex("UsersID");

                    b.ToTable("BillUser");
                });

            modelBuilder.Entity("GBank.Domain.Entities.Bill", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("billNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("GBank.Domain.Entities.News", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("News");
                });

            modelBuilder.Entity("GBank.Domain.Entities.RefreshTokens", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("GBank.Domain.Entities.Transaction", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("billID")
                        .HasColumnType("int");

                    b.Property<DateTime>("datetime")
                        .HasColumnType("datetime2");

                    b.Property<string>("direction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("transactionid")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("billID");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("GBank.Domain.Entities.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("active")
                        .HasColumnType("bit");

                    b.Property<string>("firstname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BillUser", b =>
                {
                    b.HasOne("GBank.Domain.Entities.Bill", null)
                        .WithMany()
                        .HasForeignKey("BillsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GBank.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GBank.Domain.Entities.RefreshTokens", b =>
                {
                    b.HasOne("GBank.Domain.Entities.User", "User")
                        .WithMany("RefreshTokensList")
                        .HasForeignKey("UserID");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GBank.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("GBank.Domain.Entities.Bill", "bill")
                        .WithMany("Transactions")
                        .HasForeignKey("billID");

                    b.Navigation("bill");
                });

            modelBuilder.Entity("GBank.Domain.Entities.Bill", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("GBank.Domain.Entities.User", b =>
                {
                    b.Navigation("RefreshTokensList");
                });
#pragma warning restore 612, 618
        }
    }
}
