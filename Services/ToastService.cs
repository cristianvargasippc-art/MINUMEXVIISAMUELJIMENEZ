namespace Delegame.Services;

public enum ToastKind
{
    Info,
    Success,
    Warn,
    Error
}

public sealed class Toast
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Message { get; init; } = string.Empty;
    public ToastKind Kind { get; init; } = ToastKind.Info;
    public bool Leaving { get; set; }

    public string CssClass => "toast toast-" + Kind.ToString().ToLowerInvariant();
}

public sealed class ToastService
{
    private readonly List<Toast> _toasts = [];

    public event Func<Task>? Changed;

    public IReadOnlyList<Toast> Current
    {
        get
        {
            lock (_toasts) return [.. _toasts];
        }
    }

    public void Info(string message, int ms = 3000) => Show(message, ToastKind.Info, ms);

    public void Success(string message, int ms = 3000) => Show(message, ToastKind.Success, ms);

    public void Warn(string message, int ms = 3000) => Show(message, ToastKind.Warn, ms);

    public void Error(string message, int ms = 4000) => Show(message, ToastKind.Error, ms);

    public void Show(string message, ToastKind kind, int ms)
    {
        var toast = new Toast { Message = message, Kind = kind };

        lock (_toasts) _toasts.Add(toast);
        _ = Notify();
        _ = Expire(toast, ms);
    }

    private async Task Expire(Toast toast, int ms)
    {
        await Task.Delay(ms);
        toast.Leaving = true;
        await Notify();

        await Task.Delay(300);
        lock (_toasts) _toasts.Remove(toast);
        await Notify();
    }

    private Task Notify() => Changed?.Invoke() ?? Task.CompletedTask;
}
