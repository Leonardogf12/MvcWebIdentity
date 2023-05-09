using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace MvcWebIdentity.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminUsuariosController : Controller
{
    private UserManager<IdentityUser> userManager;

    public AdminUsuariosController(UserManager<IdentityUser> userManager)
    {
        this.userManager = userManager;
    }

    public ActionResult Index()
    {
        var users = userManager.Users;
        return View(users);
    }

    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await userManager.FindByIdAsync(id);

        if (user == null)
        {
            ViewBag.ErrorMessage = $"Usuário com Id = {id} não foi encontrado.";
            return View("NotFound");
        }
        else
        {

            var result = await userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var erro in result.Errors)
            {
                ModelState.AddModelError("", erro.Description);
            }

            return View("Index");
        }
    }
}
