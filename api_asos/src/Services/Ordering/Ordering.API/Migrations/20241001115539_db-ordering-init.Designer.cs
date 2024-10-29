﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Ordering.API.Data;

#nullable disable

namespace Ordering.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241001115539_db-ordering-init")]
    partial class dborderinginit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Ordering.API.Data.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("BasePrice")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreatedUser")
                        .HasColumnType("uuid");

                    b.Property<bool>("DeleteFlag")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("DiscountId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("DiscountPrice")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifiedUser")
                        .HasColumnType("uuid");

                    b.Property<int>("PointUsed")
                        .HasColumnType("integer");

                    b.Property<string>("StatusId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("SubPrice")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Total")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("TransactionId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DiscountId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("tb_orders");
                });

            modelBuilder.Entity("Ordering.API.Data.OrderHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreatedUser")
                        .HasColumnType("uuid");

                    b.Property<bool>("DeleteFlag")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifiedUser")
                        .HasColumnType("uuid");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("tb_order_histories");
                });

            modelBuilder.Entity("Ordering.API.Data.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreatedUser")
                        .HasColumnType("uuid");

                    b.Property<bool>("DeleteFlag")
                        .HasColumnType("boolean");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifiedUser")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Stock")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("tb_order_items");
                });

            modelBuilder.Entity("Ordering.API.Data.OrderStatus", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreatedUser")
                        .HasColumnType("uuid");

                    b.Property<bool>("DeleteFlag")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifiedUser")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("tb_order_status");
                });

            modelBuilder.Entity("Ordering.API.Data.Refund", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreatedUser")
                        .HasColumnType("uuid");

                    b.Property<bool>("DeleteFlag")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifiedUser")
                        .HasColumnType("uuid");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("RefundAmount")
                        .HasColumnType("numeric");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("tb_refunds");
                });

            modelBuilder.Entity("Ordering.API.Data.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BankBranch")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BankNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreatedUser")
                        .HasColumnType("uuid");

                    b.Property<bool>("DeleteFlag")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifiedUser")
                        .HasColumnType("uuid");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("RefundId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Total")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.HasIndex("RefundId")
                        .IsUnique();

                    b.ToTable("tb_transactions");
                });

            modelBuilder.Entity("Ordering.API.Data.Order", b =>
                {
                    b.HasOne("Ordering.API.Data.OrderStatus", "Status")
                        .WithMany("Orders")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Ordering.API.Data.OrderHistory", b =>
                {
                    b.HasOne("Ordering.API.Data.Order", "Order")
                        .WithMany("OrderHistories")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Ordering.API.Data.OrderItem", b =>
                {
                    b.HasOne("Ordering.API.Data.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Ordering.API.Data.Transaction", b =>
                {
                    b.HasOne("Ordering.API.Data.Order", "Order")
                        .WithOne("Transaction")
                        .HasForeignKey("Ordering.API.Data.Transaction", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ordering.API.Data.Refund", "Refund")
                        .WithOne("Transaction")
                        .HasForeignKey("Ordering.API.Data.Transaction", "RefundId");

                    b.Navigation("Order");

                    b.Navigation("Refund");
                });

            modelBuilder.Entity("Ordering.API.Data.Order", b =>
                {
                    b.Navigation("OrderHistories");

                    b.Navigation("OrderItems");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("Ordering.API.Data.OrderStatus", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Ordering.API.Data.Refund", b =>
                {
                    b.Navigation("Transaction")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
