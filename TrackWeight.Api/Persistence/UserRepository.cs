﻿using Microsoft.EntityFrameworkCore;
using TrackWeight.Api.Models;

namespace TrackWeight.Api.Persistence;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> CreateUserAsync(
        string firstName,
        string lastName,
        string email,
        string passwordHash)
    {
        await _dbContext.Users.AddAsync(new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PasswordHash = passwordHash,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow
        });

        await _dbContext.SaveChangesAsync();

        return await GetByEmailAsync(email);
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _dbContext.Users.FirstAsync(u => u.Email == email);
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        return await _dbContext.Users.FirstAsync(u => u.Id == id);
    }

    public async Task<User> UpdateAsync(
        Guid userId,
        string? firstName,
        string? lastName,
        string? email,
        string? passwordHash)
    {
        var user = await _dbContext.Users.FindAsync(userId); 

        if (user is null)
        {
            throw new Exception("User not found");
        }

        if (firstName is not null)
            user.FirstName = firstName;

        if (lastName is not null)
            user.LastName = lastName;

        if (email is not null)
            user.Email = email;

        if (passwordHash is not null)
            user.PasswordHash = passwordHash;

        await _dbContext.SaveChangesAsync(); 

        return user;
    }
}
