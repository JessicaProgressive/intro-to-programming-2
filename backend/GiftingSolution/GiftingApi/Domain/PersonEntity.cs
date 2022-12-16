namespace GiftingApi.Domain
{
    public class PersonEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }  = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool UnFreinded{ get; set; } = false;
    }
}
