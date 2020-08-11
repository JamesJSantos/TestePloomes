using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestePloomes.Context;
using TestePloomes.Services;
using TestePloomes.ViewModels;

namespace TestePloomes.Controllers
{
    [Route("/api")]
    public class ClienteController : Controller
    {

        private readonly ClienteService _service;

        public ClienteController(ClienteService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAll();

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet]
        [Route("get-by-id/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _service.GetById(id);

            if (response == null)
                return NotFound();

            return Ok(response);

        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] ClienteViewModel cliente)
        {
            if (cliente == null)
                return BadRequest();

            var response = await _service.Create(cliente);

            if (!response)
                return BadRequest(String.Format("CPF já cadastrado."));

            return Ok();
        }

        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> Edit([FromBody] ClienteViewModel cliente)
        {
            if (cliente == null)
            {
                return BadRequest();
            }

            var response = await _service.Edit(cliente);

            if (!response)
                return BadRequest(String.Format("CPF já cadastrado."));

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await _service.Delete(id);

            if (!response)
                return BadRequest();

            return Ok();
        }
    }
}
