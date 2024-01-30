using H6_ChicBotique.Database;
using H6_ChicBotique.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace H6_ChicBotique.Repositories
{
    public interface IShippingDetailsRepository //Interface which declares the methods
    {
        Task<List<ShippingDetails>> SelectAll();  //For getting all Shipping with Order Details
        Task<ShippingDetails> Create(ShippingDetails ShippingDetails); //Creating a new ShippingDetails entity

        Task<ShippingDetails> SelectById(int Id); //For getting Shipping
                                                  //by specific Id


    }
    public class ShippingDetailsRepository:IShippingDetailsRepository
    {
        private readonly ChicBotiqueDatabaseContext _context;  //making an instance of the class ChicBotiqueDatabaseContext

        public ShippingDetailsRepository(ChicBotiqueDatabaseContext context)   //dependency injection with parameter 
        {
            _context = context;
        }

        //**implementing the methods of IAuthorRepository interface**// 

        //This method will get all of the ShippingDetailss information 
        public async Task<List<ShippingDetails>> SelectAll()
        {

            return await _context.ShippingDetails.Include(e => e.Order)
                .ToListAsync();

        }

        //With this method one ShippingDetails's info can be added
        public async Task<ShippingDetails> Create(ShippingDetails ShippingDetails)
        {
            _context.ShippingDetails.Add(ShippingDetails);
            await _context.SaveChangesAsync();
            return ShippingDetails;
        }

        //This method will get one specific ShippingDetails info whoose ShippingDetailsId has been given 
        public async Task<ShippingDetails> SelectById(int Id)
        {
            return await _context.ShippingDetails.Include(a => a.Order).FirstOrDefaultAsync(u => u.Id == Id);
        }


      

    }
}
