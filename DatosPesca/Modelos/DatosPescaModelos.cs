using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static DatosPesca.Modelos.DatosPescaModelos;

namespace DatosPesca.Modelos
{
    public class DatosPescaModelos
    {
        public class Usuario
        {
            [Key]
            public int Id { get; set; } 
            public string Nombre { get; set; }
            public string Correo { get; set; }
            public string Contraseña { get; set; }
            public List<Captura>? Capturas { get; set; }
            
        }
        public class Captura
        {
            
            [Key]
            public int CapturaId { get; set; }
            public string? ImagenNombre { get; set; }
            public int? UsuarioId { get; set; }
            [ForeignKey("UsuarioId")]
            public Usuario? Usuario { get; set; }
            public string NombreEspecie { get; set; }
            public double Tamaño { get; set; }
            public DateTime? Fecha { get; set; }
            public string? Localidad { get; set; }
            public int HoraAproximada { get; set; }
            public Zona? Zona { get; set; }
            public int Profundidad { get; set; }
            public Oleaje? Oleaje { get; set; }
            public TiempoClimatico? TiempoClimatico { get; set; }
            public ClaridadAgua? ClaridadAgua { get; set; }
            public EstiloPesca EstiloPesca { get; set; }
            public bool Anzuelo { get; set; }
            public TamañoAnzuelo? TamañoAnzuelo { get; set; }
            public bool Cebo { get; set; }
            public TipoCebo? TipoCebo { get; set; }
            public double TamañoHilo { get; set; }
            public double? TamañoBajo { get; set; }
            public bool Señuelo { get; set; }
            public TipoSeñuelo? TipoSeñuelo { get; set; }
        }
        public class CapturaInsert
        {
            public int? UsuarioId { get; set; }
            public string NombreEspecie { get; set; }
            public double Tamaño { get; set; }
            public DateTime? Fecha { get; set; }
            public string? Localidad { get; set; }
            public int HoraAproximada { get; set; }
            public Zona? Zona { get; set; }
            public int Profundidad { get; set; }
            public Oleaje? Oleaje { get; set; }
            public TiempoClimatico? TiempoClimatico { get; set; }
            public ClaridadAgua? ClaridadAgua { get; set; }
            public EstiloPesca EstiloPesca { get; set; }
            public bool Anzuelo { get; set; }
            public TamañoAnzuelo? TamañoAnzuelo { get; set; }
            public bool Cebo { get; set; }
            public TipoCebo? TipoCebo { get; set; }
            public double TamañoHilo { get; set; }
            public double? TamañoBajo { get; set; }
            public bool Señuelo { get; set; }
            public TipoSeñuelo? TipoSeñuelo { get; set; }
        }
        public class UsuarioInsert
        {
            public string Nombre { get; set; }
        }
        public class UsuarioRegistroInsert
        {
            public string Nombre { get; set; }
            public string Correo { get; set; }
            public string Contraseña { get; set; }
        }
        public class CapturaInsertObligatorio
        {
            public int? UsuarioId { get; set; }
            public string NombreEspecie { get; set; }
            public double Tamaño { get; set; }
            public EstiloPesca EstiloPesca { get; set; }
        }
        public enum Zona
        {
            Espigon,
            Playa,
            Barca,
            Kayak,
            AcantiladoProfundo,
            Roca,
            Estuario,
            Muelle,
            Arrecife,
            BancoArena,
            Canal
        }
        public enum Oleaje
        {
            Plato,           
            PicadaSuave,     
            PicadaFuerte,    
            MedioMetro,     
            UnMetro,         
            DosMetros,       
            Temporal        
        }
        public enum TiempoClimatico
        {
            Despejado,
            Soleado,
            ParcialmenteNublado,
            Nublado,
            LluviaLigera,
            LluviaModerada,
            Tormenta,
            Niebla,
            VientoFuerte
        }
        public enum ClaridadAgua
        {
            Clara,
            PocoClara,
            Turbia,
            Marron,
            MuyOscura
        }
        public enum EstiloPesca
        {
            Spinning,
            Surfcasting,
            Rockfishing,
            Eging,
            Currican,
            Jigging,
            Corcheo
        }
        public enum TipoCebo
        {
            Coreano,
            Americano,
            Tita,
            Langostino,
            Calamar,
            Sardina,
            Choco,
            Gamba,
            Lombriz,
            Rosca,
            Cangrejo
        }
        public enum TamañoAnzuelo
        {
            _12,
            _11,
            _10,
            _9,
            _8,
            _7,
            _6,
            _5,
            _4,
            _3,
            _2,
            _1,
            _1_0, 
            _2_0,
            _3_0
        }
        public enum TipoSeñuelo
        {
            Jig,
            Minnow,
            Vinilo,
            Poper,
            Pencil,
            Stickbait,
            Spinner,
            Crankbait
        }
    }
}
