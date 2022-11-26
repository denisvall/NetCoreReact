using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreReact.Models;

namespace NetCoreReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly DbdemosContext _dbdemosContext;

        public TareaController(DbdemosContext dbdemosContext)
        {
            _dbdemosContext = dbdemosContext;
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult> Listar()
        {
            List<DetTarea> listTareas = _dbdemosContext
                .DetTareas
                .OrderByDescending(t => t.Id)
                .ThenBy(t => t.FechaRegistro).ToList();

            return StatusCode(StatusCodes.Status200OK, listTareas);

        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] DetTarea model)
        {
            await _dbdemosContext.DetTareas.AddAsync(model);
            await _dbdemosContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, "OK");
        }

        [HttpDelete]
        [Route("Cerrar/{id:int}")]
        public async Task<IActionResult> Cerrar(int id)
        {
            DetTarea detTarea = _dbdemosContext.DetTareas.Find(id);

            _dbdemosContext.DetTareas.Remove(detTarea);

            await _dbdemosContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, "OK");
        }
    }
}
