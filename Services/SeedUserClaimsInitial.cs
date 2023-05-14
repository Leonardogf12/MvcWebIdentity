using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace MvcWebIdentity.Services;

public class SeedUserClaimsInitial : ISeedUserClaimsInitial
{

    private readonly UserManager<IdentityUser> _userManager;

    public SeedUserClaimsInitial(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task SeedUserClaims()
    {
        try
        {
            //*COMENTADO PARA CRIAR AS CLAIMS DAS POLITICAS PERSONALIZADAS.
            /*
            //*USER 1
            IdentityUser user = await _userManager.FindByEmailAsync("admin@localhost");
            
            if (user is not null)
            {
                var claimsList = (await _userManager.GetClaimsAsync(user))
                                                    .Select(x => x.Type);

                if (!claimsList.Contains("CadastradoEm"))
                {
                    var claimsResult = await _userManager.AddClaimAsync(user, 
                                        new Claim("CadastradoEm", "09/15/2020"));
                }

                if (!claimsList.Contains("IsAdmin"))
                {
                    var claimsResult = await _userManager.AddClaimAsync(user,
                                        new Claim("IsAdmin", "true"));
                }
            }

            //*USER 2
            IdentityUser user2 = await _userManager.FindByEmailAsync("usuario@localhost");

            if (user2 is not null)
            {
                var claimsList = (await _userManager.GetClaimsAsync(user2))
                                                    .Select(x => x.Type);

                if (!claimsList.Contains("IsAdmin"))
                {
                    var claimsResult = await _userManager.AddClaimAsync(user2,
                                        new Claim("IsAdmin", "false"));
                }

                if (!claimsList.Contains("IsFuncionario"))
                {
                    var claimsResult = await _userManager.AddClaimAsync(user2,
                                        new Claim("IsFuncionario", "true"));
                }
            }

            */

            //*usuario 1
            IdentityUser user1 = await _userManager.FindByEmailAsync("gerente@localhost");

            if (user1 is not null)
            {
                var claimsList = (await _userManager.GetClaimsAsync(user1))
                                                    .Select(x => x.Type);

                if (!claimsList.Contains("CadastradoEm"))
                {
                    var claimsResult = await _userManager.AddClaimAsync(user1,
                                        new Claim("CadastradoEm", "03/03/2021"));
                }
            }
            //*usuario 2
            IdentityUser user2 = await _userManager.FindByEmailAsync("usuario@localhost");

            if (user2 is not null)
            {
                var claimsList = (await _userManager.GetClaimsAsync(user2))
                                                    .Select(x => x.Type);

                if (!claimsList.Contains("CadastradoEm"))
                {
                    var claimsResult = await _userManager.AddClaimAsync(user2,
                                        new Claim("CadastradoEm", "01/01/2020"));
                }
            }            
        }
        catch (Exception)
        {
            throw;
        }
    }
}
