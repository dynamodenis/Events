﻿// <auto-generated />
using System;
using AptaEvents.Module.BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AptaEvents.Module.Migrations
{
    [DbContext(typeof(AptaEventsEFCoreDbContext))]
    [Migration("20230704003712_nojson")]
    partial class nojson
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Proxies:ChangeTracking", true)
                .HasAnnotation("Proxies:CheckEquality", true)
                .HasAnnotation("Proxies:LazyLoading", false)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AptaEvents.Module.BusinessObjects.ApplicationUserLoginInfo", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProviderName")
                        .HasColumnType("text");

                    b.Property<string>("ProviderUserKey")
                        .HasColumnType("text");

                    b.Property<Guid>("UserForeignKey")
                        .HasColumnType("uuid");

                    b.HasKey("ID");

                    b.HasIndex("UserForeignKey");

                    b.HasIndex("LoginProviderName", "ProviderUserKey")
                        .IsUnique();

                    b.ToTable("PermissionPolicyUserLoginInfo");
                });

            modelBuilder.Entity("AptaEvents.Module.BusinessObjects.Event", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<string>("EventLink")
                        .HasColumnType("text");

                    b.Property<bool>("Live")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("AptaEvents.Module.BusinessObjects.EventField", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("EventID")
                        .HasColumnType("uuid");

                    b.Property<string>("Field")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("EventID");

                    b.ToTable("EventFields");
                });

            modelBuilder.Entity("AptaEvents.Module.BusinessObjects.Field", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("SortOrder")
                        .HasColumnType("integer");

                    b.Property<Guid?>("TabID")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("TabID");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("AptaEvents.Module.BusinessObjects.Tab", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("SortOrder")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.ToTable("Tabs");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.ModelDifference", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ContextId")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.ToTable("ModelDifferences");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.ModelDifferenceAspect", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid?>("OwnerID")
                        .HasColumnType("uuid");

                    b.Property<string>("Xml")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("OwnerID");

                    b.ToTable("ModelDifferenceAspects");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyActionPermissionObject", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ActionId")
                        .HasColumnType("text");

                    b.Property<Guid?>("RoleID")
                        .HasColumnType("uuid");

                    b.HasKey("ID");

                    b.HasIndex("RoleID");

                    b.ToTable("PermissionPolicyActionPermissionObject");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyMemberPermissionsObject", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Criteria")
                        .HasColumnType("text");

                    b.Property<string>("Members")
                        .HasColumnType("text");

                    b.Property<int?>("ReadState")
                        .HasColumnType("integer");

                    b.Property<Guid?>("TypePermissionObjectID")
                        .HasColumnType("uuid");

                    b.Property<int?>("WriteState")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("TypePermissionObjectID");

                    b.ToTable("PermissionPolicyMemberPermissionsObject");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyNavigationPermissionObject", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ItemPath")
                        .HasColumnType("text");

                    b.Property<int?>("NavigateState")
                        .HasColumnType("integer");

                    b.Property<Guid?>("RoleID")
                        .HasColumnType("uuid");

                    b.Property<string>("TargetTypeFullName")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("RoleID");

                    b.ToTable("PermissionPolicyNavigationPermissionObject");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyObjectPermissionsObject", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Criteria")
                        .HasColumnType("text");

                    b.Property<int?>("DeleteState")
                        .HasColumnType("integer");

                    b.Property<int?>("NavigateState")
                        .HasColumnType("integer");

                    b.Property<int?>("ReadState")
                        .HasColumnType("integer");

                    b.Property<Guid?>("TypePermissionObjectID")
                        .HasColumnType("uuid");

                    b.Property<int?>("WriteState")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("TypePermissionObjectID");

                    b.ToTable("PermissionPolicyObjectPermissionsObject");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyRoleBase", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("CanEditModel")
                        .HasColumnType("boolean");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsAdministrative")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsAllowPermissionPriority")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("PermissionPolicy")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.ToTable("PermissionPolicyRoleBase");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PermissionPolicyRoleBase");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyTypePermissionObject", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("CreateState")
                        .HasColumnType("integer");

                    b.Property<int?>("DeleteState")
                        .HasColumnType("integer");

                    b.Property<int?>("NavigateState")
                        .HasColumnType("integer");

                    b.Property<int?>("ReadState")
                        .HasColumnType("integer");

                    b.Property<Guid?>("RoleID")
                        .HasColumnType("uuid");

                    b.Property<string>("TargetTypeFullName")
                        .HasColumnType("text");

                    b.Property<int?>("WriteState")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("RoleID");

                    b.ToTable("PermissionPolicyTypePermissionObject");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyUser", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("ChangePasswordOnFirstLogon")
                        .HasColumnType("boolean");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("StoredPassword")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("PermissionPolicyUser");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PermissionPolicyUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("PermissionPolicyRolePermissionPolicyUser", b =>
                {
                    b.Property<Guid>("RolesID")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersID")
                        .HasColumnType("uuid");

                    b.HasKey("RolesID", "UsersID");

                    b.HasIndex("UsersID");

                    b.ToTable("PermissionPolicyRolePermissionPolicyUser");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyRole", b =>
                {
                    b.HasBaseType("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyRoleBase");

                    b.HasDiscriminator().HasValue("PermissionPolicyRole");
                });

            modelBuilder.Entity("AptaEvents.Module.BusinessObjects.ApplicationUser", b =>
                {
                    b.HasBaseType("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyUser");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("AptaEvents.Module.BusinessObjects.ApplicationUserLoginInfo", b =>
                {
                    b.HasOne("AptaEvents.Module.BusinessObjects.ApplicationUser", "User")
                        .WithMany("UserLogins")
                        .HasForeignKey("UserForeignKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AptaEvents.Module.BusinessObjects.EventField", b =>
                {
                    b.HasOne("AptaEvents.Module.BusinessObjects.Event", null)
                        .WithMany("EventFields")
                        .HasForeignKey("EventID");
                });

            modelBuilder.Entity("AptaEvents.Module.BusinessObjects.Field", b =>
                {
                    b.HasOne("AptaEvents.Module.BusinessObjects.Tab", "Tab")
                        .WithMany("Fields")
                        .HasForeignKey("TabID");

                    b.Navigation("Tab");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.ModelDifferenceAspect", b =>
                {
                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.ModelDifference", "Owner")
                        .WithMany("Aspects")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyActionPermissionObject", b =>
                {
                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyRoleBase", "Role")
                        .WithMany("ActionPermissions")
                        .HasForeignKey("RoleID");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyMemberPermissionsObject", b =>
                {
                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyTypePermissionObject", "TypePermissionObject")
                        .WithMany("MemberPermissions")
                        .HasForeignKey("TypePermissionObjectID");

                    b.Navigation("TypePermissionObject");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyNavigationPermissionObject", b =>
                {
                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyRoleBase", "Role")
                        .WithMany("NavigationPermissions")
                        .HasForeignKey("RoleID");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyObjectPermissionsObject", b =>
                {
                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyTypePermissionObject", "TypePermissionObject")
                        .WithMany("ObjectPermissions")
                        .HasForeignKey("TypePermissionObjectID");

                    b.Navigation("TypePermissionObject");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyTypePermissionObject", b =>
                {
                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyRoleBase", "Role")
                        .WithMany("TypePermissions")
                        .HasForeignKey("RoleID");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("PermissionPolicyRolePermissionPolicyUser", b =>
                {
                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyRole", null)
                        .WithMany()
                        .HasForeignKey("RolesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyUser", null)
                        .WithMany()
                        .HasForeignKey("UsersID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AptaEvents.Module.BusinessObjects.Event", b =>
                {
                    b.Navigation("EventFields");
                });

            modelBuilder.Entity("AptaEvents.Module.BusinessObjects.Tab", b =>
                {
                    b.Navigation("Fields");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.ModelDifference", b =>
                {
                    b.Navigation("Aspects");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyRoleBase", b =>
                {
                    b.Navigation("ActionPermissions");

                    b.Navigation("NavigationPermissions");

                    b.Navigation("TypePermissions");
                });

            modelBuilder.Entity("DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyTypePermissionObject", b =>
                {
                    b.Navigation("MemberPermissions");

                    b.Navigation("ObjectPermissions");
                });

            modelBuilder.Entity("AptaEvents.Module.BusinessObjects.ApplicationUser", b =>
                {
                    b.Navigation("UserLogins");
                });
#pragma warning restore 612, 618
        }
    }
}
