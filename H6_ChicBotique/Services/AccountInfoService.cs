using H5_Webshop.Database.Entities;
using H6_ChicBotique.DTOs;
using H6_ChicBotique.Repositories;

namespace H6_ChicBotique.Services
{
    public interface IAccountInfoService
    {
        Task<List<AccountInfoResponse>> GetAll();
        
        Task<AccountInfoResponse> GetById(Guid AccountId);
        Task<Guid> GetGuidIdByUserId(int userId);


    }
    public class AccountInfoService : IAccountInfoService
    {
        private readonly AccountInfoRepository _AccountRepository;
        private readonly IPasswordEntityRepository _PasswordEntityRepository;
        private readonly IHomeAddressRepository _ShippingDetailsRepository;
        private readonly AccountInfoRepository _accountRepository;
        private readonly IJwtUtils _jwtUtils;



        public AccountInfoService(AccountInfoRepository AccountRepository, IPasswordEntityRepository PasswordEntityRepository, IShippingDetailsRepository shippingDetailsRepository, AccountInfoRepository accountRepository, IJwtUtils jwtUtils)
        {
            _PasswordEntityRepository = PasswordEntityRepository;
            // _ShippingDetailsRepository = shippingDetailsRepository;
            _AccountRepository = AccountRepository;
            _accountRepository = accountRepository;
            _jwtUtils = jwtUtils;

        }




        public async Task<List<AccountInfoResponse>> GetAll()
        {

            List<AccountInfo> Accounts = await _AccountRepository.GetAll();


            return Accounts.Select(acc => MapAccountToAccountResponse(acc)).ToList();

            //    public Guid Id { get; set; }

            //public DateTime CreatedDate { get; set; }
            //public int? AccountId { get; set; }
            //public IEnumerable<Order> Orders { get; set; }
            //public ShippingAddress ShippingAddress { get; set; }
            //public AccountInfo AccountInfo { get; set; }
        }










        public async Task<AccountInfoResponse> GetById(Guid AccountId)
        {
            AccountInfo Account = await _AccountRepository.GetById(AccountId);

            if (Account != null)
            {

                return MapAccountToAccountResponse(Account);
            }
            return null;
        }


        public async Task<Guid> GetGuidIdByUserId(int userId)
        {
            var userGuid = await _AccountRepository.GetGuidByUserId(userId);

            if (userGuid != null)
            {

                return userGuid;
            }
            return Guid.Empty;
        }



        private static AccountInfoResponse MapAccountToAccountResponse(AccountInfo Account)
        {
            if (Account == null)
            {
                throw new Exception("AccountInfo value was null");
            }
            var acc = Account == null ? null : new AccountInfoResponse
            {
                Id = Account.Id,
                UserId=Account.UserId,
                User=new UserResponse
                {
                    Id=Account.User.Id,
                    FirstName=Account.User.FirstName,
                    LastName=Account.User.LastName,
                    Email=Account.User.Email,
                    Role=Account.User.Role
                },
                CreatedDate = Account.CreatedDate,
                Orders = Account.Orders?.Select(Order => new OrderAndPaymentResponse
                {
                    Id = Order.Id,
                    OrderDate = Order.OrderDate,
                    AccountId = Order.AccountInfo.Id,
                    Status=Order.Payment.Status,
                    TransactionId=Order.Payment.TransactionId,

                    //Amount=Order.Payment.Amount,
                    OrderDetails = Order.OrderDetails.Select(order => new OrderDetailResponse
                    {
                        Id = order.Id,
                        ProductId = order.ProductId,
                        ProductTitle = order.ProductTitle,
                        ProductPrice = order.ProductPrice,
                        Quantity = order.Quantity


                    }).ToList()
                }).ToList(),

            };
            if (Account.HomeAddress != null)
            {
                acc.HomeAddress = new HomeAddressResponse
                {
                    AccountId = Account.Id,
                    Id = Account.HomeAddress.Id,
                    Address = Account.HomeAddress.Address,
                    City = Account.HomeAddress.City,
                    PostalCode = Account.HomeAddress.PostalCode,
                    Country = Account.HomeAddress.Country,
                    Phone = Account.HomeAddress.TelePhone

                };
            }
            return acc;
        }
    }
