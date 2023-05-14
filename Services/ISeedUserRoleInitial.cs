namespace MvcWebIdentity.Services
{
    public interface ISeedUserRoleInitial
    {
        Task CriaRoleAsync();
        Task CriaUserComRoleAsync();
    }
}
