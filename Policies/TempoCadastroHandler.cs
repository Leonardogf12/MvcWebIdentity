using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualBasic;

namespace MvcWebIdentity.Policies
{
    public class TempoCadastroHandler : AuthorizationHandler<TempoCadastroRequirement>
    {
        //*FUNCAO QUE VAI GERENCIAR A POLITICA PERSONALIZADA.

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        TempoCadastroRequirement requirement)
        {

            //*IF RESPONSAVEL DE VALIDADER A CLAIM 'CadastradoEm'
            if (context.User.HasClaim(c => c.Type == "CadastradoEm"))
            {
                var data = context.User.FindFirst(c => c.Type == "CadastradoEm").Value;

                var dataCadastro = DateTime.Parse(data);

                var tempoCadastro = await Task.Run(() =>
                            (DateTime.Now.Date - dataCadastro.Date).TotalDays);
                
                var tempoEmAnos = tempoCadastro / 360;

                if(tempoEmAnos >= requirement.TempoCadastroMinimo)
                {
                    context.Succeed(requirement);
                }

                return;
            }
        }
    }
}
