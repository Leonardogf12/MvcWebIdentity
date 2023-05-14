using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcWebIdentity.Models;

namespace MvcWebIdentity.Controllers
{
    public class AccountController : Controller
    {
        //*É RESPONSAVEL POR GERENCIAR AS OPERACOES DE CIRACAO, LEITURA, ATUALIZACAO E EXCLUSAO DE USUARIOS.
        private readonly UserManager<IdentityUser> userManager;

        //*É RESPONSAVEL POR GERENCIAR O PROCESSSO DE AUTENTICACAO DE USUARIOS EM UM APLICATIVO.
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signManager)
        {
            this.userManager = userManager;
            this.signInManager = signManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistroViewModel model)
        {

            if (ModelState.IsValid)
            {
                //*copia os dados do RegistroViewModel para o Identity
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };

                //*armazena os dados do usuario na tabela do AspNetUsers
                var result = await userManager.CreateAsync(user, model.Password);

                //*se o usuarui foi criado com sucesso entao faz o login usando o servico
                // de SignInManager e redireciona para o Metodo Action Index
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                //*caso aconteca um erro entao capturo aqui neste foreach
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    model.Remerberme,
                    false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Login Invalido");               
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("Account/AccessDenied")]
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}
