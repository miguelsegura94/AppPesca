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
            public List<Captura>? Capturas { get; set; }
            
        }
        public class Captura
        {
            //TODO * OBLIGATORIO, LOS DEMAS DATOS AGREGAR VALOR POR DEFECTO
            [Key]
            public int CapturaId { get; set; }
            public int? UsuarioId { get; set; }
            [ForeignKey("UsuarioId")]
            public Usuario? Usuario { get; set; }
            public string NombreEspecie { get; set; }//*
            public double Tamaño { get; set; }//*
            public DateTime? Fecha { get; set; }
            public string? Localidad { get; set; }//puig,pobla,cullera etc
            public int HoraAproximada { get; set; }
            public string? Zona { get; set; }//espigon,playa,barca etc--ENUM
            public int Profundidad { get; set; }
            public string? Oleaje { get; set; }//aceite,picada,1m,2m etc--ENUM
            public string? TiempoClimatico { get; set; }//lluvia, nublado, despejado etc--ENUM
            public string? ClaridadAgua { get; set; }//clara,poco clara,oscura,marron,etc--ENUM
            public string EstiloPesca { get; set; }//spinning, surfcasting etc--ENUM *
            public bool Anzuelo { get; set; }
            public int? TamañoAnzuelo { get; set; }
            public bool Gusano { get; set; }
            public string? TipoGusano { get; set; }
            public double TamañoHilo { get; set; }
            public double? TamañoBajo { get; set; }
            public bool Señuelo { get; set; }
            public string? TipoSeñuelo { get; set; }
        }
        public class UsuarioInsert
        {
            public string Nombre { get; set; }
        }
        public class CapturaInsertObligatorio
        {
            public int? UsuarioId { get; set; }
            public string NombreEspecie { get; set; }//*
            public double Tamaño { get; set; }//*
            public string EstiloPesca { get; set; }//*
        }
    }
}
