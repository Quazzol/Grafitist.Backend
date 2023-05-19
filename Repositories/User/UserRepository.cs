using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Grafitist.Connection;
using Grafitist.Misc;
using Grafitist.Models.User;
using Grafitist.Repositories.User.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Grafitist.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Delete(Guid id)
    {
        var user = await _context.Users!.FirstOrDefaultAsync(q => q.Id == id);
        if (user is null)
            return;

        _context.Users!.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<UserModel?> Get(Guid id)
    {
        return await _context.Users!.FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<IEnumerable<UserModel>> Get(Pager pager)
    {
        return await _context.Users!.Skip(pager.Count * (pager.No - 1)).Take(pager.Count).OrderBy(q => q.Id).ToListAsync();
    }

    public async Task<UserModel> Insert(UserModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(UserModel));
        model.Password = AesEncryption.Encrypt(model.Password);
        await _context.Users!.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<UserModel?> SignIn(UserModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(UserModel));
        var user = await _context.Users!.FirstOrDefaultAsync(q => q.Email == model.Email && q.Password == AesEncryption.Encrypt(model.Password));
        return user;
    }

    public async Task<UserModel> Update(UserModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(UserModel));
        var user = await _context.Users!.FirstOrDefaultAsync(q => q.Id == model.Id);
        if (user is null) throw new KeyNotFoundException($"Model.Id not found! {model.Id}");
        if (!model.Password.IsEmpty() && !IsValidPassword(model.Password)) throw new ValidationException("Password is not valid");

        user.Type = model.Type;
        user.Name = model.Name;
        user.Surname = model.Surname;
        user.Email = model.Email;
        if (!model.Password.IsEmpty())
            user.Password = AesEncryption.Encrypt(model.Password);
        user.Phone = model.Phone;
        await _context.SaveChangesAsync();
        return user;
    }

    private bool IsValidPassword(string? password)
    {
        if (password is null || password.IsEmpty()) return false;
        string pattern = @"[a-zA-Z0-9@$._]+$";
        return password.Length >= 8 && password.Length <= 16 && Regex.IsMatch(password, pattern);
    }
}