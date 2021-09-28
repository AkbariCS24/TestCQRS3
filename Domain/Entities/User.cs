namespace TestCQRS3.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int UserRoleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public UserRole UserRole { get; set; }
    }
}
