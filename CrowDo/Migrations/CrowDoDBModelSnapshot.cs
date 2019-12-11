﻿// <auto-generated />
using System;
using CrowDo.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CrowDo.Migrations
{
    [DbContext(typeof(CrowDoDB))]
    partial class CrowDoDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CrowDo.Entities.Funding", b =>
                {
                    b.Property<int>("FundingId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BackerMemberId");

                    b.Property<int>("Number");

                    b.Property<int?>("PackagesId");

                    b.Property<int?>("ProjectId");

                    b.HasKey("FundingId");

                    b.HasIndex("BackerMemberId");

                    b.HasIndex("PackagesId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Fundings");
                });

            modelBuilder.Entity("CrowDo.Entities.Member", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("Code");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("IsDeleted");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("MemberId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("CrowDo.Entities.Packages", b =>
                {
                    b.Property<int>("PackagesId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<int>("Cost");

                    b.Property<string>("Details");

                    b.Property<int?>("ProjectId");

                    b.Property<string>("Rewards");

                    b.Property<string>("Title");

                    b.HasKey("PackagesId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("CrowDo.Entities.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category");

                    b.Property<string>("Code");

                    b.Property<int?>("CreatorMemberId");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("IsDeleted");

                    b.Property<string>("Media");

                    b.Property<string>("NumberOfRequested");

                    b.Property<int>("NumberOfVisits");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Title");

                    b.HasKey("ProjectId");

                    b.HasIndex("CreatorMemberId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("CrowDo.Entities.Funding", b =>
                {
                    b.HasOne("CrowDo.Entities.Member", "Backer")
                        .WithMany("Fundings")
                        .HasForeignKey("BackerMemberId");

                    b.HasOne("CrowDo.Entities.Packages", "Package")
                        .WithMany()
                        .HasForeignKey("PackagesId");

                    b.HasOne("CrowDo.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("CrowDo.Entities.Packages", b =>
                {
                    b.HasOne("CrowDo.Entities.Project")
                        .WithMany("Packages")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("CrowDo.Entities.Project", b =>
                {
                    b.HasOne("CrowDo.Entities.Member", "Creator")
                        .WithMany("Projects")
                        .HasForeignKey("CreatorMemberId");
                });
#pragma warning restore 612, 618
        }
    }
}
