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
        //TODO HACER UN METODO EDITAR CAPTURA PASANDO EN EL BODY EL MODELO CAPTURA Y OTRO CON EL MODELO CAPTURA OBLIGATORIO
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
        [HttpGet("UsuariosConCapturas")]
        public async Task<ActionResult> GetUsuariosConCapturas()
        {
            Gestion gestion = new Gestion();
            try
            {
                gestion = await servicioBD.GetUsuariosConCapturas();
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
        [HttpGet("CapturasPorPropiedad/{propiedad}/{valor}")]
        public async Task<ActionResult> GetCapturasPorPropiedad(string propiedad, string valor)
        {
            Gestion gestion = new Gestion();
            try
            {
                gestion = await servicioBD.GetCapturasPorPropiedad(propiedad,valor);
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
        [HttpGet("CapturasPorLista")]
        public async Task<ActionResult> CapturasPorLista([FromQuery] List<string> propiedades, [FromQuery] List<string> valores)
        {
            Gestion gestion = new Gestion();
            try
            {
                gestion = await servicioBD.CapturasPorLista(propiedades, valores);
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
        //1-Check de como buscar las capturas, por propiedad nombre, tamaño etc
        //2-Segun las propiedades elegidas, buscar todas las capturas que coincidan con esas propiedades
        //3-Se pueden elegir varias propiedades a la vez, en ese caso mostrar individualmente, y en conjunto si todas coinciden
        //es decir, si por nombre coinciden 4, de esas 4 por tamaño ninguna, pues mostrar en un chart pie, las 4 que coinciden por nombre, y que ponga 0
        //en las que coinciden por tamaño,
        //4-Buscar en todas las capturas, las que coincidan por ej. con tamaño 20, es decir por propiedad y valor(CapturasPorLista)
        //
        //5-Si añado mas variables aqui, por ej gusano coreano, y buscar cuales coinciden con gusano coreano y 20 de tamaño
        //
        //hacer un metodo que busque individualmente por lista de propiedades y valores
        [HttpGet("CapturasPorEspecie/{nombreEspecie}")]
        public async Task<ActionResult> CapturasPorEspecie(string nombreEspecie)
        {
            Gestion gestion = new Gestion();
            try
            {
                gestion = await servicioBD.CapturasPorEspecie(nombreEspecie);
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
        [HttpGet("CapturasPorListaEnEspecie/{nombreEspecie}/{propiedades}/{valores}")]
        public async Task<ActionResult> CapturasPorListaEnEspecie(string nombreEspecie,List<string> propiedades, List<string> valores)
        {
            Gestion gestion = new Gestion();
            try
            {
                gestion = await servicioBD.CapturasPorListaEnEspecie(nombreEspecie,propiedades, valores);
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
        [HttpGet("CapturasdeUsuarioPorPropiedad/{id}/{propiedad}/{valor}")]
        public async Task<ActionResult> GetCapturasDeUnUsuarioPorPropiedad(int id,string propiedad,string valor)
        {
            Gestion gestion = new Gestion();
            try
            {
                gestion = await servicioBD.GetCapturasDeUnUsuarioPorPropiedad(id,propiedad,valor);
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
        [HttpPost("RegistroDeUsuario")]
        public async Task<ActionResult> RegistroDeUsuario([FromBody] UsuarioRegistroInsert modeloUsuario)
        {
            Gestion gestion = new Gestion();
            try
            {
                gestion = await servicioBD.RegistroDeUsuario(modeloUsuario);
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
        [HttpPost("CambiarContrasena")]
        public async Task<ActionResult> CambiarContraseña(int usuarioId,string nuevaContraseña)
        {
            Gestion gestion = new Gestion();
            try
            {
                gestion = await servicioBD.CambiarContraseña(usuarioId, nuevaContraseña);
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
        [HttpPost("AnadirCaptura")]
        public async Task<ActionResult> AñadirCapturaCompleta([FromBody] CapturaInsert modeloCaptura)
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
                    Console.WriteLine(gestion.error);
                    return NotFound(gestion);
                }
            }
            catch (Exception ex)
            {
                gestion.setError("Error de tipo {0}, mensaje: {1}", new List<dynamic>() { ex.GetType().Name, ex.Message });
            }
            return BadRequest(gestion);
        }
        [HttpPost("AnadirCapturaObligatorio")]
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
        [HttpPut("EditarCapturadeUsuario/{usuarioId}/{capturaEditarId}")]
        public async Task<ActionResult> EditarCapturaDeUnUsuario(int? usuarioId, int capturaEditarId, [FromBody] CapturaInsertObligatorio captura)
        {
            Gestion gestion = new Gestion();
            try
            {
                gestion = await servicioBD.EditarCapturaDeUnUsuario(usuarioId, capturaEditarId, captura);
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
        [HttpPut("EditarCapturaCompletadeUsuario/{usuarioId}/{capturaEditarId}")]
        public async Task<ActionResult> EditarCapturaCompletaDeUnUsuario(int? usuarioId, int capturaEditarId, [FromBody] CapturaInsert captura)
        {
            Gestion gestion = new Gestion();
            try
            {
                gestion = await servicioBD.EditarCapturaCompletaDeUnUsuario(usuarioId, capturaEditarId, captura);
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
        [HttpPut("EditarCapturasdeUsuarioPorPropiedad/{usuarioId}/{capturaId}/{propiedad}/{valor}")]
        public async Task<ActionResult> EditarCapturasDeUnUsuarioPorPropiedad(int usuarioId, int capturaId, string propiedad, string valor)
        {
            Gestion gestion = new Gestion();
            try
            {
                gestion = await servicioBD.EditarCapturasDeUnUsuarioPorPropiedad(usuarioId, capturaId, propiedad, valor);
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
        [HttpPut("EditarCapturasPertenecenAUsuario/{usuarioAntiguo}/{usuarioNuevo}/{capturaId}")]
        public async Task<ActionResult> EditarAQueUsuarioPerteneceCaptura(int usuarioAntiguo, int usuarioNuevo, int capturaId)
        {
            Gestion gestion = new Gestion();
            try
            {
                gestion = await servicioBD.EditarAQueUsuarioPerteneceCaptura(usuarioAntiguo, usuarioNuevo,capturaId);
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
