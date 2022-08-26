using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UMan.Core;
using UMan.Core.Repositories;
using UMan.DataAccess.Repositories.Exceptions;

namespace UMan.DataAccess.Repositories
{
    public class AuthorsRepository : IRepository<Core.Author>
    {
        private readonly PapersDbContext _context;

        private readonly IMapper _mapper;

        public AuthorsRepository(PapersDbContext context, IMapper mapper)
        {
            _context = context;

            _mapper = mapper;
        }
        public async Task<int> Add(Author entity, CancellationToken cancellationToken = default)
        {
            Entities.Author author = _mapper.Map<Core.Author, Entities.Author>(entity);

            await _context.Authors!.AddAsync(author, cancellationToken);

            _context.Entry(author).State = EntityState.Added;

            await _context.SaveChangesAsync(cancellationToken);

            return author.Id;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            var author = await _context.Authors!.FindAsync(new object[] { id }, cancellationToken);

            if (author == null)
            {
                throw new EntityNotFoundException<int>(id);
            }
            else
            {
                _context.Authors.Remove(author);

                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
        }

        public async Task<Author[]> Get(CancellationToken cancellationToken = default)
        {
            Entities.Author[] authors = await _context.Authors!
                 .AsNoTracking()
                 .ToArrayAsync(cancellationToken);

            return _mapper.Map<Entities.Author[], Core.Author[]>(authors);
        }

        public async Task<Author> Get(int id, CancellationToken cancellationToken = default)
        {
            Entities.Author? author = await _context.Authors!
                .Include(a => a.Papers)
                .AsNoTracking()
                .SingleOrDefaultAsync(a => a.Id == id, cancellationToken);

            if (author == null)
            {
                throw new EntityNotFoundException<int>(id);
            }
            else
            {
                return _mapper.Map<Entities.Author, Core.Author>(author);
            }
        }

        public async Task<int> Update(int id, Author entity, CancellationToken cancellationToken = default)
        {
            Entities.Author? author = await _context.Authors!
                .FindAsync(new object[] { id }, cancellationToken);

            if (author != null)
            {
                _context.Authors.Update(_mapper.Map<Core.Author, Entities.Author>(entity));

                if (entity.Papers != null)
                {
                    _context.Entry(author!.Papers).State = EntityState.Modified;
                }

                _context.Entry(author!).State = EntityState.Modified;

                await _context.SaveChangesAsync(cancellationToken);

                return author!.Id;
            }
            else
            {
                throw new EntityNotFoundException<int>(id);
            }
        }
    }
}
