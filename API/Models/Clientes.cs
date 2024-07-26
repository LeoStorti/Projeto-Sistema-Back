using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Clientes
    {

        [Key] public int ClienteId { get; set; }
        public string? NomeCliente { get; set; }
        public string? CNPJCliente { get; set; }
        public string? EnderecoCliente { get; set; }
        public string? TelefoneCliente { get; set; }
    }
}
