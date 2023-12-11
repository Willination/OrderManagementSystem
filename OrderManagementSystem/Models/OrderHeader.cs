using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Models
{
    public class OrderHeader
    {
        public OrderHeader()
        {
            OrderLines = new List<OrderLine>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? OrderNumber { get; set; }
        public string? OrderType { get; set; }
        public string? OrderStatus { get; set; }
        public string? CustomerName { get; set; }
        public DateTime CreateDate { get; set; }

        public List<OrderLine>? OrderLines { get; set; }

        [NotMapped]
        public bool ShowOrderLines { get; set; }
    }
}
