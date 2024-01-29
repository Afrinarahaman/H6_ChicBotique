using H6_ChicBotique.Database;
using H6_ChicBotique.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace H6_ChicBotique.Repositories
{
    public interface IOrderRepository     //Interface which declares the methods
    {
        Task<List<Order>> SelectAllOrders();  //For getting all Orders with Order Details 
        Task<Order> SelectOrderById(int orderId); //For getting Order by specific Id
        Task<List<Order>> SelectOrdersByAccountInfoId(Guid AccountInfoId); ////For getting Orders by specific unique AccountInfoId
        Task<Order> CreateNewOrder(Order orderId); //Creating a new user entity
    }
    // Implementation of IOrderRepository interface in OrderRepository class
    public class OrderRepository:IOrderRepository
    {
        private readonly ChicBotiqueDatabaseContext _context;

        public OrderRepository(ChicBotiqueDatabaseContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> SelectAllOrders()
        {
            try
            {
                return await _context.Order
                         .Include(o => o.AccountInfo)
                         .Include(o => o.OrderDetails)
                         .Include(s => s.ShippingDetails)
                         .Include(p => p.Payment)

                          .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<List<Order>> SelectOrdersByAccountInfoId(Guid AccountId)
        {
            try
            {
                return await _context.Order
                    .Include(o => o.AccountInfo)
                         .Include(o => o.OrderDetails).ThenInclude(x => x.Product).Where(c => c.AccountId== AccountId)
                         .Include(s => s.ShippingDetails)
                          .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Order> SelectOrderById(int orderId)
        {
            try
            {
                return await _context.Order
                    .Include(a => a.OrderDetails).ThenInclude(a => a.Product)
                    .Include(c => c.AccountInfo).ThenInclude(s => s.HomeAddress)
                    .Include(s => s.ShippingDetails)
                    .Include(p => p.Payment)
                    .FirstOrDefaultAsync(order => order.Id == orderId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Order> CreateNewOrder(Order order)
        {
            try
            {
                _context.Order.Add(order);
                await _context.SaveChangesAsync();
                return order;

            }
            catch (Exception err)
            {

                Console.Write(err.Message);
                return null;
            }



        }
    
        public async Task<Order> DeleteOrderById(int orderId)
        {
            var deleteOrder = await _context.Set<Order>().FirstOrDefaultAsync(o => o.Id == orderId);
            try
            {
                if (deleteOrder != null)
                {
                    _context.Remove(deleteOrder);
                    await _context.SaveChangesAsync();

                }

                return deleteOrder;
            }
            catch (Exception)
            {
                return null;
            }
        }



        public async Task<Order> UpdateExistingOrder(int orderId, Order order)
        {


            try
            {
                Order updateOrder = await _context.Order.FirstOrDefaultAsync(order => order.Id== orderId);

                //_context.Update(order); //update all properties with navigation object
                _context.Entry(updateOrder).CurrentValues.SetValues(order); //update only the properties inside the order without orderdetails(navigation object)

                await _context.SaveChangesAsync();

                return await _context.Order.FirstOrDefaultAsync(order => order.Id == orderId);
            }
            catch (Exception)
            {
                return null;
            }

        }

    }
}
