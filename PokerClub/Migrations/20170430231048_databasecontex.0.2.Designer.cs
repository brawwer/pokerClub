using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PokerClub.Data;

namespace PokerClub.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20170430231048_databasecontex.0.2")]
    partial class databasecontex02
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PokerClub.Models.PokerTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Host");

                    b.HasKey("Id");

                    b.ToTable("PokerTables");
                });

            modelBuilder.Entity("PokerClub.Models.PokerTableHasMembers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MemberId");

                    b.Property<int>("PokerTableId");

                    b.HasKey("Id");

                    b.HasIndex("PokerTableId");

                    b.ToTable("PokerTableHasMembers");
                });

            modelBuilder.Entity("PokerClub.Models.PokerTableValuation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<string>("MemberId");

                    b.Property<int>("PokerTableId");

                    b.Property<int>("ValueCategoryId");

                    b.HasKey("Id");

                    b.HasIndex("PokerTableId");

                    b.HasIndex("ValueCategoryId");

                    b.ToTable("PokerTableValuations");
                });

            modelBuilder.Entity("PokerClub.Models.ValueCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ValueCategories");
                });

            modelBuilder.Entity("PokerClub.Models.PokerTableHasMembers", b =>
                {
                    b.HasOne("PokerClub.Models.PokerTable", "PokerTable")
                        .WithMany("PokerTableHasMembers")
                        .HasForeignKey("PokerTableId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PokerClub.Models.PokerTableValuation", b =>
                {
                    b.HasOne("PokerClub.Models.PokerTable", "PokerTable")
                        .WithMany()
                        .HasForeignKey("PokerTableId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PokerClub.Models.ValueCategory", "ValueCategory")
                        .WithMany()
                        .HasForeignKey("ValueCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
