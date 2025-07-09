using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Model
{
    public class AddLoanDto
    {
        [Required]
        public Guid BookId { get; set; }

        [Required]
        public Guid MemberId { get; set; }

        [Required]
        public DateTime DueTime { get; set; }
    }
}
