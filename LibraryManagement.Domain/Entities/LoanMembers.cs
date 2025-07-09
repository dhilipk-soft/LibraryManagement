using LibraryManagement.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Entities
{
    public class LoanMembers
    {
        public Guid LoanId { get; set; }
        public  Loan Loan { get; set; }
        
        public Guid MemberId { get; set; }
        public Member Member { get; set; }
    }
}
