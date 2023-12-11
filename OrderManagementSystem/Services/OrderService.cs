namespace OrderManagementSystem.Services
{
    using Microsoft.EntityFrameworkCore;
    using OrderManagementSystem.Data;
    using OrderManagementSystem.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class OrderService
    {
        private readonly AppDbContext _dbContext;

        public OrderService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<OrderHeader> GetOrderHeaders()
        {
            return _dbContext.OrderHeader?.ToList() ?? new List<OrderHeader>();
        }

        public OrderHeader GetOrderHeaderById(int id)
        {
            return _dbContext.OrderHeader?.FirstOrDefault(o => o.Id == id) ?? new OrderHeader();
        }

        public void AddOrderHeader(OrderHeader order)
        {
            if (_dbContext.OrderHeader == null)
            {
                _dbContext.OrderHeader = _dbContext.Set<OrderHeader>();
            }

            _dbContext.OrderHeader.Add(order);
            _dbContext.SaveChanges(); // Save OrderHeader first to get a valid Id

            if (order.OrderLines != null)
            {
                foreach (var orderLine in order.OrderLines)
                {
                    orderLine.OrderHeaderId = order.Id;
                }

                _dbContext.SaveChanges();
            }
        }

        public List<OrderLine> GetOrderLines(int orderId)
        {
            
                if (_dbContext.OrderHeader == null)
                {
                    _dbContext.OrderHeader = _dbContext.Set<OrderHeader>();
                }

                var order = _dbContext.OrderHeader
                .Include(o => o.OrderLines) 
                .FirstOrDefault(o => o.Id == orderId);

            if (order != null && order.OrderLines != null)
            {
                return order.OrderLines.ToList();
            }

            return new List<OrderLine>();
        }

        public void UpdateOrderHeader(OrderHeader updatedOrder)
        {
            var existingOrder = _dbContext.OrderHeader?.Find(updatedOrder.Id);
            if (existingOrder != null)
            {
                existingOrder.OrderNumber = updatedOrder.OrderNumber;
                existingOrder.OrderType = updatedOrder.OrderType;
                existingOrder.OrderStatus = updatedOrder.OrderStatus;
                existingOrder.CustomerName = updatedOrder.CustomerName;
                existingOrder.CreateDate = updatedOrder.CreateDate;

                _dbContext.SaveChanges();
            }
        }

        public void DeleteOrderHeader(int id)
        {
            var orderToDelete = _dbContext.OrderHeader?.Find(id);
            if (orderToDelete != null)
            {
                _dbContext.OrderHeader?.Remove(orderToDelete);
                _dbContext.SaveChanges();
            }
        }

        public void DeleteOrderLine(int orderId, int orderLineId)
        {
            var orderToDeleteLineFrom = _dbContext.OrderHeader?.Find(orderId);

            if (orderToDeleteLineFrom != null && orderToDeleteLineFrom.OrderLines != null)
            {
                var orderLineToDelete = orderToDeleteLineFrom.OrderLines.FirstOrDefault(ol => ol.Id == orderLineId);

                if (orderLineToDelete != null)
                {
                    orderToDeleteLineFrom.OrderLines.Remove(orderLineToDelete);
                    _dbContext.SaveChanges();
                }
            }
        }
    }
}
