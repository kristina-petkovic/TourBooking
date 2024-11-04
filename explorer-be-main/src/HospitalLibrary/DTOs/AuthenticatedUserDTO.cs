namespace HospitalLibrary.Core.DTOs
{
    public class AuthenticatedUserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int Role { get; set; }
        public string Token { get; set; }

    }
}