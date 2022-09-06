using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RDIServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] LoginViewModel loginViewModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (!_InterfaceUsuarioApp.IsUsuarioExiste(loginViewModel.Username))
                    {
                        await _InterfaceUsuarioApp.AddUsuario(loginViewModel);
                        return CreatedAtAction(nameof(GetById), new { id = loginViewModel.Id }, loginViewModel);
                    }
                    else
                        return BadRequest(new { message = $"Usuário {loginViewModel.Username} já está cadastrado!" });

                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao incluir usuario " + ex.Message);
            }

        }
    }
}
