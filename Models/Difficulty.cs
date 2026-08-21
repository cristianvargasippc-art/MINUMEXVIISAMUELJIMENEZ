namespace Delegame.Models;

public enum Difficulty
{
    Adjunto = 1,
    Embajador = 2,
    SecretarioGeneral = 3
}

public static class DifficultyRules
{
    public static int Seconds(this Difficulty difficulty) => difficulty switch
    {
        Difficulty.Adjunto => 40,
        Difficulty.Embajador => 25,
        Difficulty.SecretarioGeneral => 15,
        _ => 30
    };

    public static string Label(this Difficulty difficulty) => difficulty switch
    {
        Difficulty.Adjunto => "Adjunto",
        Difficulty.Embajador => "Embajador",
        Difficulty.SecretarioGeneral => "Sec. General",
        _ => "Delegado"
    };
}
