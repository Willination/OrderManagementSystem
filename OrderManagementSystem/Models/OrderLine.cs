using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Models
{
    public class OrderLine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int LineNumber { get; set; }
        public string? ProductCode { get; set; }
        public string? ProductType { get; set; }
        public decimal? CostPrice { get; set; }
        public decimal? SalesPrice { get; set; }
        public int Quantity { get; set; }

        public int? OrderHeaderId { get; set; }
        public OrderHeader? OrderHeader { get; set; }

    }
}
