﻿using Domain.Entities;
using Domain.Repositories.Borrowing;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Repositories
{
    public class BorrowingRepository : IBorrowingRepository, IQueryBorrowingRepository
    {
        private readonly LibraryContext _libraryContext;

        public BorrowingRepository(LibraryContext libraryContext)
        {
            this._libraryContext = libraryContext;
        }

        public async Task<BorrowingEntity> CreateBorrowing(BorrowingEntity borrowing, CancellationToken cancellationToken = default)
        {
            await _libraryContext.Borrowings
                .AddAsync(borrowing);
            await _libraryContext.SaveChangesAsync();
            return borrowing;
        }

        public async Task<List<BorrowingEntity>> ExpiringBorrowinsForUser(UserEntity user, CancellationToken cancellationToken = default)
        {
            var result = await _libraryContext.Borrowings
                .Where(e => e.BorrowedBy.Id == user.Id)
                .ToListAsync(cancellationToken);
            return result;
        }

        public async Task<BorrowingEntity?> ReturnBorrowing(int borrowingId, CancellationToken cancellationToken = default)
        {
            var borrowing = await _libraryContext.Borrowings
                .FindAsync(borrowingId, cancellationToken);
            if (borrowing == null)
            {
                return null;
            }
            borrowing.ReturnedAt = DateTime.Now;
            await _libraryContext.SaveChangesAsync();
            return borrowing;
        }

        public async Task<List<BorrowingEntity>> GetBorrowings(int pageNumber = 0, int pageSize = 100, CancellationToken cancellationToken = default)
        {
            var result = await _libraryContext.Borrowings
                .AsNoTracking()
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
            return result;
        }
    }
}