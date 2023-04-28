namespace MvcWebIdentity.Services
{
    public interface ICriaRegrasUsuario
    {
        Task CriaRegrasAsync();
        Task CriaUsuarioComRegraAsync();
    }
}
