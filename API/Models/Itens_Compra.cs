using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class Itens_Compra

    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [SwaggerIgnore]
        public int Itens_Compra_PK { get; set; }
        public decimal ValorUnitario { get; set; }
        public int Quantidade { get; internal set; }

        public int Compra_Id { get; set; }
        [ForeignKey("Compra_Id")]
        [JsonIgnore]
        public Compras Compras { get; set; }

        public int? ProductId { get; set; }
        [ForeignKey("ProductId")]
        [JsonIgnore]
        public Produtos Produtos { get; set; }
    }
}
