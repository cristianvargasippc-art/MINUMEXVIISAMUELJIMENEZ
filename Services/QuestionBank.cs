using Delegame.Models;

namespace Delegame.Services;

public sealed class QuestionBank
{
    public IReadOnlyList<Workshop> BaseWorkshops { get; } =
    [
        new()
        {
            Key = "oratoria",
            Name = "Oratoria Parlamentaria",
            Description = "Discursos bajo presión, gestión de imprevistos y arquitectura del argumento.",
            Level = "Avanzado",
            Questions =
            [
                new()
                {
                    Text = "Durante tu intervención, la conexión a internet cae cuando ibas a mostrar evidencia visual clave. Los delegados de Rusia y China intercambian sonrisas. ¿Qué haces?",
                    Context = "Tienes 3 minutos de tiempo asignado restante.",
                    Options =
                    [
                        "Pides un receso técnico de 20 minutos.",
                        "Sonríes, dices 'Continuemos al estilo clásico' y reformulas el argumento de memoria con más convicción.",
                        "Lees textualmente tus notas sin mirar a nadie.",
                        "Cedes el turno de palabra."
                    ],
                    Correct = 1,
                    Explanation = "Improvisar con solidez argumental es la firma del orador de alto nivel. El dominio del tema debe ser suficiente para prescindir de apoyos visuales — y hacerlo con calma convierte el imprevisto en una demostración de autoridad.",
                    Principle = "Principio: Adaptabilidad y dominio del contenido bajo presión técnica."
                },
                new()
                {
                    Text = "El delegado de Francia acaba de desmontar tu punto principal sobre autodeterminación. La sala entera espera tu réplica. Tienes 2 minutos. ¿Cuál es tu estructura?",
                    Context = "La réplica debe referirse directamente al argumento impugnado.",
                    Options =
                    [
                        "Ignoras el argumento y reiteras tu posición inicial.",
                        "Atacas la credibilidad del delegado francés.",
                        "Reconoces los puntos válidos ANTES de presentar tu contra-argumento con evidencia nueva.",
                        "Solicitas más tiempo de preparación."
                    ],
                    Correct = 2,
                    Explanation = "La técnica 'reconocimiento concesivo + reencuadre' es estándar en debates parlamentarios de alto nivel. Reconocer la validez parcial demuestra madurez analítica.",
                    Principle = "Técnica: Reconocimiento concesivo + reencuadre argumentativo."
                },
                new()
                {
                    Text = "Te informan que solo tienes 1 minuto más — la mitad de lo acordado. Tu argumento central aún no fue expuesto. ¿Qué haces?",
                    Context = "El argumento central está en el párrafo 5 de tus notas. Ya vas en el 3.",
                    Options =
                    [
                        "Continúas al ritmo normal y excedes el tiempo.",
                        "Protestas formalmente exigiendo los minutos completos.",
                        "Comprimes la intervención: saltas a la tesis central y cierras con una conclusión impactante.",
                        "Te detienes sin llegar a la conclusión."
                    ],
                    Correct = 2,
                    Explanation = "La síntesis ejecutiva bajo presión distingue a un orador competente de uno excelente.",
                    Principle = "Habilidad: Síntesis ejecutiva bajo restricciones de tiempo."
                },
                new()
                {
                    Text = "Un delegado dormido ronca durante tu discurso. Varios delegados se ríen. ¿Tu movimiento?",
                    Context = "45 delegados presentes. Tema: financiamiento climático.",
                    Options =
                    [
                        "Te ofendes y pides orden al presidente.",
                        "Subes el volumen para imponerte al ruido.",
                        "Pausas y dices: 'Parece que mi colega sueña con un mundo más justo. Asegurémonos de que al despertar, lo convenzamos.' Y continúas con más energía.",
                        "Te apresuras para terminar."
                    ],
                    Correct = 2,
                    Explanation = "Convertir un momento incómodo en un one-liner memorable es nivel élite. Demostraste ingenio, control y carisma bajo presión.",
                    Principle = "Técnica: Gestión dinámica de la atención mediante humor controlado."
                },
                new()
                {
                    Text = "Tu país es acusado con evidencia parcialmente correcta de incumplir la Resolución 2334. ¿Cuál es la respuesta más inteligente?",
                    Context = "La acusación se hace en debate general, no en procedimiento formal.",
                    Options =
                    [
                        "Niegas categóricamente todas las acusaciones.",
                        "Reconoces los aspectos factuales correctos, contextualizas y presentas acciones correctivas.",
                        "Contraatacas con acusaciones al acusador.",
                        "Guardas silencio para no escalar."
                    ],
                    Correct = 1,
                    Explanation = "Negar hechos verificables destruye credibilidad. El reconocimiento contextualizado con plan de acción convierte una debilidad en responsabilidad.",
                    Principle = "Estrategia: Reconocimiento responsable con marco de solución."
                },
                new()
                {
                    Text = "Tienes exactamente 90 segundos para posicionarte en el debate de apertura sobre Reforma del Consejo de Seguridad. ¿Cuál es la estructura óptima?",
                    Options =
                    [
                        "Antecedentes históricos (60s) + posición actual (30s).",
                        "Tesis central (15s) + dos argumentos clave (50s) + llamado a la acción (25s).",
                        "Saludo protocolario extenso (25s) + introducción personal (20s) + propuesta (45s).",
                        "Lectura íntegra del comunicado oficial."
                    ],
                    Correct = 1,
                    Explanation = "En intervenciones cortas, la estructura Tesis-Argumentos-Llamado a la acción maximiza el impacto. Los saludos extensos consumen tiempo sin valor persuasivo.",
                    Principle = "Estructura: Modelo TCA para intervenciones de tiempo limitado."
                },
                new()
                {
                    Text = "Debes defender una posición con la que estás en desacuerdo pero que tu delegación te asignó. ¿Qué es éticamente correcto?",
                    Options =
                    [
                        "Expresas públicamente tu desacuerdo personal.",
                        "Te niegas a dar el discurso.",
                        "Defiendes la posición asignada con máxima competencia técnica, separando tu rol de tus opiniones.",
                        "Das el discurso pero incluyes señales implícitas de desacuerdo."
                    ],
                    Correct = 2,
                    Explanation = "La distinción entre posición personal e institucional es fundamental. Un diplomático que expresa disidencia pública viola el principio de representación.",
                    Principle = "Principio: Disciplina institucional y separación rol-persona."
                },
                new()
                {
                    Text = "A mitad del discurso notas que las cifras que memorizaste difieren del documento oficial. ¿Qué haces?",
                    Context = "Los datos son la base central de tu argumento sobre deuda climática.",
                    Options =
                    [
                        "Usas las cifras de memoria para no perder el ritmo.",
                        "Pausas brevemente, señalas el documento oficial y citas la cifra con exactitud mencionando la fuente.",
                        "Omites los datos y continúas solo con argumentos cualitativos.",
                        "Promedias ambas cifras."
                    ],
                    Correct = 1,
                    Explanation = "En diplomacia, la precisión factual es no negociable. Una cifra incorrecta puede ser impugnada públicamente y destruir el argumento completo.",
                    Principle = "Principio: Integridad factual y verificación en tiempo real."
                }
            ]
        },
        new()
        {
            Key = "negociacion",
            Name = "Negociación Multilateral",
            Description = "Coaliciones, bloqueos, tácticas de última hora y mediación de alto nivel.",
            Level = "Avanzado",
            Questions =
            [
                new()
                {
                    Text = "Buscas los votos del Grupo Africano. Uno te ofrece su apoyo a cambio de votar contra una enmienda de tu aliado estratégico. ¿Qué haces?",
                    Context = "La votación es mañana. Todavía necesitas 4 votos más.",
                    Options =
                    [
                        "Aceptas: los votos africanos son más urgentes ahora.",
                        "Rechazas y buscas otros votos.",
                        "Exploras si existe una modificación técnica a la enmienda que satisfaga a ambas partes.",
                        "Finges aceptar pero luego votas como quieras."
                    ],
                    Correct = 2,
                    Explanation = "Las negociaciones raramente son binarias. La búsqueda de una tercera opción es la movida del negociador avanzado.",
                    Principle = "Táctica: Transformar opciones binarias en negociaciones multipartitas."
                },
                new()
                {
                    Text = "La contraparte llega tarde, interrumpe y hace demandas de último minuto sistemáticamente. El tema es acceso humanitario urgente. ¿Cómo respondes?",
                    Options =
                    [
                        "Respondes de forma similar para establecer reciprocidad.",
                        "Ignoras el comportamiento y sigues negociando.",
                        "Nombras el patrón explícitamente sin acusaciones personales y propones procedimientos estructurados.",
                        "Abandonas la negociación y reportas al Secretario General."
                    ],
                    Correct = 2,
                    Explanation = "Nombrar el comportamiento disruptivo sin atacar a la persona desactiva la táctica sin escalar el conflicto.",
                    Principle = "Técnica: Metacomunicación y estructuración del proceso."
                },
                new()
                {
                    Text = "Tu delegación hizo tres concesiones para construir confianza. La contraparte no ha concedido nada. Faltan 2 horas para el cierre. ¿Qué haces?",
                    Options =
                    [
                        "Continúas concediendo: la buena voluntad será correspondida.",
                        "Retiras las concesiones y reinicias desde posiciones duras.",
                        "Haces explícito el desbalance sin hostilidad y estableces que la siguiente concesión debe ser de ellos.",
                        "Escala el conflicto y pide mediación."
                    ],
                    Correct = 2,
                    Explanation = "Hacer explícito el desbalance — 'Hemos avanzado en tres frentes; necesitamos movimiento de su lado' — reequilibra la dinámica sin romper el diálogo.",
                    Principle = "Principio: Reciprocidad explícita como condición de sostenibilidad."
                },
                new()
                {
                    Text = "Se filtra un documento confidencial con tus líneas rojas reales. La contraparte claramente tiene acceso. La sesión de cierre es en 30 minutos. ¿Cómo procedes?",
                    Options =
                    [
                        "Actúas como si la filtración no hubiera ocurrido.",
                        "Suspendes la negociación y denuncias el espionaje.",
                        "Reconoces la situación, reevalúas qué información sigue siendo confidencial y renegocies las premisas.",
                        "Proporcionas información falsa para confundir."
                    ],
                    Correct = 2,
                    Explanation = "Cuando una filtración es conocida, fingir que no ocurrió destruye credibilidad. La transparencia estratégica restaura paridad informativa.",
                    Principle = "Estrategia: Transparencia proactiva ante ventajas informativas comprometidas."
                },
                new()
                {
                    Text = "Un estado pequeño tiene el voto decisivo. Tres potencias lo presionan con incentivos económicos que no puedes igualar. Mencionó preocupaciones por falta de representación. ¿Qué haces?",
                    Options =
                    [
                        "Renuncias a ese voto.",
                        "Ofreces los mismos incentivos económicos aunque excedan tu mandato.",
                        "Identificas la necesidad subyacente —representación— y ofreces mecanismos concretos de inclusión.",
                        "Alertas a las potencias competidoras para que anulen sus incentivos."
                    ],
                    Correct = 2,
                    Explanation = "Detrás de cada demanda existe una necesidad más profunda. Un asiento en un subcomité puede superar cualquier incentivo económico.",
                    Principle = "Principio de Fisher & Ury: Negociar sobre intereses, no posiciones."
                },
                new()
                {
                    Text = "Dos aliados clave tienen un conflicto bilateral que bloquea tu coalición. Ambos te piden que elijas bando. La coalición requiere unanimidad. ¿Tu rol?",
                    Options =
                    [
                        "Apoyas al aliado con quien tienes relación más antigua.",
                        "Te niegas a involucrarte.",
                        "Asumes el rol de mediador informal separando el conflicto bilateral de los objetivos comunes.",
                        "Amenazas a ambos con abandonar la coalición."
                    ],
                    Correct = 2,
                    Explanation = "El mediador que separa diferencias bilaterales de objetivos colectivos se convierte en el poder central de cualquier coalición.",
                    Principle = "Rol: Honest broker con interés en el resultado colectivo."
                },
                new()
                {
                    Text = "Tienes el acuerdo casi cerrado cuando la contraparte introduce una demanda completamente nueva en la última hora. ¿Cómo respondes?",
                    Context = "La nueva demanda implica concesión sustancial sobre soberanía de recursos.",
                    Options =
                    [
                        "Cedes para no perder el acuerdo casi cerrado.",
                        "Rechazas el acuerdo completo y reinicias desde cero.",
                        "Nombras la táctica explícitamente, señalas que adiciones requerirán reabrir puntos a tu favor y propones extender la sesión.",
                        "Aceptas condicionalmente sujeto a revisión."
                    ],
                    Correct = 2,
                    Explanation = "La táctica 'last-minute add' funciona por la presión del tiempo. Nombrarla neutraliza su efectividad.",
                    Principle = "Táctica: Identificación y neutralización del last-minute add."
                },
                new()
                {
                    Text = "Presentas una resolución con 9 votos asegurados pero Rusia o China pueden vetar. Tema: protección de civiles en conflicto activo. ¿Estrategia antes de la votación?",
                    Options =
                    [
                        "Presentas tal como está y asumes el veto como resultado aceptable.",
                        "Retiras la resolución para preservar credibilidad del Consejo.",
                        "Entablas consultas confidenciales con Rusia y China para identificar modificaciones mínimas que eviten el veto.",
                        "Presentas e inicias campaña pública de presión."
                    ],
                    Correct = 2,
                    Explanation = "Una resolución vetada tiene valor político pero cero efecto en el terreno. Identificar modificaciones mínimas que eviten el veto produce un resultado superior.",
                    Principle = "Estrategia: Minimizar distancia entre impacto político y efecto operativo."
                }
            ]
        },
        new()
        {
            Key = "protocolo",
            Name = "Protocolo y Derecho ONU",
            Description = "Carta de la ONU, Capítulo VII, Convención de Viena y procedimiento parlamentario.",
            Level = "Técnico",
            Questions =
            [
                new()
                {
                    Text = "¿Bajo qué condición puede el Consejo de Seguridad tomar medidas coercitivas contra un Estado miembro sin su consentimiento?",
                    Context = "Artículos 39-42, Carta de las Naciones Unidas.",
                    Options =
                    [
                        "Cuando la AG aprueba una resolución por 2/3.",
                        "Cuando el Secretario General emite declaración de emergencia.",
                        "Cuando el CS determina la existencia de amenaza a la paz conforme al Capítulo VII.",
                        "Cuando cinco o más miembros del CS presentan petición formal."
                    ],
                    Correct = 2,
                    Explanation = "El Capítulo VII (Arts. 39-42) es la base jurídica de las intervenciones coercitivas. Solo cuando el CS califica formalmente bajo el Art. 39 puede autorizar sanciones (Art. 41) o uso de la fuerza (Art. 42).",
                    Principle = "Referencia: Carta ONU, Capítulo VII, Arts. 39-42."
                },
                new()
                {
                    Text = "¿Cuál es la diferencia jurídica fundamental entre una resolución del CS adoptada bajo el Capítulo VI y una bajo el Capítulo VII?",
                    Options =
                    [
                        "Las del Cap. VI son más recientes históricamente.",
                        "Las del Cap. VI tienen mayor jerarquía normativa.",
                        "Las del Cap. VI son recomendatorias y consensuales; las del Cap. VII son obligatorias y coercitivas.",
                        "Solo las del Cap. VII pueden ser vetadas."
                    ],
                    Correct = 2,
                    Explanation = "El Capítulo VI permite al CS recomendar soluciones con consentimiento. El Capítulo VII produce resoluciones vinculantes para todos los Estados bajo el Art. 25.",
                    Principle = "Distinción: Capítulo VI (consensual) vs. Capítulo VII (coercitivo)."
                },
                new()
                {
                    Text = "¿Qué instrumento codificó las normas de inmunidad diplomática y es la base del derecho consular moderno?",
                    Options =
                    [
                        "Convención de Viena sobre Derecho de los Tratados (1969).",
                        "Convención de Viena sobre Relaciones Diplomáticas (1961).",
                        "Convención de Ginebra sobre el Estatuto de los Refugiados (1951).",
                        "Pacto Internacional de Derechos Civiles y Políticos (1966)."
                    ],
                    Correct = 1,
                    Explanation = "La Convención de Viena de 1961 codificó normas sobre privilegios e inmunidades, inviolabilidad de misiones y valija diplomática. Es la referencia obligada para cualquier delegado.",
                    Principle = "Instrumento: Convención de Viena sobre Relaciones Diplomáticas (1961)."
                },
                new()
                {
                    Text = "La abstención de un miembro permanente del CS, ¿equivale a un veto?",
                    Context = "Práctica constitucional del Consejo de Seguridad.",
                    Options =
                    [
                        "Sí: cualquier voto no afirmativo de un P5 bloquea la resolución.",
                        "No: por práctica consuetudinaria, la abstención de un P5 no se equipara al veto; la resolución puede aprobarse con 9 votos favorables si ningún P5 vota en contra.",
                        "Solo si la abstención es comunicada formalmente por escrito.",
                        "La abstención suspende la votación."
                    ],
                    Correct = 1,
                    Explanation = "Por práctica consuetudinaria —no en la Carta— la abstención de un P5 no equivale al veto. Esto permitió la Res. 678 sobre la Guerra del Golfo.",
                    Principle = "Práctica: Doctrina de la abstención en el CS (1950-presente)."
                },
                new()
                {
                    Text = "Un Estado desea presentar una reserva a un tratado multilateral. ¿Cuándo es válida según la Convención de Viena de 1969?",
                    Context = "Artículos 19-23, Convención de Viena sobre Derecho de los Tratados.",
                    Options =
                    [
                        "Solo si todos los demás Estados la aceptan expresamente.",
                        "Si no está prohibida por el tratado, es compatible con su objeto y fin, y no requiere unanimidad.",
                        "Las reservas solo son válidas en tratados bilaterales.",
                        "Requiere aprobación del Secretario General."
                    ],
                    Correct = 1,
                    Explanation = "El Art. 19 establece tres condiciones: no estar expresamente prohibida, ser compatible con el objeto y fin del tratado, y no violar la regla de unanimidad si aplica.",
                    Principle = "Art. 19, Convención de Viena sobre el Derecho de los Tratados (1969)."
                },
                new()
                {
                    Text = "Un delegado levanta un 'Punto de Orden' durante un discurso. ¿Cuándo es apropiado?",
                    Options =
                    [
                        "Puede interrumpir cualquier discurso para hacer una corrección al argumento.",
                        "Solo puede señalar una violación del reglamento y debe ser resuelto por el presidente antes de continuar.",
                        "Suspende automáticamente la sesión 10 minutos.",
                        "Solo los miembros permanentes pueden invocarlo."
                    ],
                    Correct = 1,
                    Explanation = "El punto de orden es una herramienta de procedimiento, no de contenido. Solo es válido para señalar que el reglamento está siendo violado.",
                    Principle = "Distinción: Punto de Orden (procedimiento) vs. Réplica (contenido)."
                },
                new()
                {
                    Text = "¿Cuál es el quórum necesario para que la AG apruebe una resolución sobre 'cuestiones importantes'?",
                    Context = "Artículo 18 de la Carta de la ONU.",
                    Options =
                    [
                        "Mayoría simple (50%+1) de presentes y votantes.",
                        "Dos tercios de los miembros presentes y votantes.",
                        "Unanimidad de todos los Estados miembro.",
                        "Voto afirmativo de los cinco P5 más 4 adicionales."
                    ],
                    Correct = 1,
                    Explanation = "El Art. 18 establece que las 'cuestiones importantes' —paz, seguridad, presupuesto— requieren mayoría de dos tercios de presentes y votantes.",
                    Principle = "Art. 18, Carta ONU: Votaciones en la Asamblea General."
                },
                new()
                {
                    Text = "¿Qué es el principio de 'non-refoulement' y en qué instrumento está consagrado?",
                    Options =
                    [
                        "Es el principio de no intervención; consagrado en la Carta ONU.",
                        "Es la prohibición de devolver a una persona a territorio donde corre riesgo de persecución; consagrado en la Convención de Ginebra de 1951.",
                        "Es la inmunidad diplomática de jefes de Estado; consagrado en la Convención de Viena de 1961.",
                        "Es la soberanía permanente sobre recursos naturales; consagrado en la Resolución 1803."
                    ],
                    Correct = 1,
                    Explanation = "El non-refoulement (Art. 33, Convención de Ginebra de 1951) prohíbe devolver a un refugiado donde su vida esté en peligro. Es considerado norma de jus cogens.",
                    Principle = "Art. 33, Convención de Ginebra de 1951 — Principio de Non-refoulement."
                }
            ]
        }
    ];
}
