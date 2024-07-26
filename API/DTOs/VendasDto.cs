namespace API.DTOs
{
    public class VendasDto
    {
        public int ClienteId { get; set; }
        public decimal ValorDeVenda { get; set; }
        public DateTime DataVenda { get; set; }
        public List<ItensVendaDto> Itens_Venda { get; set; }
    }
}
