﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UMan.Core;
using UMan.Core.Repositories;
using UMan.DataAccess.Repositories.Exceptions;

namespace UMan.DataAccess.Repositories
{
    public class PapersRepository : IRepository<Core.Paper>
    {
        private readonly PapersDbContext _context;

        private readonly IMapper _mapper;

        public PapersRepository(PapersDbContext context, IMapper mapper)
        {
            _context = context;

            _mapper = mapper;
        }

        public async Task<int> Add(Paper entity, CancellationToken cancellationToken = default)
        {

            Entities.Paper paper = _mapper.Map<Core.Paper, Entities.Paper>(entity);

            await _context.Papers!.AddAsync(paper);

            await _context.SaveChangesAsync();

            return paper.Id;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            Entities.Paper? paper = await _context.Papers!.FindAsync(new object[] { id }, cancellationToken);

            if (paper == null)
            {
                throw new EntityNotFoundException<int>(id);
            }
            else
            {
                _context.Papers!.Remove(paper);

                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
        }

        public async Task<Paper[]> Get(CancellationToken cancellationToken = default)
        {
            Entities.Paper[] papers = await _context.Papers!
               .AsNoTracking()
               .ToArrayAsync(cancellationToken);

            return _mapper.Map<Entities.Paper[], Core.Paper[]>(papers);
        }

        public async Task<Paper> Get(int id, CancellationToken cancellationToken = default)
        {
            Entities.Paper? paper = await _context.Papers!
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id);

            if (paper == null)
            {
                throw new EntityNotFoundException<int>(id);
            }
            else
            {
                return _mapper.Map<Entities.Paper, Core.Paper>(paper);
            }
        }

        public async Task<int> Update(int id, Paper entity, CancellationToken cancellationToken = default)
        {
            Entities.Paper? paper = await _context.Papers!
                .FindAsync(new object[] { id }, cancellationToken);

            if (paper != null)
            {
                _context.Papers.Update(_mapper.Map<Core.Paper, Entities.Paper>(entity));

                if (entity.Author != null)
                {
                    _context.Entry(paper!.Author).State = EntityState.Modified;
                }

                _context.Entry(paper!).State = EntityState.Modified;

                await _context.SaveChangesAsync(cancellationToken);

                return paper!.Id;
            }
            else
            {
                throw new EntityNotFoundException<int>(id);
            }
        }
    }
}