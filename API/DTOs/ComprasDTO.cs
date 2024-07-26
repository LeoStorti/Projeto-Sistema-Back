namespace API.DTOs
{
    public class ComprasDTO
    {
        public int Fornecedor_Id { get; set; }
        public decimal ValorDeCompra { get; set; }
        public DateTime DataCompra { get; set; }
        public List<ItensCompraDTO> Itens_Compra { get; set; }
    }
}
