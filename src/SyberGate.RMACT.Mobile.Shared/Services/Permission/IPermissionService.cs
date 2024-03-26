namespace SyberGate.RMACT.Services.Permission
{
    public interface IPermissionService
    {
        bool HasPermission(string key);
    }
}