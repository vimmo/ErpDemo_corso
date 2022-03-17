using ErpDemoEF.Models;
using ErpDemoEF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErpDemoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientiController : ControllerBase
    {
        private readonly ILogger<ClientiController> _logger;
        private readonly IDBClientiService _dBClientiService;

        public ClientiController(ILogger<ClientiController> logger, 
            IDBClientiService dBClientiService)
        {
            _logger = logger;
            _dBClientiService = dBClientiService;
        }
        [HttpGet]
        public IEnumerable<Clienti> Get()
        {
            return _dBClientiService.LeggiListaClienti();
        }
        [HttpGet("{id}")]
        public Clienti Get(int id)
        {
            return _dBClientiService.LeggiCliente(id);
        }
        [HttpPost]
        public ActionResult<Clienti> PostCliente(Clienti cliente)
        {
            return _dBClientiService.CreaCliente(cliente);
        }
        [HttpPut("{id}")]
        public ActionResult<Clienti> PutCliente(int id, Clienti cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }
            _dBClientiService.ModificaCliente(cliente);
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult<Clienti> DeleteCliente(int id)
        {
            var cliente = _dBClientiService.LeggiCliente(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _dBClientiService.EliminaCliente(cliente);
            return NoContent();
        }
    }
}
