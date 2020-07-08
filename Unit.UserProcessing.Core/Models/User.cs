namespace Unit.UserProcessing.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }

        public Data.Models.User ToDataModel()
        {
            return new Data.Models.User
            {
                FirstName = FirstName,
                LastName = LastName,
                EmailAddress = EmailAddress,
                IsActive = true
            };
        }
    }
}
