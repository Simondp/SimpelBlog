﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpelBlog.Model;

namespace Model.Migrations
{
    [DbContext(typeof(BlogContext))]
    [Migration("20210103161208_PostInfoAdded")]
    partial class PostInfoAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SimpelBlog.Model.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createdOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("updatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("SimpelBlog.Model.Post", b =>
                {
                    b.Property<Guid>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("createdOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("postAsHtml")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("postAsMarkdown")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("updatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("PostId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("SimpelBlog.Model.PostInfo", b =>
                {
                    b.Property<Guid>("PostInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("postStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("publishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("recap")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("titel")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PostInfoId");

                    b.HasIndex("PostId")
                        .IsUnique();

                    b.ToTable("PostInfos");
                });

            modelBuilder.Entity("SimpelBlog.Model.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createdOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("firstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("updatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SimpelBlog.Model.Comment", b =>
                {
                    b.HasOne("SimpelBlog.Model.Post", null)
                        .WithMany("comments")
                        .HasForeignKey("PostId");
                });

            modelBuilder.Entity("SimpelBlog.Model.PostInfo", b =>
                {
                    b.HasOne("SimpelBlog.Model.Post", "post")
                        .WithOne("postInfo")
                        .HasForeignKey("SimpelBlog.Model.PostInfo", "PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
