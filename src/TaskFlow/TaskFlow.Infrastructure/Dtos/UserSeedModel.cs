namespace TaskFlow.Infrastructure.Dtos
{
    public class UserSeedModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
    }
}
