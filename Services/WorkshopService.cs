using Delegame.Models;

namespace Delegame.Services;

public sealed class WorkshopService
{
    private readonly QuestionBank _bank;
    private readonly DelegameStore _store;

    public WorkshopService(QuestionBank bank, DelegameStore store)
    {
        _bank = bank;
        _store = store;
        _store.Changed += () => Changed?.Invoke();
    }

    public event Action? Changed;

    public IReadOnlyList<Workshop> All()
    {
        var custom = _store.Read(d => d.CustomWorkshops.ToList());
        return [.. _bank.BaseWorkshops, .. custom];
    }

    public IReadOnlyList<Workshop> Custom() => _store.Read(d => d.CustomWorkshops.ToList());

    public IReadOnlyList<Workshop> Enabled() => [.. All().Where(w => IsEnabled(w.Key))];

    public Workshop? Find(string key) => All().FirstOrDefault(w => w.Key == key);

    public bool IsEnabled(string key) =>
        _store.Read(d => !d.WorkshopEnabled.TryGetValue(key, out var enabled) || enabled);

    public void SetEnabled(string key, bool enabled) =>
        _store.Mutate(d => d.WorkshopEnabled[key] = enabled);

    public Workshop Create(string name, List<Question> questions)
    {
        var workshop = new Workshop
        {
            Key = "custom_" + DateTime.UtcNow.Ticks,
            Name = name,
            Description = $"Taller personalizado · {questions.Count} preguntas",
            Level = "Personalizado",
            IsCustom = true,
            CreatedAt = DateTime.UtcNow,
            Questions = questions
        };

        _store.Mutate(d => d.CustomWorkshops.Add(workshop));
        return workshop;
    }

    public void Delete(string key) => _store.Mutate(d =>
    {
        d.CustomWorkshops.RemoveAll(w => w.Key == key);
        d.WorkshopEnabled.Remove(key);
    });
}
