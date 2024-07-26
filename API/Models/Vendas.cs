using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class Vendas
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [SwaggerIgnore]
        public int Vendas_Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [SwaggerIgnore]
        public string? NF { get; set; }

        public int? ClienteId { get; set; }

        public decimal? ValorDeVenda { get; set; }

        public DateTime DataVenda { get; set; } = DateTime.Now; // Defina a data aqui

        [JsonIgnore]
        public ICollection<Itens_Venda> Itens_Venda { get; set; }

    }
}