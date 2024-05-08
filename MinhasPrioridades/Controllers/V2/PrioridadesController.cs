using ApplicationPrioridadesAPP.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MinhasPrioridades.Controllers.V2
{
    [Route("api/v2/categoria")]
    [ApiController]
    [Authorize]
    public class PrioridadesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PrioridadesController> _logger;
        public PrioridadesController(IMediator mediator,
                                   ILogger<PrioridadesController> logger)
        {

            _logger = logger;
            _mediator = mediator;
        }
    }
}

