using Delegame.Services;
using Microsoft.AspNetCore.Components;

namespace Delegame.Components;

public abstract class FlowComponent : ComponentBase, IDisposable
{
    [Inject] protected GameFlow Flow { get; set; } = default!;

    protected override void OnInitialized() => Flow.Changed += Refresh;

    private Task Refresh() => InvokeAsync(StateHasChanged);

    public void Dispose() => Flow.Changed -= Refresh;
}
