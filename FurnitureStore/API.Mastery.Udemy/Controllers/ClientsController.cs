﻿using FurnitureStoreData;
using FurnitureStoreModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Mastery.Udemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly APIcontext _context;//varaible global

        public ClientsController(APIcontext context)
        {
            //dependencia inyectada
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<Client>> Get()
        {
            return await _context.Clients.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);

            if(client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Client client)
        {
           await _context.Clients.AddAsync(client);
           await _context.SaveChangesAsync();

            return CreatedAtAction("Post", client.Id, client);
        }
        [HttpPut]
        public async Task<IActionResult> Put(Client client)
        {
            _context.Clients.Update(client);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult>Delete(Client client)
        {
            if (client == null) return NotFound();

            _context.Clients.Remove(client);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
