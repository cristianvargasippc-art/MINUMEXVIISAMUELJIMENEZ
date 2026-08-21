using Delegame.Models;

namespace Delegame.Components.Screens;

public abstract class ScreenBase : FlowComponent
{
    protected abstract Screen Id { get; }

    protected bool IsActive => Flow.Screen == Id;

    protected string ScreenClass => IsActive ? "screen on" : "screen";
}
