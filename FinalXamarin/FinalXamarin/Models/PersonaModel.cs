using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalXamarin.Models
{
    public class PersonaModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [MaxLength(30)]
        public string Nombre { get; set; }

        [MaxLength(30)]
        public string Telefono { get; set; }

        [MaxLength(100)]
        public string Direccion { get; set; }
    }
}
