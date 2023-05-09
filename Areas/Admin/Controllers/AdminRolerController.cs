using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using MvcWebIdentity.Areas.Admin.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MvcWebIdentity.Areas.Admin.Controllers;


[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminRolerController : Controller
{

    private RoleManager<IdentityRole> roleManager;
    private UserManager<IdentityUser> userManager;

    public AdminRolerController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        this.roleManager = roleManager;
        this.userManager = userManager;
    }

    public ViewResult Index() => View(roleManager.Roles);
    public ViewResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create([Required] string nome)
    {
        if (ModelState.IsValid)
        {
            IdentityResult result = await roleManager.CreateAsync(new IdentityRole(nome));
            if (result.Succeeded)
                return RedirectToAction("Index");
            else
                Errors(result);
        }

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Update(string id)
    {
        IdentityRole role = await roleManager.FindByIdAsync(id);

        List<IdentityUser> membros = new List<IdentityUser>();
        List<IdentityUser> semMembros = new List<IdentityUser>();
       
        //List<IdentityUser> list = new List<IdentityUser>();

        if(role != null)
        {
            foreach (IdentityUser user in userManager.Users.ToList())
            {                
                var list = await userManager.IsInRoleAsync(user, role.Name) ? membros : semMembros;

                list.Add(user);
            }

            return View(new RoleEdit
            {
                Role = role,
                Membros = membros,
                SemMembros = semMembros
            });
        }
        else
        {
            return View();
        }
       
    }

    [HttpPost]
    public async Task<IActionResult> Update(RoleModification model)
    {
        IdentityResult Result;

        if (ModelState.IsValid)
        {
            //*GERENCIA QUANDO VAI INCLUIR O USUSARIO
            foreach (string userId in model.AddIds ?? new string[] { })
            {
                IdentityUser user = await userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    Result = await userManager.AddToRoleAsync(user, model.NameRole);

                    if (!Result.Succeeded)
                        Errors(Result);
                }
            }

            //*AQUI É QUANDO REMOVE OS USUARIOS
            foreach (string userId in model.DeletarIds ?? new string[] { })
            {
                IdentityUser user = await userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    Result = await userManager.RemoveFromRoleAsync(user, model.NameRole);

                    if (!Result.Succeeded)
                        Errors(Result);
                }
            }
        }

        if (ModelState.IsValid)
            return RedirectToAction("Index");
        else
            return await Update(model.RoleId);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        IdentityRole role = await roleManager.FindByIdAsync(id);

        if (role == null)
        {
            ModelState.AddModelError("", "Regra não encontrada");
            return View("Index", roleManager.Roles);
        }

        return View(role);

    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var role = await roleManager.FindByIdAsync(id);

        if (role != null)
        {
            IdentityResult result = await roleManager.DeleteAsync(role);

            if (result.Succeeded)
                return RedirectToAction("Index");
            else
                Errors(result);

        }
        else
        {
            ModelState.AddModelError("", "Regra não encontrada");
        }

        return View("Index", roleManager.Roles);

    }

    private void Errors(IdentityResult result)
    {
        foreach (IdentityError erro in result.Errors)
        {
            ModelState.AddModelError("", erro.Description);
        }
    }
}
