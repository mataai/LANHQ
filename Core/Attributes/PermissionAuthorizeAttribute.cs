namespace Core.Attributes
{

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class PermissionAuthorizeAttribute : Attribute
    {
        public PermissionAuthorizeAttribute(string permission)
        {

        }
    }
}
