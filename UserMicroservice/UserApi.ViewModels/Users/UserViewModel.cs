namespace UserApi.ViewModels.Users
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string CellPhoneNumber { get; set; }

        public int Role { get; set; }

        public int Gender { get; set; }
    }
}
