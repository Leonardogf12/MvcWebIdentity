using Microsoft.AspNetCore.Authorization;

namespace MvcWebIdentity.Policies
{
    public class TempoCadastroRequirement : IAuthorizationRequirement
    {
        //*PROPRIEDADES A QUAL A POLITICA PERSONALIZADA UTILIZARA PARA VALIDACOES.

        public int TempoCadastroMinimo { get; }

        public TempoCadastroRequirement(int tempoCadastroMinimo)
        {
           TempoCadastroMinimo = tempoCadastroMinimo;
        }

    }
}
