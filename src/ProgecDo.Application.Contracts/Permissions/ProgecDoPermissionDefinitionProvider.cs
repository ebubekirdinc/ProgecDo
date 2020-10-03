using ProgecDo.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ProgecDo.Permissions
{
    public class ProgecDoPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(ProgecDoPermissions.GroupName, L("Permission:ProgecDo"));

            //Define your own permissions here. Example:
            //myGroup.AddPermission(ProgecDoPermissions.MyPermission1, L("Permission:MyPermission1"));
            
            var projectsPermission = myGroup.AddPermission(ProgecDoPermissions.Projects.Default, L("Permission:Projects"));
            projectsPermission.AddChild(ProgecDoPermissions.Projects.Create, L("Permission:Projects.Create"));
            projectsPermission.AddChild(ProgecDoPermissions.Projects.Edit, L("Permission:Projects.Edit"));
            projectsPermission.AddChild(ProgecDoPermissions.Projects.Delete, L("Permission:Projects.Delete"));
            projectsPermission.AddChild(ProgecDoPermissions.Projects.AssignUserToProject, L("Permission:Projects.AssignUserToProject"));

        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ProgecDoResource>(name);
        }
    }
}
