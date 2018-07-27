﻿// <auto-generated />
using Miki.Framework.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Meru.Core.Migrations
{
    [DbContext(typeof(IAContext))]
    partial class IAModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Miki.Framework.Models.CommandState", b =>
                {
                    b.Property<string>("CommandName");

                    b.Property<long>("ChannelId");

                    b.Property<bool>("State");

                    b.HasKey("CommandName", "ChannelId");

                    b.ToTable("CommandStates");
                });

            modelBuilder.Entity("Miki.Framework.Models.Identifier", b =>
                {
                    b.Property<long>("GuildId");

                    b.Property<string>("DefaultValue");

                    b.Property<string>("Value");

                    b.HasKey("GuildId", "DefaultValue");

                    b.ToTable("Identifiers");
                });

            modelBuilder.Entity("Miki.Framework.Models.ModuleState", b =>
                {
                    b.Property<string>("ModuleName");

                    b.Property<long>("ChannelId");

                    b.Property<bool>("State");

                    b.HasKey("ModuleName", "ChannelId");

                    b.ToTable("ModuleStates");
                });
#pragma warning restore 612, 618
        }
    }
}
