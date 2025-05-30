﻿using Microsoft.EntityFrameworkCore;
using HMS_API.DbContexts;
using HMS_API.Models;
using HMS_API.Repository.IRepository;

namespace HMS_API.Repository
{
    public class RefreshTokenRepository:IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _db;
        public RefreshTokenRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        //private readonly List<RefreshToken> _refreshTokens = new List<RefreshToken>();
        public async Task Create(RefreshToken refreshToken)
        {
            _db.RefreshTokens.Add(refreshToken);
            await _db.SaveChangesAsync();
        }

        public async Task<RefreshToken> GetByToken(string token)
        {
            var refreshToken = await _db.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
            return refreshToken;
        }

        public async Task Delete(Guid id)
        {
            var refreshToken = await _db.RefreshTokens.FindAsync(id);
            if (refreshToken != null)
            {
                _db.RefreshTokens.Remove(refreshToken);
                await _db.SaveChangesAsync();
            }

        }

        public async Task DeleteAll(string userId)
        {
            var refreshTokens = await _db.RefreshTokens.Where(t => t.UserId == userId).ToListAsync();
            _db.RefreshTokens.RemoveRange(refreshTokens);
            await _db.SaveChangesAsync();
        }
    }
}
