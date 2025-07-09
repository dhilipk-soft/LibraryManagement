using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.DTOs.Loan
{
    public class LoanMemberDto
    {
        public Guid LoanId { get; set; }
        public Guid MemberId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueTime { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
