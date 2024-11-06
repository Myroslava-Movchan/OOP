using System.ComponentModel.DataAnnotations;

namespace Online_Store_Management.DataAccess
{
    public class CustomerDbModel
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public required string? LastName { get; set; }
        public int PostIndex { get; set; }
    }
}
