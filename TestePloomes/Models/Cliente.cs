using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestePloomes.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(14)]
        public string CPF { get; set; }

        [Required]
        public int Idade { get; set; }

        [Required]
        public string Sexo { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MaxLength(14)]
        public string Celular { get; set; }
    }
}
