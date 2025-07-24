using LibraryManagement.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly LibraryContext _context;

        public MemberRepository(LibraryContext context) 
        {
            _context = context;
        }

        public async Task<Member?> GetByIdAsync(Guid id)
            => await _context.Members.FirstOrDefaultAsync(m => m.MemberId == id);

        public async Task<List<Member>> GetAllAsync()
            => await _context.Members.ToListAsync();

        public async Task<bool> ExistsByEmailOrPhoneAsync(string email, string phone)
            => await _context.Members.AnyAsync(m => m.Email == email || m.Phone == phone);

        public async Task AddAsync(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Member member)
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Member member)
        {
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
        }
        public async Task<Member?> GetByPhoneAsync(string phone)
        {
            return await _context.Members
                .FirstOrDefaultAsync(m => m.Phone == phone);
        }

        public async Task UpdateRefreshTokenAsync(Member member, string refreshToken, DateTime expiryTime)
        {
            member.RefreshToken = refreshToken;
            member.RefreshTokenExpiryTime = expiryTime;
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
        }
    }
}
