using H6_ChicBotique.Database;
using H6_ChicBotique.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace H6_ChicBotique.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> AddPayment(Payment payment);
    }
    public class PaymentRepository:IPaymentRepository
    {
        private readonly ChicBotiqueDatabaseContext _context;

        public PaymentRepository(ChicBotiqueDatabaseContext context)
        {
            _context = context;
        }
        public async Task<Payment> AddPayment(Payment payment)
        {
            _context.Payment.Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }
    }
}
