using System.Text.RegularExpressions;
using DatosPesca.Servicio;
using GestorBaseDatos.GestionCarpeta;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Collections.Specialized.BitVector32;
using static DatosPesca.Modelos.DatosPescaModelos;

namespace DatosPesca.Controllers
{
    public class DatosPescaController : Controller
    {
        private readonly ServicioBD servicioBD;
        public DatosPescaController(ServicioBD _servicioBD)
        {
            servicioBD = _servicioBD;
        }
        //TODO METODO GET CON TODAS LAS CAPTURAS DE UN USUARIO POR propiedad GENERICA
        //TODO METODO GET CON TODAS LAS CAPTURAS POR propiedad GENERICA
        [HttpGet("Usuarios")]
        public async Task<ActionResult> GetUsuarios()
        {
            Gestion gestion = new Gestion();
            try
            {
                gestion = await servicioBD.GetUsuarios();
                if (gestion.isCorrect())
                {
                    return Ok(gestion);
                }
                else
                {
                    return NotFound(gestion);
                }
            }
            catch (Exception ex)
            {
                gestion.setError("Error de tipo {0}, mensaje: {1}", new List<dynamic>() { ex.GetType().Name, ex.Message });
            }


            return BadRequest(gestion);
        }
        [HttpGet("Capturas")]
        public async Task<ActionResult> GetCapturas()
        {
            Gestion gestion = new Gestion();
            try
            {
                gestion = await servicioBD.GetCapturas();
                if (gestion.isCorrect())
                {
                    return Ok(gestion);
                }
                else
                {
                    return NotFound(gestion);
                }
            }
            catch (Exception ex)
            {
                gestion.setError("Error de tipo {0}, mensaje: {1}", new List<dynamic>() { ex.GetType().Name, ex.Message });
            }


            return BadRequest(gestion);
        }
        [HttpGet("CapturasdeUsuario/{id}")]
        public async Task<ActionResult> GetCapturasDeUnUsuario(int id)
        {
            Gestion gestion = new Gestion();
            try
            {
                gestion = await servicioBD.GetCapturasDeUnUsuario(id);
                if (gestion.isCorrect())
                {
                    return Ok(gestion);
                }
                else
                {
                    return NotFound(gestion);
                }
            }
            catch (Exception ex)
            {
                gestion.setError("Error de tipo {0}, mensaje: {1}", new List<dynamic>() { ex.GetType().Name, ex.Message });
            }
            return BadRequest(gestion);
        }
        [HttpPost("Crear usuario")]
        public async Task<ActionResult> CrearUsuario([FromBody] UsuarioInsert modeloUsuario)
        {
            Gestion gestion = new Gestion();
            try
            {
                gestion = await servicioBD.CrearUsuario(modeloUsuario);
                if (gestion.isCorrect())
                {
                    return Ok(gestion);
                }
                else
                {
                    return NotFound(gestion);
                }
            }
            catch (Exception ex)
            {
                gestion.setError("Error de tipo {0}, mensaje: {1}", new List<dynamic>() { ex.GetType().Name, ex.Message });
            }
            return BadRequest(gestion);
        }
        [HttpPost("Añadir Captura")]
        public async Task<ActionResult> AñadirCapturaCompleta([FromBody] Captura modeloCaptura)
        {
            Gestion gestion = new Gestion();
            try
            {
                gestion= await servicioBD.AñadirCapturaCompleta(modeloCaptura);
                if (gestion.isCorrect())
                {
                    return Ok(gestion);
                }
                else
                {
                    return NotFound(gestion);
                }
            }
            catch (Exception ex)
            {
                gestion.setError("Error de tipo {0}, mensaje: {1}", new List<dynamic>() { ex.GetType().Name, ex.Message });
            }
            return BadRequest(gestion);
        }
        [HttpPost("Añadir Captura Obligatorio")]
        public async Task<ActionResult> AñadirCapturaObligatorio([FromBody] CapturaInsertObligatorio modeloCaptura)
        {
            Gestion gestion = new Gestion();
            try
            {
                gestion = await servicioBD.AñadirCapturaObligatorio(modeloCaptura);
                if (gestion.isCorrect())
                {
                    return Ok(gestion);
                }
                else
                {
                    return NotFound(gestion);
                }
            }
            catch (Exception ex)
            {
                gestion.setError("Error de tipo {0}, mensaje: {1}", new List<dynamic>() { ex.GetType().Name, ex.Message });
            }
            return BadRequest(gestion);
        }
        [HttpDelete("EliminarUsuario/{id}")]
        public async Task<ActionResult> EliminarUsuario(int id)
        {
            Gestion gestion = new Gestion();
            try
            {
                gestion = await servicioBD.EliminarUsuario(id);
                if (gestion.isCorrect())
                {
                    return Ok(gestion);
                }
                else
                {
                    return NotFound(gestion);
                }
            }
            catch (Exception ex)
            {
                gestion.setError("Error de tipo {0}, mensaje: {1}", new List<dynamic>() { ex.GetType().Name, ex.Message });
            }
            return BadRequest(gestion);
        }
        [HttpDelete("EliminarCaptura/{id}")]
        public async Task<ActionResult> EliminarCaptura(int id)
        {
            Gestion gestion = new Gestion();
            try
            {
                gestion = await servicioBD.EliminarCaptura(id);
                if (gestion.isCorrect())
                {
                    return Ok(gestion);
                }
                else
                {
                    return NotFound(gestion);
                }
            }
            catch (Exception ex)
            {
                gestion.setError("Error de tipo {0}, mensaje: {1}", new List<dynamic>() { ex.GetType().Name, ex.Message });
            }
            return BadRequest(gestion);
        }
        [HttpPut("EditarNombreUsuario/{id}/{nombre}")]
        public async Task<ActionResult> EditarNombreUsuario(int id,string nombre)
        {
            Gestion gestion = new Gestion();
            try
            {
                gestion = await servicioBD.EditarNombreUsuario(id,nombre);
                if (gestion.isCorrect())
                {
                    return Ok(gestion);
                }
                else
                {
                    return NotFound(gestion);
                }
            }
            catch (Exception ex)
            {
                gestion.setError("Error de tipo {0}, mensaje: {1}", new List<dynamic>() { ex.GetType().Name, ex.Message });
            }
            return BadRequest(gestion);
        }
    }
}
