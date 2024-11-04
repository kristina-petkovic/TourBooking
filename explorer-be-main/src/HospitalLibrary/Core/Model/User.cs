using HospitalLibrary.Core.Model.Enum;

namespace HospitalLibrary.Core.Model
{
    public class User: Entity
    {
        
        public int IssueCount { get; set; }
        public bool Malicious { get; set; }
        public bool Blocked  { get; set; }
        public int AuthorPoints  { get; set; }
        public bool TopAuthor  { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        public string Email { get; set; }

        public Role Role { get; set; }
    }
}