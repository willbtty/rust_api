﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RustGameAPI.Data;

#nullable disable

namespace RustGameAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RustGameAPI.Models.Friend", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("FriendUserID")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.HasKey("UserID", "FriendUserID");

                    b.HasIndex("FriendUserID");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("RustGameAPI.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RustGameAPI.Models.Friend", b =>
                {
                    b.HasOne("RustGameAPI.Models.User", "FriendUser")
                        .WithMany("FriendOf")
                        .HasForeignKey("FriendUserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RustGameAPI.Models.User", "User")
                        .WithMany("Friends")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FriendUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RustGameAPI.Models.User", b =>
                {
                    b.Navigation("FriendOf");

                    b.Navigation("Friends");
                });
#pragma warning restore 612, 618
        }
    }
}
