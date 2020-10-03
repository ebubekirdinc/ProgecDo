namespace ProgecDo.Permissions
{
    public static class ProgecDoPermissions
    {
        public const string GroupName = "ProgecDo";

        //Add your own permission names. Example:
        //public const string MyPermission1 = GroupName + ".MyPermission1";

        public static class Projects
        {
            public const string Default = GroupName + ".Projects";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
            public const string AssignUserToProject = Default + ".AssignUserToProject";
        }
    }
}