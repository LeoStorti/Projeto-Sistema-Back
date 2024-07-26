using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Models
{
    public class Produtos
    {
        internal readonly char Produtos_Nome;
        internal readonly object ValorUnitario;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [SwaggerIgnore] // Esta anotação oculta a propriedade no Swagger
        public int ProductId { get; set; }
        public string? NomeProduto { get; set; }
        public string? Fornecedor { get; set; }
        public int? Quantidade { get; set; }
        public decimal? ValorDeCompra { get; set; }
        public decimal? ValorDeVenda { get; set; }

        public List<Itens_Venda> ItensVenda{ get; set; } = new();


    }
}