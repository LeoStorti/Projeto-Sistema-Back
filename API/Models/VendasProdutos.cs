using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class VendaProdutos
    {
        public int VendaId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorDeVenda { get; set; } // Adicionando o campo ValorDeVenda

        public Produtos? Produto { get; set; }
        public Vendas? Venda { get; set; }

    }
}
