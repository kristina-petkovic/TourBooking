using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Core.Model
{
    public class Entity
    {
        [Key]
        public int Id { get; set; }
        public bool Deleted { get; set; }
    }
}