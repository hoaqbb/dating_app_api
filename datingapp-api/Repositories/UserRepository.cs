using AutoMapper;
using AutoMapper.QueryableExtensions;
using datingapp_api.Data.Entities;
using datingapp_api.DTOs;
using datingapp_api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace datingapp_api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatingAppContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DatingAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MemberDto> GetMemberByUsernameAsync(string username)
        {
            return await _context.Users
                .Where(x => x.UserName == username)
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _context.Users
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(User user)
        {
            //    _context.Users.Update
            //    _context.Entry(user).State = EntityState.Modified;
        }
    }
}
