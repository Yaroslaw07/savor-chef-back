﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SavorChef.Backend.Data;

#nullable disable

namespace SavorChef.Backend.Migrations
{
    [DbContext(typeof(ApiContext))]
    partial class ApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ProductEntityRecipeEntity", b =>
                {
                    b.Property<int>("AssociatedProductsId")
                        .HasColumnType("integer");

                    b.Property<int>("AssociatedRecipesId")
                        .HasColumnType("integer");

                    b.HasKey("AssociatedProductsId", "AssociatedRecipesId");

                    b.HasIndex("AssociatedRecipesId");

                    b.ToTable("ProductEntityRecipeEntity");
                });

            modelBuilder.Entity("SavorChef.Backend.Data.Entities.ProductEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SavorChef.Backend.Data.Entities.RecipeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Difficulty")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DishCategory")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PreparationInstructions")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PreparationTime")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RecipeDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("SavorChef.Backend.Data.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProductEntityRecipeEntity", b =>
                {
                    b.HasOne("SavorChef.Backend.Data.Entities.ProductEntity", null)
                        .WithMany()
                        .HasForeignKey("AssociatedProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SavorChef.Backend.Data.Entities.RecipeEntity", null)
                        .WithMany()
                        .HasForeignKey("AssociatedRecipesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SavorChef.Backend.Data.Entities.RecipeEntity", b =>
                {
                    b.HasOne("SavorChef.Backend.Data.Entities.UserEntity", "UserEntity")
                        .WithMany("RecipesEntities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("SavorChef.Backend.Data.Entities.UserEntity", b =>
                {
                    b.Navigation("RecipesEntities");
                });
#pragma warning restore 612, 618
        }
    }
}
