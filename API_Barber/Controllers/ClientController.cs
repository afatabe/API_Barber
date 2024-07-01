﻿using Barber.Application.DTOs;
using Barber.Application.DTOs.Register;
using Barber.Application.Interfaces;
using Barber.Domain.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Barber.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("add")]
        public async Task<ActionResult> Create([FromBody] ClientRegisterDTO clientRegisterDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var isValid = await _clientService.AddAsync(clientRegisterDTO);
                if (isValid)
                {
                    return CreatedAtAction(nameof(GetClientById), new { id = clientRegisterDTO.Name }, clientRegisterDTO);
                }
                return BadRequest("Client registration failed.");
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }
            catch (DomainExceptionValidation d)
            {
                return BadRequest(d.Message);
            }
        }

        [HttpPut("{id:int:min(1)}")]
        [Authorize]
        public async Task<ActionResult> Update([FromBody] ClientDTO clientDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var isSucceeded = await _clientService.UpdateAsync(clientDTO, id);
                if (isSucceeded)
                {
                    return Ok("Updated!");
                }
                return BadRequest("Update failed. Client dont exist.");
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }
            catch (DomainExceptionValidation d)
            {
                return BadRequest(d.Message);
            }
        }

        [HttpDelete("{id:int:min(1)}", Name = "Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var isDeleted = await _clientService.RemoveAsync(id);
                if (isDeleted)
                {
                    return Ok("Deleted successfully!");
                }
                return BadRequest("Deletion failed. Please verify all fields and try again.");
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }
            catch (DomainExceptionValidation d)
            {
                return BadRequest(d.Message);
            }
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClients()
        {
            var clients = await _clientService.GetAllAsync();
            return Ok(clients);
        }

        [HttpGet("{id:int:min(1)}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ClientDTO>> GetClientById(int id)
        {
            var client = await _clientService.GetByIdAsync(id);
            if (client is not null)
            {
                return Ok(client);
            }
            return NotFound("Client not found.");
        }
    }
}
