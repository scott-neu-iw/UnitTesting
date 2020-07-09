namespace Unit.UserProcessing.Data.Models
{
    public interface IUserPrincipal
    {
        string EmailAddress { get; }
        string GivenName { get; }
        string EmployeeId { get; }
        string MiddleName { get; }
        string Surname { get; }
        bool? Enabled { get; }
    }
}
