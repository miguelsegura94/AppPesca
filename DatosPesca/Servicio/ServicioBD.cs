using DatosPesca.Context;
using GestorBaseDatos.GestionCarpeta;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static DatosPesca.Modelos.DatosPescaModelos;

namespace DatosPesca.Servicio
{
    public class ServicioBD
    {
        private readonly DatosPescaContext _context;

        public ServicioBD(DatosPescaContext context)
        {
            _context = context;
        }

        //TODO EDITAR DATOS EN CAPTURAS
        //TODO EDITAR A QUE USUARIO PERTENCE UNA CAPTURA
        public async Task<Gestion> GetUsuarios()
        {
            Gestion gestion = new Gestion();
            try
            {
                gestion.data = await _context.Usuarios.ToListAsync();
                if (gestion.data.Count > 0)
                {
                    gestion.Correct();
                }
                else
                {
                    gestion.setError("No hay usuarios");
                }
            }
            catch (Exception ex)
            {
                gestion.setError("Error de tipo {0}, mensaje: {1}", new List<dynamic>() { ex.GetType().Name, ex.Message });
            }
            return gestion;
        }
        public async Task<Gestion> GetCapturas()
        {
            Gestion gestion = new Gestion();
            try
            {
                gestion.data = await _context.Capturas.ToListAsync();
                if (gestion.data.Count > 0)
                {
                    gestion.Correct();
                }
                else
                {
                    gestion.setError("No hay capturas con ese ID");
                }
            }
            catch (Exception ex)
            {
                gestion.setError("Error de tipo {0}, mensaje: {1}", new List<dynamic>() { ex.GetType().Name, ex.Message });
            }
            return gestion;
        }
        public async Task<Gestion> GetCapturasDeUnUsuario(int id)
        {
            Gestion gestion = new Gestion();
            try
            {
                gestion.data = await _context.Capturas.Where(c => c.UsuarioId == id).ToListAsync();
                if (gestion.data.Count > 0)
                {
                    gestion.Correct();
                }
                else
                {
                    gestion.setError("No hay capturas en ese usuario");
                }
            }
            catch (Exception ex)
            {
                gestion.setError("Error de tipo {0}, mensaje: {1}", new List<dynamic>() { ex.GetType().Name, ex.Message });
            }
            return gestion;
        }
        public async Task<Gestion> AñadirCapturaCompleta(Captura modeloCaptura)
        {
            Gestion gestion = new Gestion();
            try
            {
                bool usuarioExiste = _context.Usuarios.Any(u => u.Id == modeloCaptura.UsuarioId);
                if (!usuarioExiste)
                {
                    gestion.setError("El usuario al que agregar la captura no existe.");
                    return gestion;
                }
                if (modeloCaptura.Tamaño < 0)
                {
                    gestion.setError("El tamaño no puede ser inferior a 0.");
                    return gestion;
                }
                if (modeloCaptura.Fecha == DateTime.MinValue)
                {
                    gestion.setError("La fecha no es válida.");
                    return gestion;
                }
                if (string.IsNullOrWhiteSpace(modeloCaptura.Localidad) || string.IsNullOrEmpty(modeloCaptura.Localidad))
                {
                    gestion.setError("La localidad no puede estar vacía.");
                    return gestion;
                }
                if (modeloCaptura.HoraAproximada < 0 || modeloCaptura.HoraAproximada > 23)
                {
                    gestion.setError("La hora no es correcta.");
                    return gestion;
                }
                if (string.IsNullOrWhiteSpace(modeloCaptura.Zona) || string.IsNullOrEmpty(modeloCaptura.Zona))
                {
                    gestion.setError("La zona no puede esta vacía.");
                    return gestion;
                }
                if (modeloCaptura.Profundidad < 0)
                {
                    gestion.setError("La profundidad no puede ser menor que 0.");
                    return gestion;
                }
                if (string.IsNullOrWhiteSpace(modeloCaptura.Oleaje) || string.IsNullOrEmpty(modeloCaptura.Oleaje))
                {
                    gestion.setError("El oleaje no puede esta vacío.");
                    return gestion;
                }
                if (string.IsNullOrWhiteSpace(modeloCaptura.TiempoClimatico) || string.IsNullOrEmpty(modeloCaptura.TiempoClimatico))
                {
                    gestion.setError("El tiempo climático no puede esta vacío.");
                    return gestion;
                }
                if (string.IsNullOrWhiteSpace(modeloCaptura.ClaridadAgua) || string.IsNullOrEmpty(modeloCaptura.ClaridadAgua))
                {
                    gestion.setError("La claridad del agua no puede esta vacía.");
                    return gestion;
                }
                if (string.IsNullOrWhiteSpace(modeloCaptura.EstiloPesca) || string.IsNullOrEmpty(modeloCaptura.EstiloPesca))
                {
                    gestion.setError("El estilo de pesca no puede esta vacío.");
                    return gestion;
                }
                if (modeloCaptura.Anzuelo)
                {
                    if (modeloCaptura.TamañoAnzuelo < 0)
                    {
                        gestion.setError("El tamaño del anzuelo no puede ser inferior a 0.");
                        return gestion;
                    }
                }
                if (modeloCaptura.Gusano)
                {
                    if (string.IsNullOrWhiteSpace(modeloCaptura.TipoGusano) || string.IsNullOrEmpty(modeloCaptura.TipoGusano))
                    {
                        gestion.setError("El nombre del gusano no puede esta vacío.");
                        return gestion;
                    }
                }
                if (modeloCaptura.Señuelo)
                {
                    if (string.IsNullOrWhiteSpace(modeloCaptura.TipoSeñuelo) || string.IsNullOrEmpty(modeloCaptura.TipoSeñuelo))
                    {
                        gestion.setError("El tipo de señuelo no puede esta vacío.");
                        return gestion;
                    }
                }
                await _context.Capturas.AddAsync(modeloCaptura);
                await _context.SaveChangesAsync();
                gestion.Correct("Captura añadida correctamente");
            }
            catch (Exception ex)
            {
                gestion.setError("Error de tipo {0}, mensaje: {1}", new List<dynamic>() { ex.GetType().Name, ex.Message });
            }
            return gestion;
        }
        public async Task<Gestion> CrearUsuario(UsuarioInsert modeloUsuario)
        {
            Gestion gestion = new Gestion();
            Usuario usuarioAdd = new Usuario();
            try
            {
                usuarioAdd.Nombre = modeloUsuario.Nombre;
                await _context.Usuarios.AddAsync(usuarioAdd);
                await _context.SaveChangesAsync();
                gestion.Correct("Usuario creado correctamente");
            }
            catch (Exception ex)
            {
                gestion.setError("Error de tipo {0}, mensaje: {1}", new List<dynamic>() { ex.GetType().Name, ex.Message });
            }
            return gestion;
        }
        public async Task<Gestion> AñadirCapturaObligatorio(CapturaInsertObligatorio modeloCaptura)
        {
            Gestion gestion = new Gestion();
            Captura captura = new Captura();
            try
            {
                bool usuarioExiste = _context.Usuarios.Any(u => u.Id == modeloCaptura.UsuarioId);
                if (!usuarioExiste)
                {
                    gestion.setError("El usuario al que agregar la captura no existe.");
                    return gestion;
                }
                if (string.IsNullOrWhiteSpace(modeloCaptura.NombreEspecie) || string.IsNullOrEmpty(modeloCaptura.NombreEspecie))
                {
                    gestion.setError("El nombre no puede estar vacío.");
                    return gestion;
                }
                if (string.IsNullOrWhiteSpace(modeloCaptura.EstiloPesca) || string.IsNullOrEmpty(modeloCaptura.EstiloPesca))
                {
                    gestion.setError("El estilo de pesca no puede estar vacío.");
                    return gestion;
                }
                if (modeloCaptura.Tamaño < 0)
                {
                    gestion.setError("El tamaño no puede ser inferior a 0.");
                    return gestion;
                }
                captura.UsuarioId = modeloCaptura.UsuarioId;
                captura.NombreEspecie = modeloCaptura.NombreEspecie;
                captura.Tamaño = modeloCaptura.Tamaño;
                captura.EstiloPesca = modeloCaptura.EstiloPesca;
                await _context.Capturas.AddAsync(captura);
                await _context.SaveChangesAsync();
                gestion.Correct("Captura añadida correctamente");
            }
            catch (Exception ex)
            {
                gestion.setError("Error de tipo {0}, mensaje: {1}", new List<dynamic>() { ex.GetType().Name, ex.Message });
            }
            return gestion;
        }
        public async Task<Gestion> EliminarCaptura(int id)
        {
            Gestion gestion = new Gestion();
            try
            {
                Captura capturaDelete = await _context.Capturas.FirstOrDefaultAsync(c=>c.CapturaId==id);
                if (capturaDelete!=null)
                {
                    _context.Capturas.Remove(capturaDelete);
                    await _context.SaveChangesAsync();
                    gestion.Correct("Captura eliminada correctamente");
                }
                else
                {
                    gestion.setError("No hay capturas con ese ID");
                }
            }
            catch (Exception ex)
            {
                gestion.setError("Error de tipo {0}, mensaje: {1}", new List<dynamic>() { ex.GetType().Name, ex.Message });
            }
            return gestion;
        }
        public async Task<Gestion> EliminarUsuario(int id)
        {
            Gestion gestion = new Gestion();
            try
            {
                Usuario usuarioDelete = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
                if (usuarioDelete != null)
                {
                    _context.Usuarios.Remove(usuarioDelete);
                    await _context.SaveChangesAsync();
                    gestion.Correct("Usuario eliminado correctamente");
                }
                else
                {
                    gestion.setError("No hay usuarios con ese ID");
                }
            }
            catch (Exception ex)
            {
                gestion.setError("Error de tipo {0}, mensaje: {1}", new List<dynamic>() { ex.GetType().Name, ex.Message });
            }
            return gestion;
        }
        public async Task<Gestion> EditarNombreUsuario(int id,string nombre)
        {
            Gestion gestion = new Gestion();
            try
            {
                Usuario usuarioEditar = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
                if (usuarioEditar != null)
                {
                    if (usuarioEditar.Nombre == nombre)
                    {
                        gestion.setError("El nombre debe ser distinto");
                        return gestion;
                    }
                    usuarioEditar.Nombre = nombre;
                    _context.Usuarios.Update(usuarioEditar);
                    await _context.SaveChangesAsync();
                    gestion.Correct($"Nombre de usuario editado correctamente. Nuevo nombre: {nombre}");
                }
                else
                {
                    gestion.setError("No hay usuarios con ese ID");
                }
            }
            catch (Exception ex)
            {
                gestion.setError("Error de tipo {0}, mensaje: {1}", new List<dynamic>() { ex.GetType().Name, ex.Message });
            }
            return gestion;
        }
    }
}
