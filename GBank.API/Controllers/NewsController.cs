using GBank.Application.Contracts.Persistence;
using GBank.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository nr;

        public NewsController(INewsRepository nr)
        {
            this.nr = nr;
        }
        // GET: api/<NewsController>
        [HttpGet]
        public async Task<List<News>> Get()
        {
            return (List<News>)await nr.GetAllAsync();
        }

        // GET api/<NewsController>/5
        [HttpGet("{id}")]
        public async Task<News> Get(int id)
        {
            return await nr.GetByIdAsync(id);
        }
        [HttpGet("GetSomeCount")]
        public async Task<List<News>> GetSomeCount(int count)
        {
            return await nr.GetSomeCountOfNews(count);
        }

        [HttpPost]
        public async Task<int> Add(News n)
        {
            
            return (await nr.AddAsync(n)).ID;
        }

    }
}
