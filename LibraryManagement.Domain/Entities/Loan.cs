using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Model.Entities
{
    public class Loan
    {
        public Guid LoanId { get; set; }

        public Guid BookId { get; set; }

        public Book Book { get; set; }

        public Guid MemberId { get; set; }

        public Member Member { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime DueTime { get; set; }

        public DateTime? ReturnDate { get; set; }

    }
}
