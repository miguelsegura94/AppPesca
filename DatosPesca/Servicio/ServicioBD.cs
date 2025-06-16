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
        //TODO AÑADIR NUEVO METODO PARA AÑADIR FOTO DE LA CAPTURA, Y TAMBIEN IMAGEN DE PERFIL DE USUARIO
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
        public async Task<Gestion> GetUsuariosConCapturas()
        {
            Gestion gestion = new Gestion();
            try
            {
                gestion.data = await _context.Usuarios.Include(u => u.Capturas).ToListAsync();

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
                        if (double.TryParse(valor, out double tamañoPez))
                            gestion.data = await _context.Capturas.Where(c => c.Tamaño == tamañoPez).ToListAsync();
                        else
                            gestion.setError("El valor para 'tamaño' debe ser numérico.");
                        return gestion;
                    case "fecha":
                        if (DateTime.TryParse(valor, out DateTime fecha))
                            gestion.data = await _context.Capturas.Where(c => c.Fecha == fecha).ToListAsync();
                        else
                            gestion.setError("El valor para 'fecha' debe ser una fecha válida.");
                        return gestion;
                    case "hora":
                        if (int.TryParse(valor, out int hora))
                            gestion.data = await _context.Capturas.Where(c => c.HoraAproximada == hora).ToListAsync();
                        else
                            gestion.setError("El valor para 'hora' debe ser numérico.");
                        return gestion;
                    case "zona":
                        if (Enum.TryParse<Zona>(valor, true, out var zonaEnum))
                            gestion.data = await _context.Capturas.Where(c => c.Zona == zonaEnum).ToListAsync();
                        else
                            gestion.setError("El valor para 'zona' no es válido.");
                        return gestion;
                    case "profundidad":
                        if (int.TryParse(valor, out int profundidad))
                            gestion.data = await _context.Capturas.Where(c => c.Profundidad == profundidad).ToListAsync();
                        else
                            gestion.setError("El valor para 'profundidad' debe ser numérico.");
                        return gestion;
                    case "oleaje":
                        if (Enum.TryParse<Oleaje>(valor, true, out var oleajeEnum))
                            gestion.data = await _context.Capturas.Where(c => c.Oleaje == oleajeEnum).ToListAsync();
                        else
                            gestion.setError("El valor para 'oleaje' no es válido.");
                        return gestion;
                    case "estilo":
                        if (Enum.TryParse<EstiloPesca>(valor, true, out var estiloEnum))
                            gestion.data = await _context.Capturas.Where(c => c.EstiloPesca == estiloEnum).ToListAsync();
                        else
                            gestion.setError("El valor para 'estilo' no es válido.");
                        return gestion;
                    case "clima":
                        if (Enum.TryParse<TiempoClimatico>(valor, true, out var climaEnum))
                            gestion.data = await _context.Capturas.Where(c => c.TiempoClimatico == climaEnum).ToListAsync();
                        else
                            gestion.setError("El valor para 'clima' no es válido.");
                        return gestion;
                    case "claridad agua":
                        if (Enum.TryParse<ClaridadAgua>(valor, true, out var claridadEnum))
                            gestion.data = await _context.Capturas.Where(c => c.ClaridadAgua == claridadEnum).ToListAsync();
                        else
                            gestion.setError("El valor para 'claridad agua' no es válido.");
                        return gestion;
                    case "tamaño anzuelo":
                        if (Enum.TryParse<TamañoAnzuelo>(valor.Replace("/", "_"), true, out var anzueloEnum))
                            gestion.data = await _context.Capturas.Where(c => c.TamañoAnzuelo == anzueloEnum).ToListAsync();
                        else
                            gestion.setError("El valor para 'tamaño anzuelo' no es válido.");
                        return gestion;
                    case "tipo gusano":
                        if (Enum.TryParse<TipoCebo>(valor, true, out var ceboEnum))
                            gestion.data = await _context.Capturas.Where(c => c.TipoCebo == ceboEnum).ToListAsync();
                        else
                            gestion.setError("El valor para 'tipo cebo' no es válido.");
                        return gestion;
                    case "tamaño bajo":
                        if (double.TryParse(valor, out double tamañoBajo))
                            gestion.data = await _context.Capturas.Where(c => c.TamañoBajo == tamañoBajo).ToListAsync();
                        else
                            gestion.setError("El valor para 'tamaño bajo' debe ser numérico.");
                        return gestion;
                    case "tipo señuelo":
                        if (Enum.TryParse<TipoSeñuelo>(valor, true, out var señueloEnum))
                            gestion.data = await _context.Capturas.Where(c => c.TipoSeñuelo == señueloEnum).ToListAsync();
                        else
                            gestion.setError("El valor para 'tipo señuelo' no es válido.");
                        return gestion;
                    default:
                        gestion.setError($"La propiedad '{propiedad}' no es válida.");
                        return gestion;
                }
                if (gestion.data != null && gestion.data.Count > 0)
                    gestion.Correct();
                else
                    gestion.setError($"En {propiedad} no hay capturas con valor {valor}.");
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
                        if (double.TryParse(valor, out double tamañoPez))
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
                        if (Enum.TryParse<Zona>(valor, true, out var zonaEnum))
                            gestion.data = await _context.Capturas.Where(c => c.Zona == zonaEnum && c.UsuarioId == id).ToListAsync();
                        else
                            gestion.setError("El valor para 'zona' no es válido.");
                        return gestion;

                    case "profundidad":
                        if (int.TryParse(valor, out int profundidad))
                            gestion.data = await _context.Capturas.Where(c => c.Profundidad == profundidad && c.UsuarioId == id).ToListAsync();
                        else
                            gestion.setError("El valor para 'profundidad' debe ser numérico.");
                        return gestion;

                    case "oleaje":
                        if (Enum.TryParse<Oleaje>(valor, true, out var oleajeEnum))
                            gestion.data = await _context.Capturas.Where(c => c.Oleaje == oleajeEnum && c.UsuarioId == id).ToListAsync();
                        else
                            gestion.setError("El valor para 'oleaje' no es válido.");
                        return gestion;

                    case "estilo":
                        if (Enum.TryParse<EstiloPesca>(valor, true, out var estiloEnum))
                            gestion.data = await _context.Capturas.Where(c => c.EstiloPesca == estiloEnum && c.UsuarioId == id).ToListAsync();
                        else
                            gestion.setError("El valor para 'estilo' no es válido.");
                        return gestion;

                    case "clima":
                        if (Enum.TryParse<TiempoClimatico>(valor, true, out var climaEnum))
                            gestion.data = await _context.Capturas.Where(c => c.TiempoClimatico == climaEnum && c.UsuarioId == id).ToListAsync();
                        else
                            gestion.setError("El valor para 'clima' no es válido.");
                        return gestion;

                    case "claridad agua":
                        if (Enum.TryParse<ClaridadAgua>(valor, true, out var claridadEnum))
                            gestion.data = await _context.Capturas.Where(c => c.ClaridadAgua == claridadEnum && c.UsuarioId == id).ToListAsync();
                        else
                            gestion.setError("El valor para 'claridad agua' no es válido.");
                        return gestion;

                    case "tamaño anzuelo":
                        if (Enum.TryParse<TamañoAnzuelo>(valor.Replace("/", "_"), true, out var anzueloEnum))
                            gestion.data = await _context.Capturas.Where(c => c.TamañoAnzuelo == anzueloEnum && c.UsuarioId == id).ToListAsync();
                        else
                            gestion.setError("El valor para 'tamaño anzuelo' no es válido.");
                        return gestion;

                    case "tipo cebo":
                        if (Enum.TryParse<TipoCebo>(valor, true, out var tipoCeboEnum))
                            gestion.data = await _context.Capturas.Where(c => c.TipoCebo == tipoCeboEnum && c.UsuarioId == id).ToListAsync();
                        else
                            gestion.setError("El valor para 'tipo gusano' no es válido.");
                        return gestion;

                    case "tamaño bajo":
                        if (double.TryParse(valor, out double tamañoBajo))
                            gestion.data = await _context.Capturas.Where(c => c.TamañoBajo == tamañoBajo && c.UsuarioId == id).ToListAsync();
                        else
                            gestion.setError("El valor para 'tamaño bajo' debe ser numérico.");
                        return gestion;

                    case "tipo señuelo":
                        if (Enum.TryParse<TipoSeñuelo>(valor, true, out var tipoSenEnum))
                            gestion.data = await _context.Capturas.Where(c => c.TipoSeñuelo == tipoSenEnum && c.UsuarioId == id).ToListAsync();
                        else
                            gestion.setError("El valor para 'tipo señuelo' no es válido.");
                        return gestion;

                    default:
                        gestion.setError($"La propiedad '{propiedad}' no es válida.");
                        return gestion;
                }

                if (gestion.data != null && gestion.data.Count > 0)
                    gestion.Correct();
                else
                    gestion.setError($"No hay capturas para {propiedad} con valor '{valor}'.");
            }
            catch (Exception ex)
            {
                gestion.setError($"Error de tipo {ex.GetType().Name}, mensaje: {ex.Message}");
            }

            return gestion;
        }

        public async Task<Gestion> AñadirCapturaCompleta(CapturaInsert modeloCaptura)
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
                if (string.IsNullOrWhiteSpace(modeloCaptura.Localidad))
                {
                    gestion.setError("La localidad no puede estar vacía.");
                    return gestion;
                }
                if (modeloCaptura.HoraAproximada < 0 || modeloCaptura.HoraAproximada > 23)
                {
                    gestion.setError("La hora no es correcta.");
                    return gestion;
                }
                if (modeloCaptura.Zona == null)
                {
                    gestion.setError("La zona no puede estar vacía.");
                    return gestion;
                }
                if (modeloCaptura.Profundidad < 0)
                {
                    gestion.setError("La profundidad no puede ser menor que 0.");
                    return gestion;
                }
                if (modeloCaptura.Oleaje == null)
                {
                    gestion.setError("El oleaje no puede estar vacío.");
                    return gestion;
                }
                if (modeloCaptura.TiempoClimatico == null)
                {
                    gestion.setError("El tiempo climático no puede estar vacío.");
                    return gestion;
                }
                if (modeloCaptura.ClaridadAgua == null)
                {
                    gestion.setError("La claridad del agua no puede estar vacía.");
                    return gestion;
                }
                if (modeloCaptura.EstiloPesca == null)
                {
                    gestion.setError("El estilo de pesca no puede estar vacío.");
                    return gestion;
                }
                if (modeloCaptura.Anzuelo)
                {
                    if (modeloCaptura.TamañoAnzuelo == null)
                    {
                        gestion.setError("El tamaño del anzuelo no puede estar vacío si se usa anzuelo.");
                        return gestion;
                    }
                }
                if (modeloCaptura.Cebo)
                {
                    if (modeloCaptura.TipoCebo == null)
                    {
                        gestion.setError("El tipo de cebo no puede estar vacío si se usa cebo.");
                        return gestion;
                    }
                }
                if (modeloCaptura.Señuelo)
                {
                    if (modeloCaptura.TipoSeñuelo == null)
                    {
                        gestion.setError("El tipo de señuelo no puede estar vacío si se usa señuelo.");
                        return gestion;
                    }
                }
                Captura capturaMapeo = ConvertirCapturaInsertACaptura(modeloCaptura);
                await _context.Capturas.AddAsync(capturaMapeo);
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
                if (!Enum.IsDefined(typeof(EstiloPesca), modeloCaptura.EstiloPesca))
                {
                    gestion.setError("El estilo de pesca especificado no es válido.");
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
        public async Task<Gestion> EditarCapturaDeUnUsuario(int? usuarioId, int capturaEditarId, CapturaInsertObligatorio captura)
        {
            Gestion gestion = new Gestion();
            try
            {
                Captura capturaEditar = await _context.Capturas.FirstOrDefaultAsync(c => c.CapturaId == capturaEditarId);
                if (capturaEditar == null)
                {
                    gestion.setError("La captura no existe.");
                    return gestion;
                }
                bool usuarioExiste = _context.Usuarios.Any(u => u.Id == usuarioId);
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
                capturaEditar.UsuarioId = captura.UsuarioId;
                capturaEditar.NombreEspecie = captura.NombreEspecie;
                capturaEditar.Tamaño = captura.Tamaño;
                capturaEditar.EstiloPesca = captura.EstiloPesca;
                gestion.Correct($"Captura editada correctamente.");
                await _context.SaveChangesAsync();
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
                        if (double.TryParse(valor, out double tamañoPez))
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
                        {
                            gestion.setError("El valor para 'tamaño' debe ser numérico.");
                            return gestion;
                        }
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
                        {
                            gestion.setError("El valor para 'fecha' debe ser una fecha.");
                            return gestion;
                        }
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
                        {
                            gestion.setError("El valor para 'hora' debe ser numérico.");
                            return gestion;
                        }
                    case "zona":
                        if (Enum.TryParse<Zona>(valor, true, out var nuevaZona))
                        {
                            if (capturaEditar.Zona == nuevaZona)
                            {
                                gestion.setError($"La propiedad {propiedad} ya tiene como valor {valor}");
                                return gestion;
                            }
                            capturaEditar.Zona = nuevaZona;
                            break;
                        }
                        else
                        {
                            gestion.setError("El valor para 'zona' no es válido.");
                            return gestion;
                        }
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
                        {
                            gestion.setError("El valor para 'profundidad' debe ser numérico.");
                            return gestion;
                        }
                    case "oleaje":
                        if (Enum.TryParse<Oleaje>(valor, true, out var nuevoOleaje))
                        {
                            if (capturaEditar.Oleaje == nuevoOleaje)
                            {
                                gestion.setError($"La propiedad {propiedad} ya tiene como valor {valor}");
                                return gestion;
                            }
                            capturaEditar.Oleaje = nuevoOleaje;
                            break;
                        }
                        else
                        {
                            gestion.setError("El valor para 'oleaje' no es válido.");
                            return gestion;
                        }
                    case "estilo":
                        if (Enum.TryParse<EstiloPesca>(valor, true, out var nuevoEstilo))
                        {
                            if (capturaEditar.EstiloPesca == nuevoEstilo)
                            {
                                gestion.setError($"La propiedad {propiedad} ya tiene como valor {valor}");
                                return gestion;
                            }
                            capturaEditar.EstiloPesca = nuevoEstilo;
                            break;
                        }
                        else
                        {
                            gestion.setError("El valor para 'estilo' no es válido.");
                            return gestion;
                        }
                    case "clima":
                        if (Enum.TryParse<TiempoClimatico>(valor, true, out var nuevoClima))
                        {
                            if (capturaEditar.TiempoClimatico == nuevoClima)
                            {
                                gestion.setError($"La propiedad {propiedad} ya tiene como valor {valor}");
                                return gestion;
                            }
                            capturaEditar.TiempoClimatico = nuevoClima;
                            break;
                        }
                        else
                        {
                            gestion.setError("El valor para 'clima' no es válido.");
                            return gestion;
                        }
                    case "claridad agua":
                        if (Enum.TryParse<ClaridadAgua>(valor, true, out var nuevaClaridad))
                        {
                            if (capturaEditar.ClaridadAgua == nuevaClaridad)
                            {
                                gestion.setError($"La propiedad {propiedad} ya tiene como valor {valor}");
                                return gestion;
                            }
                            capturaEditar.ClaridadAgua = nuevaClaridad;
                            break;
                        }
                        else
                        {
                            gestion.setError("El valor para 'claridad agua' no es válido.");
                            return gestion;
                        }
                    case "tamaño anzuelo":
                        if (Enum.TryParse<TamañoAnzuelo>(valor.Replace("/", "_"), true, out var nuevoAnzuelo))
                        {
                            if (capturaEditar.TamañoAnzuelo == nuevoAnzuelo)
                            {
                                gestion.setError($"La propiedad {propiedad} ya tiene como valor {valor}");
                                return gestion;
                            }
                            capturaEditar.TamañoAnzuelo = nuevoAnzuelo;
                            break;
                        }
                        else
                        {
                            gestion.setError("El valor para 'tamaño anzuelo' no es válido.");
                            return gestion;
                        }
                    case "tipo gusano":
                        if (Enum.TryParse<TipoCebo>(valor, true, out var nuevoCebo))
                        {
                            if (capturaEditar.TipoCebo == nuevoCebo)
                            {
                                gestion.setError($"La propiedad {propiedad} ya tiene como valor {valor}");
                                return gestion;
                            }
                            capturaEditar.TipoCebo = nuevoCebo;
                            break;
                        }
                        else
                        {
                            gestion.setError("El valor para 'tipo gusano' no es válido.");
                            return gestion;
                        }
                    case "tamaño bajo":
                        if (double.TryParse(valor, out double tamañoBajo))
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
                        {
                            gestion.setError("El valor para 'tamaño bajo' debe ser numérico.");
                            return gestion;
                        }
                    case "tipo señuelo":
                        if (Enum.TryParse<TipoSeñuelo>(valor, true, out var nuevoSeñuelo))
                        {
                            if (capturaEditar.TipoSeñuelo == nuevoSeñuelo)
                            {
                                gestion.setError($"La propiedad {propiedad} ya tiene como valor {valor}");
                                return gestion;
                            }
                            capturaEditar.TipoSeñuelo = nuevoSeñuelo;
                            break;
                        }
                        else
                        {
                            gestion.setError("El valor para 'tipo señuelo' no es válido.");
                            return gestion;
                        }
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
        public Captura ConvertirCapturaInsertACaptura(CapturaInsert modeloCaptura)
        {
            return new Captura
            {
                UsuarioId = modeloCaptura.UsuarioId,
                NombreEspecie = modeloCaptura.NombreEspecie,
                Tamaño = modeloCaptura.Tamaño,
                Fecha = modeloCaptura.Fecha,
                Localidad = modeloCaptura.Localidad,
                HoraAproximada = modeloCaptura.HoraAproximada,
                Zona = modeloCaptura.Zona,
                Profundidad = modeloCaptura.Profundidad,
                Oleaje = modeloCaptura.Oleaje,
                TiempoClimatico = modeloCaptura.TiempoClimatico,
                ClaridadAgua = modeloCaptura.ClaridadAgua,
                EstiloPesca = modeloCaptura.EstiloPesca,
                Anzuelo = modeloCaptura.Anzuelo,
                TamañoAnzuelo = modeloCaptura.TamañoAnzuelo,
                Cebo = modeloCaptura.Cebo,
                TipoCebo = modeloCaptura.TipoCebo,
                TamañoHilo = modeloCaptura.TamañoHilo,
                TamañoBajo = modeloCaptura.TamañoBajo,
                Señuelo = modeloCaptura.Señuelo,
                TipoSeñuelo = modeloCaptura.TipoSeñuelo
            };
        }

    }
}
