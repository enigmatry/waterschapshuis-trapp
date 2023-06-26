using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Seeding.Users
{
    public class RolesAndPermissionsSeeding : ISeeding
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            seedPermissions(new[] {
                PermissionId.MapRead,
                PermissionId.MapContentRead,
                PermissionId.MapContentWrite,
                PermissionId.ReportReadWrite,
                PermissionId.TimeRegistrationPersonalReadWrite,
                PermissionId.TimeRegistrationManagementReadWrite,
                PermissionId.UserReadWrite,
                PermissionId.Management,
                PermissionId.ReadOnly,
                PermissionId.ApiPublic,
                PermissionId.ApiPrivate,
                PermissionId.Mobile,
                PermissionId.AssignMaintainerRole
            });

            void seedPermissions(PermissionId[] permissionIds) =>
                permissionIds.ToList().ForEach(permissionId =>
                    modelBuilder.Entity<Permission>().HasData(Permission.Create(permissionId)));
         
            var roles = GetRoles();
            modelBuilder.Entity<Role>().HasData(roles);
            
            seedRolePermissions("Bestrijder", new[] { 
                PermissionId.MapRead,
                PermissionId.MapContentRead,
                PermissionId.MapContentWrite,
                PermissionId.ReportReadWrite,
                PermissionId.Mobile
            });
            seedRolePermissions("Senior gebruiker", new[] {
                PermissionId.MapRead,
                PermissionId.MapContentRead,
                PermissionId.MapContentWrite,
                PermissionId.ReportReadWrite,
                PermissionId.TimeRegistrationPersonalReadWrite,
                PermissionId.TimeRegistrationManagementReadWrite,
                PermissionId.Mobile
            });
            seedRolePermissions("Beheerder", new[] {
                PermissionId.MapRead,
                PermissionId.MapContentRead,
                PermissionId.MapContentWrite,
                PermissionId.ReportReadWrite,
                PermissionId.TimeRegistrationPersonalReadWrite,
                PermissionId.UserReadWrite,
                PermissionId.Management,
                PermissionId.Mobile,
                PermissionId.AssignMaintainerRole
            });
            seedRolePermissions("Landelijke rapporteur", new[] {
                PermissionId.MapRead,
                PermissionId.MapContentRead,
                PermissionId.ReportReadWrite,
                PermissionId.ReadOnly
            });
            seedRolePermissions("Regionale functioneel beheerder", new[] {
                PermissionId.MapRead,
                PermissionId.MapContentRead,
                PermissionId.MapContentWrite,
                PermissionId.ReportReadWrite,
                PermissionId.UserReadWrite
            });
            seedRolePermissions("Landelijke functioneel beheerder", new[] {
                PermissionId.MapRead,
                PermissionId.MapContentRead,
                PermissionId.ReportReadWrite,
                PermissionId.UserReadWrite,
                PermissionId.Management
            });
            seedRolePermissions("Externe partij publiek", new[] {
                PermissionId.MapContentRead,
                PermissionId.ApiPublic
            });
            seedRolePermissions("Externe partij private", new[] {
                PermissionId.MapContentRead,
                PermissionId.ApiPublic,
                PermissionId.ApiPrivate
            });

            void seedRolePermissions(string roleName, PermissionId[] permissionIds) =>
                permissionIds.ToList().ForEach(permissionId =>
                    modelBuilder.Entity<RolePermission>()
                        .HasData(RolePermission.Create(roles.Single(x => x.Name == roleName).Id, permissionId)));
        }

        public List<Role> GetRoles() => new List<Role>
        {
            Role.Create("Bestrijder", "Trapper", 0)
                .WithId(Role.TrapperRoleId),
            Role.Create("Senior gebruiker", "Senior user", 1)
                .WithId(Role.SeniorUserId),
            Role.Create("Beheerder", "Maintainer", 2)
                .WithId(Role.MaintainerRoleId),
            Role.Create("Landelijke rapporteur", "National reporter", 3)
                .WithId(Role.NationalReporterId),
            Role.Create("Regionale functioneel beheerder", "Regions application manager", 4)
                .WithId(Role.RegionsApplicationManagerId),
            Role.Create("Landelijke functioneel beheerder", "National application manager", 5)
                .WithId(Role.NationalApplicationManagerId),
            Role.Create("Externe partij publiek", "External public interface", 6)
                .WithId(Role.ExternalPublicInterfaceId),
            Role.Create("Externe partij private", "External private interface", 7)
                .WithId(Role.ExternalPrivateInterfaceId),
        };
    }
}
