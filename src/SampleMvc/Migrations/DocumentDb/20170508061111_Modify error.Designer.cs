using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SampleMvc.Board;
using SampleMvc.Board.Data;
using SampleMvc.Data;

namespace SampleMvc.Migrations.DocumentDb
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20170508061111_Modify error")]
    partial class Modifyerror
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SampleMvc.Board.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnswerNum");

                    b.Property<int>("CommentCount");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(4000);

                    b.Property<DateTime>("ModifyDate");

                    b.Property<string>("ModifyIp");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int>("ParentNum");

                    b.Property<DateTime>("PostDate");

                    b.Property<string>("PostIp");

                    b.Property<int>("ReadCount");

                    b.Property<int>("Ref");

                    b.Property<int>("RefOrder");

                    b.Property<int>("Step");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Documents");
                });
        }
    }
}
