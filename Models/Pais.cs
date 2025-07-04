﻿using System.ComponentModel.DataAnnotations;

namespace modulo10proyectofinal.Models
{
    public class Pais
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del país es obligatorio")]
        public string Nombre { get; set; }
    }
}
