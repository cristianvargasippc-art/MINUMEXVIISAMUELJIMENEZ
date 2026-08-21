namespace Delegame.Models;

public sealed record Grade(int Minimum, string Seal, string Title, string Description)
{
    private static readonly Grade[] Scale =
    [
        new(90, "◆", "Secretario General — Distinción Excepcional",
            "Dominio técnico y analítico de nivel superior. Capacidad demostrada para actuar bajo presión en escenarios de alta complejidad. Listo para liderar delegaciones internacionales en DELEGAME."),
        new(75, "★", "Embajador de Primera Clase",
            "Sólida comprensión de los principios y procedimientos diplomáticos. Con práctica adicional en los escenarios más desafiantes, alcanzarás el nivel de excelencia."),
        new(60, "●", "Diplomático Acreditado",
            "Captas los conceptos fundamentales y aplicas estrategias básicas. Identifica tus puntos débiles en procedimiento parlamentario y negociación avanzada."),
        new(0, "○", "Attaché en Formación",
            "La diplomacia se perfecciona con estudio constante. Repasa la Carta ONU, la Convención de Viena y bibliografía básica de negociación multilateral.")
    ];

    public static Grade For(int accuracy) => Scale.First(g => accuracy >= g.Minimum);
}
