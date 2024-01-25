namespace H6_ChicBotique.DTOs
{
    public class AccountInfoResponse
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public int? UserId { get; set; }
      
        public HomeAddressResponse HomeAddress { get; set; }
        public UserResponse User { get; set; }
    }
}
