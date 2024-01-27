using H6_ChicBotique.Database.Entities;
using H6_ChicBotique.DTOs;
using H6_ChicBotique.Repositories;

namespace H6_ChicBotique.Services
{
    public interface IHomeAddressService
    {
        Task<List<HomeAddressResponse>> GetAllHomeAddresses();
        Task<HomeAddressResponse> GetHomeAddressById(int HomeAddressId);


        Task<HomeAddressResponse> UpdateHomeAddress(int HomeAddressId, HomeAddressRequest updateHomeAddress);
        // Task<HomeAddressResponse> DeletehippingAddress(int HomeAddressId);


    }
    public class HomeAddressService:IHomeAddressService
    {
        private readonly IHomeAddressRepository _HomeAddressRepository;
        public HomeAddressService(IHomeAddressRepository HomeAddressRepository)
        {
            _HomeAddressRepository = HomeAddressRepository;
        }
        public async Task<List<HomeAddressResponse>> GetAllHomeAddresses()
        {
            List<HomeAddress> HomeAddresss = await _HomeAddressRepository.SelectAll();

            return HomeAddresss.Select(HomeAddress => MapHomeAddressToHomeAddressResponse(HomeAddress)).ToList();

        }
        public async Task<HomeAddressResponse> GetHomeAddressById(int HomeAddressId)
        {
            HomeAddress HomeAddress = await _HomeAddressRepository.SelectById(HomeAddressId);

            if (HomeAddress != null)
            {

                return MapHomeAddressToHomeAddressResponse(HomeAddress);
            }
            return null;
        }




        public async Task<HomeAddressResponse> UpdateHomeAddress(int HomeAddressId, HomeAddressRequest updateHomeAddress)
        {
            HomeAddress local = await _HomeAddressRepository.SelectById(HomeAddressId);
            HomeAddress HomeAddress = MapHomeAddressRequestToHomeAddress(updateHomeAddress);
            HomeAddress.Id = HomeAddressId;
            HomeAddress.AccountInfoId = local.AccountInfoId;
            HomeAddress updatedHomeAddress = await _HomeAddressRepository.Update(HomeAddress);

            if (updatedHomeAddress != null)
            {

                return MapHomeAddressToHomeAddressResponse(updatedHomeAddress);
            }

            return null;
        }



        private HomeAddressResponse MapHomeAddressToHomeAddressResponse(HomeAddress HomeAddress)
        {

            return new HomeAddressResponse
            {
                Id = HomeAddress.Id,
                AccountId= HomeAddress.AccountInfoId,
                Address=HomeAddress.Address,
                City=HomeAddress.City,
                PostalCode=HomeAddress.PostalCode,
                Country=HomeAddress.Country,
                Phone=HomeAddress.TelePhone,


                Account = new AccountInfoResponse
                {
                    Id = HomeAddress.AccountInfo.Id,
                    CreatedDate=HomeAddress.AccountInfo.CreatedDate,
                    UserId=HomeAddress.AccountInfo.UserId

                }
            };

        }
        private static HomeAddress MapHomeAddressRequestToHomeAddress(HomeAddressRequest HomeAddressRequest)
        {
            return new HomeAddress()
            {


                Address = HomeAddressRequest.Address,
                City = HomeAddressRequest.City,
                PostalCode = HomeAddressRequest.PostalCode,
                Country=HomeAddressRequest.Country,
                TelePhone=HomeAddressRequest.Phone
            };
        }
    }
}
