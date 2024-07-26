using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Fornecedor
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
        public string? CNPJ { get; set; }
        public string? Endereco { get; set; }

    }
}