using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class Itens_Venda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [SwaggerIgnore]
        public int Itens_Venda_PK { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }

        public int Vendas_Id_FK { get; set; }
        [ForeignKey("Vendas_Id_FK")]
        [JsonIgnore]
        public Vendas Vendas { get; set; }

        public int? ProdutoId_FK { get; set; }

        [ForeignKey("ProdutoId_FK")]

        [JsonIgnore]  // Adicione esta linha para evitar a serialização circular
        public Produtos Produtos { get; set; }
    }

}

