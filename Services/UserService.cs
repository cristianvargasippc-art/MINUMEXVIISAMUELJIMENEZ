using Delegame.Models;

namespace Delegame.Services;

public sealed record AuthResult(bool Success, string Message, AppUser? User = null);

public sealed class UserService
{
    private readonly DelegameStore _store;

    public UserService(DelegameStore store)
    {
        _store = store;
        _store.Changed += () => Changed?.Invoke();
    }

    public event Action? Changed;

    public IReadOnlyList<AppUser> All() =>
        _store.Read(d => d.Users.OrderByDescending(u => u.IsMaster).ThenBy(u => u.Name).ToList());

    public AuthResult Authenticate(string name, string password)
    {
        var user = _store.Read(d => d.Users
            .FirstOrDefault(u => string.Equals(u.Name, name, StringComparison.OrdinalIgnoreCase)));

        if (user is null || !PasswordHasher.Verify(password, user.PasswordHash))
            return new AuthResult(false, "Usuario o contraseña incorrectos.");

        if (user.Disabled)
            return new AuthResult(false, "Esta cuenta está desactivada.");

        return new AuthResult(true, $"Bienvenido, {user.Name}", user);
    }

    public AuthResult Create(string name, string password, string role)
    {
        if (name.Length < 3)
            return new AuthResult(false, "Mínimo 3 caracteres en el usuario.");

        if (password.Length < 6)
            return new AuthResult(false, "La contraseña debe tener al menos 6 caracteres.");

        var exists = _store.Read(d => d.Users
            .Any(u => string.Equals(u.Name, name, StringComparison.OrdinalIgnoreCase)));

        if (exists) return new AuthResult(false, "Ese usuario ya existe.");

        var user = new AppUser { Name = name, Role = role, PasswordHash = PasswordHasher.Hash(password) };
        _store.Mutate(d => d.Users.Add(user));
        return new AuthResult(true, $"Usuario \"{name}\" creado como {role}.", user);
    }

    public string ResetPassword(string id)
    {
        var password = PasswordHasher.Generate();
        _store.Mutate(d =>
        {
            var user = d.Users.FirstOrDefault(u => u.Id == id);
            if (user is not null) user.PasswordHash = PasswordHasher.Hash(password);
        });
        return password;
    }

    public void ToggleDisabled(string id) => _store.Mutate(d =>
    {
        var user = d.Users.FirstOrDefault(u => u.Id == id);
        if (user is not null && !user.IsMaster) user.Disabled = !user.Disabled;
    });

    public void Delete(string id) => _store.Mutate(d => d.Users.RemoveAll(u => u.Id == id && !u.IsMaster));
}
