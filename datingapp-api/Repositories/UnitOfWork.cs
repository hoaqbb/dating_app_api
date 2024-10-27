using AutoMapper;
using datingapp_api.Data.Entities;
using datingapp_api.Interfaces;

namespace datingapp_api.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatingAppContext _context;
        private readonly IMapper _mapper;

        public UnitOfWork(DatingAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IUserRepository UserRepository => new UserRepository(_context, _mapper);

        public IMessageRepository MessageRepository => new MessageRepository(_context, _mapper);

        public ILikesRepository LikesRepository => new LikesRepository(_context);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}
