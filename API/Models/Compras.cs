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
    public class Compras
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [SwaggerIgnore] public int Compra_Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [SwaggerIgnore]
        public string? NF { get; set; }

        public int Fornecedor_Id { get; set; }

        public decimal? ValorDeCompra { get; set; }

        public DateTime DataCompra { get; set; } = DateTime.Now; // Defina a data aqui


        [JsonIgnore]
        public ICollection<Itens_Compra> Itens_Compra { get; set; }
    }
}
