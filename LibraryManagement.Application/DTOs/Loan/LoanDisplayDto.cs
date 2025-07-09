using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.DTOs.Loan
{
    public class LoanDisplayDto
    {
        public Guid LoanId { get; set; }

        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public Guid MemberId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime DueTime { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
