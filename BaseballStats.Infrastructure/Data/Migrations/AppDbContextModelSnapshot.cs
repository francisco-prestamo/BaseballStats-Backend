﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BaseballStats.Domain.Entities.AlignedPlayerInGame", b =>
                {
                    b.Property<long>("PlayerId")
                        .HasColumnType("bigint");

                    b.Property<long>("GameId")
                        .HasColumnType("bigint");

                    b.Property<string>("Position")
                        .HasColumnType("text");

                    b.Property<long>("TeamId")
                        .HasColumnType("bigint");

                    b.HasKey("PlayerId", "GameId", "Position");

                    b.HasIndex("GameId");

                    b.HasIndex("TeamId");

                    b.HasIndex("PlayerId", "GameId")
                        .IsUnique();

                    b.HasIndex("Position", "TeamId", "GameId")
                        .IsUnique();

                    b.ToTable("AlignedPlayerInGame");
                });

            modelBuilder.Entity("BaseballStats.Domain.Entities.DirectionStaff", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("DirectionStaff");
                });

            modelBuilder.Entity("BaseballStats.Domain.Entities.Game", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int>("Runs1")
                        .HasColumnType("integer");

                    b.Property<int>("Runs2")
                        .HasColumnType("integer");

                    b.Property<long>("SeriesId")
                        .HasColumnType("bigint");

                    b.Property<long>("Team1Id")
                        .HasColumnType("bigint");

                    b.Property<long>("Team2Id")
                        .HasColumnType("bigint");

                    b.Property<bool>("Winner1")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("SeriesId");

                    b.HasIndex("Team2Id");

                    b.HasIndex("Team1Id", "Team2Id", "Date")
                        .IsUnique();

                    b.ToTable("Game");
                });

            modelBuilder.Entity("BaseballStats.Domain.Entities.Identity.RegisteredUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("RegisteredUser");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("BaseballStats.Domain.Entities.Player", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<double?>("BattingAverage")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("YearsOfExperience")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Player");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("BaseballStats.Domain.Entities.PlayerInPosition", b =>
                {
                    b.Property<long>("PlayerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Position")
                        .HasColumnType("text");

                    b.Property<double>("Effectiveness")
                        .HasColumnType("double precision");

                    b.HasKey("PlayerId", "Position");

                    b.ToTable("PlayerInPosition");
                });

            modelBuilder.Entity("BaseballStats.Domain.Entities.PlayerInSeries", b =>
                {
                    b.Property<long>("PlayerId")
                        .HasColumnType("bigint");

                    b.Property<long>("SeriesId")
                        .HasColumnType("bigint");

                    b.Property<long?>("TeamId")
                        .HasColumnType("bigint");

                    b.HasKey("PlayerId", "SeriesId");

                    b.HasIndex("SeriesId");

                    b.HasIndex("TeamId");

                    b.ToTable("PlayerInSeries");
                });

            modelBuilder.Entity("BaseballStats.Domain.Entities.Season", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Season");
                });

            modelBuilder.Entity("BaseballStats.Domain.Entities.Series", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<long>("SeasonId")
                        .HasColumnType("bigint");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("SeasonId");

                    b.ToTable("Series");
                });

            modelBuilder.Entity("BaseballStats.Domain.Entities.StarPlayerInPosition", b =>
                {
                    b.Property<long>("PlayerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Position")
                        .HasColumnType("text");

                    b.Property<long>("SeriesId")
                        .HasColumnType("bigint");

                    b.HasKey("PlayerId", "Position", "SeriesId");

                    b.HasIndex("SeriesId");

                    b.ToTable("StarPlayerInPosition");
                });

            modelBuilder.Entity("BaseballStats.Domain.Entities.Substitution", b =>
                {
                    b.Property<long>("PlayerInId")
                        .HasColumnType("bigint");

                    b.Property<long>("PlayerOutId")
                        .HasColumnType("bigint");

                    b.Property<long>("GameId")
                        .HasColumnType("bigint");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("interval");

                    b.Property<long>("TeamId")
                        .HasColumnType("bigint");

                    b.HasKey("PlayerInId", "PlayerOutId", "GameId", "Time");

                    b.HasIndex("GameId");

                    b.HasIndex("PlayerOutId");

                    b.HasIndex("TeamId");

                    b.ToTable("Substitution");
                });

            modelBuilder.Entity("BaseballStats.Domain.Entities.Team", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Initials")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("RepresentedEntity")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<long>("TechnicalDirectorId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TechnicalDirectorId");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("DirectionStaffTeam", b =>
                {
                    b.Property<long>("DirectionStaffsId")
                        .HasColumnType("bigint");

                    b.Property<long>("TeamsLeadId")
                        .HasColumnType("bigint");

                    b.HasKey("DirectionStaffsId", "TeamsLeadId");

                    b.HasIndex("TeamsLeadId");

                    b.ToTable("DirectionStaffTeam");
                });

            modelBuilder.Entity("BaseballStats.Domain.Entities.Identity.TechnicalDirector", b =>
                {
                    b.HasBaseType("BaseballStats.Domain.Entities.Identity.RegisteredUser");

                    b.ToTable("TechnicalDirector");
                });

            modelBuilder.Entity("BaseballStats.Domain.Entities.Pitcher", b =>
                {
                    b.HasBaseType("BaseballStats.Domain.Entities.Player");

                    b.Property<double>("AllowedRunsAvg")
                        .HasColumnType("double precision");

                    b.Property<int>("GamesLostNumber")
                        .HasColumnType("integer");

                    b.Property<int>("GamesWonNumber")
                        .HasColumnType("integer");

                    b.Property<bool>("RightHanded")
                        .HasColumnType("boolean");

                    b.ToTable("Pitcher");
                });

            modelBuilder.Entity("BaseballStats.Domain.Entities.AlignedPlayerInGame", b =>
                {
                    b.HasOne("BaseballStats.Domain.Entities.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaseballStats.Domain.Entities.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaseballStats.Domain.Entities.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Player");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("BaseballStats.Domain.Entities.Game", b =>
                {
                    b.HasOne("BaseballStats.Domain.Entities.Series", "Series")
                        .WithMany()
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaseballStats.Domain.Entities.Team", "Team1")
                        .WithMany()
                        .HasForeignKey("Team1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaseballStats.Domain.Entities.Team", "Team2")
                        .WithMany()
                        .HasForeignKey("Team2Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Series");

                    b.Navigation("Team1");

                    b.Navigation("Team2");
                });

            modelBuilder.Entity("BaseballStats.Domain.Entities.PlayerInPosition", b =>
                {
                    b.HasOne("BaseballStats.Domain.Entities.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("BaseballStats.Domain.Entities.PlayerInSeries", b =>
                {
                    b.HasOne("BaseballStats.Domain.Entities.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaseballStats.Domain.Entities.Series", "Series")
                        .WithMany()
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaseballStats.Domain.Entities.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId");

                    b.Navigation("Player");

                    b.Navigation("Series");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("BaseballStats.Domain.Entities.Series", b =>
                {
                    b.HasOne("BaseballStats.Domain.Entities.Season", "Season")
                        .WithMany()
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Season");
                });

            modelBuilder.Entity("BaseballStats.Domain.Entities.StarPlayerInPosition", b =>
                {
                    b.HasOne("BaseballStats.Domain.Entities.Series", "Series")
                        .WithMany()
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaseballStats.Domain.Entities.PlayerInPosition", "PlayerInPosition")
                        .WithMany()
                        .HasForeignKey("PlayerId", "Position")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlayerInPosition");

                    b.Navigation("Series");
                });

            modelBuilder.Entity("BaseballStats.Domain.Entities.Substitution", b =>
                {
                    b.HasOne("BaseballStats.Domain.Entities.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaseballStats.Domain.Entities.Player", "PlayerIn")
                        .WithMany()
                        .HasForeignKey("PlayerInId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaseballStats.Domain.Entities.Player", "PlayerOut")
                        .WithMany()
                        .HasForeignKey("PlayerOutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaseballStats.Domain.Entities.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("PlayerIn");

                    b.Navigation("PlayerOut");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("BaseballStats.Domain.Entities.Team", b =>
                {
                    b.HasOne("BaseballStats.Domain.Entities.Identity.TechnicalDirector", "TechnicalDirector")
                        .WithMany()
                        .HasForeignKey("TechnicalDirectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TechnicalDirector");
                });

            modelBuilder.Entity("DirectionStaffTeam", b =>
                {
                    b.HasOne("BaseballStats.Domain.Entities.DirectionStaff", null)
                        .WithMany()
                        .HasForeignKey("DirectionStaffsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaseballStats.Domain.Entities.Team", null)
                        .WithMany()
                        .HasForeignKey("TeamsLeadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BaseballStats.Domain.Entities.Identity.TechnicalDirector", b =>
                {
                    b.HasOne("BaseballStats.Domain.Entities.Identity.RegisteredUser", null)
                        .WithOne()
                        .HasForeignKey("BaseballStats.Domain.Entities.Identity.TechnicalDirector", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BaseballStats.Domain.Entities.Pitcher", b =>
                {
                    b.HasOne("BaseballStats.Domain.Entities.Player", null)
                        .WithOne()
                        .HasForeignKey("BaseballStats.Domain.Entities.Pitcher", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
