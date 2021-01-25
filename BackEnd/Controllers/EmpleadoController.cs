using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmpleadoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Empleado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleado()
        {
            return await _context.Empleado.ToListAsync();
        }

        // GET: api/Empleado
        [HttpGet("list")]
        public IActionResult GetEmpleadoPaginated([FromQuery] string searchText, [FromQuery] int? page, [FromQuery] int pageSize = 5)
        {
            var query = string.IsNullOrEmpty(searchText) ? _context.Empleado
                               : _context.Empleado.Where(e =>
                                e.Nombre.ToLower().Contains(searchText.ToLower()) ||
                                e.Apellido.ToLower().Contains(searchText.ToLower()));

            int totalCount = query.Count();

            double totalSalaries = (double)_context.Empleado
                .Select(x => x.Salario).Sum();

            double femaleSalaries = (double)_context.Empleado
                .Where(y => y.Genero == "Femenino")
                .Select(x => x.Salario).Sum();

            double maleSalaries = (double)_context.Empleado
                .Where(y => y.Genero == "Masculino")
                .Select(x => x.Salario).Sum();

            PageResult<Empleado> result = new PageResult<Empleado>
            {
                Count = totalCount,
                PageIndex = page ?? 1,
                PageSize = pageSize,
                Items = query.Skip((page - 1 ?? 0) * pageSize).Take(pageSize).ToList()
            };
            return Ok(result);
        }
    }
}