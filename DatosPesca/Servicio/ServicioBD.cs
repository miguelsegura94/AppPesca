using DatosPesca.Context;
using GestorBaseDatos.GestionCarpeta;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static DatosPesca.Modelos.DatosPescaModelos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DatosPesca.Servicio
{
    public class ServicioBD
    {
        private readonly DatosPescaContext _context;

        public ServicioBD(DatosPescaContext context)
        {
            _context = context;
        }
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
        public async Task<Gestion> GetCapturasPorPropiedad(string propiedad, string valor)
        {
            Gestion gestion = new Gestion();
            try
            {
                switch (propiedad.ToLower())
                {
                    case "especie":
                        gestion.data = await _context.Capturas.Where(c => c.NombreEspecie == valor).ToListAsync();
                        break;
                    case "localidad":
                        gestion.data = await _context.Capturas.Where(c => c.Localidad == valor).ToListAsync();
                        break;
                    case "tamaño":
                        if (int.TryParse(valor, out int tamañoPez))
                            gestion.data = await _context.Capturas.Where(c => c.Tamaño == tamañoPez).ToListAsync();
                        else
                            gestion.setError("El valor para 'tamaño' debe ser numérico.");
                        return gestion;
                    case "fecha":
                        if (DateTime.TryParse(valor, out DateTime fecha))
                            gestion.data = await _context.Capturas.Where(c => c.Fecha == fecha).ToListAsync();
                        else
                            gestion.setError("El valor para 'fecha' debe ser una fecha.");
                        return gestion;
                    case "hora":
                        if (int.TryParse(valor, out int hora))
                            gestion.data = await _context.Capturas.Where(c => c.HoraAproximada == hora).ToListAsync();
                        else
                            gestion.setError("El valor para 'hora' debe ser numérico.");
                        return gestion;
                    case "zona":
                        gestion.data = await _context.Capturas.Where(c => c.Zona == valor).ToListAsync();
                        break;
                    case "profundidad":
                        if (int.TryParse(valor, out int profundidad))
                            gestion.data = await _context.Capturas.Where(c => c.Profundidad == profundidad).ToListAsync();
                        else
                            gestion.setError("El valor para 'profundidad' debe ser numérico.");
                        return gestion;
                    case "oleaje":
                        gestion.data = await _context.Capturas.Where(c => c.Oleaje == valor).ToListAsync();
                        break;
                    case "estilo":
                        gestion.data = await _context.Capturas.Where(c => c.EstiloPesca == valor).ToListAsync();
                        break;
                    case "clima":
                        gestion.data = await _context.Capturas.Where(c => c.TiempoClimatico == valor).ToListAsync();
                        break;
                    case "claridad agua":
                        gestion.data = await _context.Capturas.Where(c => c.ClaridadAgua == valor).ToListAsync();
                        break;
                    case "tamaño anzuelo":
                        if (int.TryParse(valor, out int tamañoAnzuelo))
                            gestion.data = await _context.Capturas.Where(c => c.TamañoAnzuelo == tamañoAnzuelo).ToListAsync();
                        else
                            gestion.setError("El valor para 'tamaño anzuelo' debe ser numérico.");
                        return gestion;
                    case "tipo gusano":
                        gestion.data = await _context.Capturas.Where(c => c.TipoGusano == valor).ToListAsync();
                        break;
                    case "tamaño bajo":
                        if (int.TryParse(valor, out int tamañoBajo))
                            gestion.data = await _context.Capturas.Where(c => c.TamañoBajo == tamañoBajo).ToListAsync();
                        else
                            gestion.setError("El valor para 'tamaño bajo' debe ser numérico.");
                        return gestion;
                    case "tipo señuelo":
                        gestion.data = await _context.Capturas.Where(c => c.TipoSeñuelo == valor).ToListAsync();
                        break;
                    default:
                        gestion.setError($"La propiedad '{propiedad}' no es válida.");
                        return gestion;
                }
                if (gestion.data.Count > 0)
                {
                    gestion.Correct();
                }
                else
                {
                    gestion.setError($"En {propiedad} no hay capturas con valor {valor}.");
                }
            }
            catch (Exception ex)
            {
                gestion.setError("Error de tipo {0}, mensaje: {1}", new List<dynamic>() { ex.GetType().Name, ex.Message });
            }
            return gestion;
        }
        public async Task<Gestion> GetCapturasDeUnUsuarioPorPropiedad(int id, string propiedad, string valor)
        {

            Gestion gestion = new Gestion();
            try
            {
                bool usuarioExiste = _context.Capturas.Any(c => c.UsuarioId == id);
                if (!usuarioExiste)
                {
                    gestion.setError("El usuario que buscas no existe.");
                    return gestion;
                }
                switch (propiedad.ToLower())
                {
                    case "especie":
                        gestion.data = await _context.Capturas.Where(c => c.NombreEspecie == valor && c.UsuarioId == id).ToListAsync();
                        break;
                    case "localidad":
                        gestion.data = await _context.Capturas.Where(c => c.Localidad == valor && c.UsuarioId == id).ToListAsync();
                        break;
                    case "tamaño":
                        if (int.TryParse(valor, out int tamañoPez))
                            gestion.data = await _context.Capturas.Where(c => c.Tamaño == tamañoPez && c.UsuarioId == id).ToListAsync();
                        else
                            gestion.setError("El valor para 'tamaño' debe ser numérico.");
                        return gestion;
                    case "fecha":
                        if (DateTime.TryParse(valor, out DateTime fecha))
                            gestion.data = await _context.Capturas.Where(c => c.Fecha == fecha && c.UsuarioId == id).ToListAsync();
                        else
                            gestion.setError("El valor para 'fecha' debe ser una fecha.");
                        return gestion;
                    case "hora":
                        if (int.TryParse(valor, out int hora))
                            gestion.data = await _context.Capturas.Where(c => c.HoraAproximada == hora && c.UsuarioId == id).ToListAsync();
                        else
                            gestion.setError("El valor para 'hora' debe ser numérico.");
                        return gestion;
                    case "zona":
                        gestion.data = await _context.Capturas.Where(c => c.Zona == valor && c.UsuarioId == id).ToListAsync();
                        break;
                    case "profundidad":
                        if (int.TryParse(valor, out int profundidad))
                            gestion.data = await _context.Capturas.Where(c => c.Profundidad == profundidad && c.UsuarioId == id).ToListAsync();
                        else
                            gestion.setError("El valor para 'profundidad' debe ser numérico.");
                        return gestion;
                    case "oleaje":
                        gestion.data = await _context.Capturas.Where(c => c.Oleaje == valor && c.UsuarioId == id).ToListAsync();
                        break;
                    case "estilo":
                        gestion.data = await _context.Capturas.Where(c => c.EstiloPesca == valor && c.UsuarioId == id).ToListAsync();
                        break;
                    case "clima":
                        gestion.data = await _context.Capturas.Where(c => c.TiempoClimatico == valor && c.UsuarioId == id).ToListAsync();
                        break;
                    case "claridad agua":
                        gestion.data = await _context.Capturas.Where(c => c.ClaridadAgua == valor && c.UsuarioId == id).ToListAsync();
                        break;
                    case "tamaño anzuelo":
                        if (int.TryParse(valor, out int tamañoAnzuelo))
                            gestion.data = await _context.Capturas.Where(c => c.TamañoAnzuelo == tamañoAnzuelo && c.UsuarioId == id).ToListAsync();
                        else
                            gestion.setError("El valor para 'tamaño anzuelo' debe ser numérico.");
                        return gestion;
                    case "tipo gusano":
                        gestion.data = await _context.Capturas.Where(c => c.TipoGusano == valor && c.UsuarioId == id).ToListAsync();
                        break;
                    case "tamaño bajo":
                        if (int.TryParse(valor, out int tamañoBajo))
                            gestion.data = await _context.Capturas.Where(c => c.TamañoBajo == tamañoBajo && c.UsuarioId == id).ToListAsync();
                        else
                            gestion.setError("El valor para 'tamaño bajo' debe ser numérico.");
                        return gestion;
                    case "tipo señuelo":
                        gestion.data = await _context.Capturas.Where(c => c.TipoSeñuelo == valor && c.UsuarioId == id).ToListAsync();
                        break;
                    default:
                        gestion.setError($"La propiedad '{propiedad}' no es válida.");
                        return gestion;
                }
                if (gestion.data.Count > 0)
                {
                    gestion.Correct();
                }
                else
                {
                    gestion.setError($"En {propiedad} no hay capturas con valor {valor}.");
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
                Captura capturaDelete = await _context.Capturas.FirstOrDefaultAsync(c => c.CapturaId == id);
                if (capturaDelete != null)
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
        public async Task<Gestion> EditarNombreUsuario(int id, string nombre)
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
        public async Task<Gestion> EditarCapturasDeUnUsuarioPorPropiedad(int usuarioId, int capturaId, string propiedad, string valor)
        {
            Gestion gestion = new Gestion();
            try
            {
                Captura capturaEditar = await _context.Capturas.FirstOrDefaultAsync(c => c.CapturaId == capturaId);
                bool usuarioExiste = _context.Capturas.Any(c => c.UsuarioId == usuarioId);
                if (!usuarioExiste)
                {
                    gestion.setError("El usuario que buscas no existe.");
                    return gestion;
                }
                if (capturaEditar == null)
                {
                    gestion.setError("La captura no existe.");
                    return gestion;
                }
                switch (propiedad.ToLower())
                {
                    case "especie":
                        if (capturaEditar.NombreEspecie == valor)
                        {
                            gestion.setError($"La propiedad {propiedad} ya tiene como valor {valor}");
                            return gestion;
                        }
                        capturaEditar.NombreEspecie = valor;
                        break;
                    case "localidad":
                        if (capturaEditar.Localidad == valor)
                        {
                            gestion.setError($"La propiedad {propiedad} ya tiene como valor {valor}");
                            return gestion;
                        }
                        capturaEditar.Localidad = valor;
                        break;
                    case "tamaño":
                        if (int.TryParse(valor, out int tamañoPez))
                        {
                            if (capturaEditar.Tamaño == tamañoPez)
                            {
                                gestion.setError($"La propiedad {propiedad} ya tiene como valor {valor}");
                                return gestion;
                            }
                            capturaEditar.Tamaño = tamañoPez;
                            break;
                        }
                        else
                            gestion.setError("El valor para 'tamaño' debe ser numérico.");
                        return gestion;
                    case "fecha":
                        if (DateTime.TryParse(valor, out DateTime fecha))
                        {
                            if (capturaEditar.Fecha == fecha)
                            {
                                gestion.setError($"La propiedad {propiedad} ya tiene como valor {valor}");
                                return gestion;
                            }
                            capturaEditar.Fecha = fecha;
                            break;
                        }
                        else
                            gestion.setError("El valor para 'fecha' debe ser una fecha.");
                        return gestion;
                    case "hora":
                        if (int.TryParse(valor, out int hora))
                        {
                            if (capturaEditar.HoraAproximada == hora)
                            {
                                gestion.setError($"La propiedad {propiedad} ya tiene como valor {valor}");
                                return gestion;
                            }
                            capturaEditar.HoraAproximada = hora;
                            break;
                        }
                        else
                            gestion.setError("El valor para 'hora' debe ser numérico.");
                        return gestion;
                    case "zona":
                        if (capturaEditar.Zona == valor)
                        {
                            gestion.setError($"La propiedad {propiedad} ya tiene como valor {valor}");
                            return gestion;
                        }
                        capturaEditar.Zona = valor;
                        break;
                    case "profundidad":
                        if (int.TryParse(valor, out int profundidad))
                        {
                            if (capturaEditar.Profundidad == profundidad)
                            {
                                gestion.setError($"La propiedad {propiedad} ya tiene como valor {valor}");
                                return gestion;
                            }
                            capturaEditar.Profundidad = profundidad;
                            break;
                        }
                        else
                            gestion.setError("El valor para 'profundidad' debe ser numérico.");
                        return gestion;
                    case "oleaje":
                        if (capturaEditar.Oleaje == valor)
                        {
                            gestion.setError($"La propiedad {propiedad} ya tiene como valor {valor}");
                            return gestion;
                        }
                        capturaEditar.Oleaje = valor;
                        break;
                    case "estilo":
                        if (capturaEditar.EstiloPesca == valor)
                        {
                            gestion.setError($"La propiedad {propiedad} ya tiene como valor {valor}");
                            return gestion;
                        }
                        capturaEditar.EstiloPesca = valor;
                        break;
                    case "clima":
                        if (capturaEditar.TiempoClimatico == valor)
                        {
                            gestion.setError($"La propiedad {propiedad} ya tiene como valor {valor}");
                            return gestion;
                        }
                        capturaEditar.TiempoClimatico = valor;
                        break;
                    case "claridad agua":
                        if (capturaEditar.ClaridadAgua == valor)
                        {
                            gestion.setError($"La propiedad {propiedad} ya tiene como valor {valor}");
                            return gestion;
                        }
                        capturaEditar.ClaridadAgua = valor;
                        break;
                    case "tamaño anzuelo":
                        if (int.TryParse(valor, out int tamañoAnzuelo))
                        {
                            if (capturaEditar.TamañoAnzuelo == tamañoAnzuelo)
                            {
                                gestion.setError($"La propiedad {propiedad} ya tiene como valor {valor}");
                                return gestion;
                            }
                            capturaEditar.TamañoAnzuelo = tamañoAnzuelo;
                            break;
                        }
                        else
                            gestion.setError("El valor para 'tamaño anzuelo' debe ser numérico.");
                        return gestion;
                    case "tipo gusano":
                        if (capturaEditar.TipoGusano == valor)
                        {
                            gestion.setError($"La propiedad {propiedad} ya tiene como valor {valor}");
                            return gestion;
                        }
                        capturaEditar.TipoGusano = valor;
                        break;
                    case "tamaño bajo":
                        if (int.TryParse(valor, out int tamañoBajo))
                        {
                            if (capturaEditar.TamañoBajo == tamañoBajo)
                            {
                                gestion.setError($"La propiedad {propiedad} ya tiene como valor {valor}");
                                return gestion;
                            }
                            capturaEditar.TamañoBajo = tamañoBajo;
                            break;
                        }
                        else
                            gestion.setError("El valor para 'tamaño bajo' debe ser numérico.");
                        return gestion;
                    case "tipo señuelo":
                        if (capturaEditar.TipoSeñuelo == valor)
                        {
                            gestion.setError($"La propiedad {propiedad} ya tiene como valor {valor}");
                            return gestion;
                        }
                        capturaEditar.TipoSeñuelo = valor;
                        break;
                    default:
                        gestion.setError($"La propiedad '{propiedad}' no es válida.");
                        return gestion;
                }
                gestion.Correct($"La propiedad '{propiedad}' ahora tiene como valor {valor}.");
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                gestion.setError("Error de tipo {0}, mensaje: {1}", new List<dynamic>() { ex.GetType().Name, ex.Message });
            }
            return gestion;
        }
        public async Task<Gestion> EditarAQueUsuarioPerteneceCaptura(int usuarioAntiguo, int usuarioNuevo, int capturaId)
        {
            //buscar el usuario que tiene la captura
            //mostrar las capturas
            //elegir la captura
            //decir a que nuevo usuario poner la captura
            Gestion gestion = new Gestion();
            try
            {
                if (usuarioAntiguo == usuarioNuevo)
                {
                    gestion.setError("El usuario nuevo y el antiguo no pueden ser el mismo");
                    return gestion;
                }
                Usuario usuarioAntes = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == usuarioAntiguo);
                if (usuarioAntes != null)
                {
                    Captura capturaEditar = await _context.Capturas.FirstOrDefaultAsync(c => c.CapturaId == capturaId);
                    if (capturaEditar != null)
                    {
                        Usuario usuarioAhora = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == usuarioNuevo);
                        if (usuarioAhora != null)
                        {
                            if (capturaEditar.UsuarioId != usuarioAntiguo)
                            {
                                gestion.setError("Esa captura no pertenece a ese usuario");
                            }
                            else
                            {
                                capturaEditar.UsuarioId = usuarioNuevo;
                                gestion.Correct("Captura editada correctamente");
                                await _context.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            gestion.setError("No hay usuarios con ese ID");
                        }
                    }
                    else
                    {
                        gestion.setError("No hay capturas con ese ID");
                    }
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
