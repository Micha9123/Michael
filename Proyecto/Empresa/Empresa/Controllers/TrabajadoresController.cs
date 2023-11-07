using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Empresa.Models;

using Microsoft.AspNetCore.Cors;


namespace Empresa.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class TrabajadoresController : ControllerBase
    { 
        public readonly TrabajadoresPruebaContext _dbcontext;
        public TrabajadoresController(TrabajadoresPruebaContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]

        public IActionResult Lista()
        {
            List<Trabajadores> lista = new List<Trabajadores>();

            try
            {
                lista = _dbcontext.Trabajadores.Include(c => c.IdDepartamentoNavigation)
                    .Include(c => c.IdDistritoNavigation)
                    .Include(c => c.IdProvinciaNavigation).ToList();
               
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }
        }

        [HttpGet]
        [Route("Obtener/{idTrabajador:int}")]

        public IActionResult Obtener(int idTrabajador)
        {
            Trabajadores oTrabajador = _dbcontext.Trabajadores.Find(idTrabajador);

            if(oTrabajador == null){
                return BadRequest("Trabajador no encontrado");
            }
           
            try
            {
                oTrabajador = _dbcontext.Trabajadores.Include(c => c.IdDepartamentoNavigation)
                    .Include(c => c.IdDistritoNavigation)
                    .Include(c => c.IdProvinciaNavigation)
                    .Where(p => p.Id == idTrabajador).FirstOrDefault();

  
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oTrabajador });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = oTrabajador });
            }
        }

        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] Trabajadores objeto)
        {
            try 
            {
                _dbcontext.Trabajadores.Add(objeto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message});

            }

        }

        [HttpPut]
        [Route("Editar")]

        public IActionResult Editar([FromBody] Trabajadores objeto)
        {

            Trabajadores oTrabajador = _dbcontext.Trabajadores.Find(objeto.Id);

            if (oTrabajador == null)
            {
                return BadRequest("Trabajador no encontrado");
            }

            try
            {
                oTrabajador.TipoDocumento = objeto.TipoDocumento is null ? oTrabajador.TipoDocumento : objeto.TipoDocumento;
                oTrabajador.NumeroDocumento = objeto.NumeroDocumento is null ? oTrabajador.NumeroDocumento : objeto.NumeroDocumento;
                oTrabajador.Nombres = objeto.Nombres is null ? oTrabajador.Nombres : objeto.Nombres;
                oTrabajador.Sexo = objeto.Sexo is null ? oTrabajador.Sexo : objeto.Sexo;
                oTrabajador.IdDepartamento = objeto.IdDepartamento is null ? oTrabajador.IdDepartamento : objeto.IdDepartamento;
                oTrabajador.IdProvincia = objeto.IdProvincia is null ? oTrabajador.IdProvincia : objeto.IdProvincia;
                oTrabajador.IdDistrito = objeto.IdDistrito is null ? oTrabajador.IdDistrito : objeto.IdDistrito;

                _dbcontext.Trabajadores.Update(oTrabajador);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });

            }

        }

        [HttpDelete]
        [Route("Eliminar/{idTrabajador:int}")]
        public IActionResult Eliminar(int idTrabajador)
        {
            Trabajadores oTrabajador = _dbcontext.Trabajadores.Find(idTrabajador);

            if (oTrabajador == null)
            {
                return BadRequest("Trabajador no encontrado");
            }

            try
            {

                _dbcontext.Trabajadores.Remove(oTrabajador);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });

            }


        }
    }
}
