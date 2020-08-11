using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestePloomes.ViewModels
{
    public class ClienteViewModel
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Nome { get; set; }


        [MaxLength(14)]
        public string CPF { get; set; }

        public int Idade { get; set; }

        public string Sexo { get; set; }


        public string Email { get; set; }


        [MaxLength(13)]
        public string Celular { get; set; }
    }
}
