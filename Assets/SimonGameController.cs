using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using InsideDarkStudio.Clients.IDS.Models;
using UnityEngine.UI;
using Metrics = System.Collections.Generic.List<InsideDarkStudio.Clients.IDS.Models.MetricItem>;


public class SimonGameController : MonoBehaviour
{
    private enum GameState
    {
        Idle,
        Starting,
        ShowingSequence,
        WaitingForInput,
        GameOver
    }

    [Header("ICP Backend")]
    [SerializeField] private ICPTestRunner icpService;

    [Header("UI Text References")]
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private TMP_Text gamesPlayedText;

    [Header("Audio")]
    [SerializeField] private SimonToneSynthesizer toneSynthesizer;

    [Header("Game Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button[] colorButtons = new Button[4];

    [Header("UI")]
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text bestText;
    [SerializeField] private TMP_Text statusText;

    [Header("Simon Colors")]
    [Tooltip("Images de los botones en orden: rojo, azul, amarillo, verde.")]
    [SerializeField] private Image[] colorImages = new Image[4];

    [Tooltip("Colores normales de los botones.")]
    [SerializeField] private Color[] normalColors = new Color[4];

    [Tooltip("Intensidad del brillo durante la reproducción.")]
    [SerializeField, Range(1f, 3f)]
    private float highlightMultiplier = 1.6f;

    [Header("Timing")]
    [SerializeField, Min(0.05f)]
    private float flashDuration = 0.3f;

    [SerializeField, Min(0.05f)]
    private float flashPause = 0.15f;

    [SerializeField, Min(0f)]
    private float sequencePause = 0.7f;

    [SerializeField, Min(0f)]
    private float inputFeedbackDuration = 0.12f;

    [SerializeField] 
    private float repeatedColorPause = 0.35f;

    private readonly List<int> sequence = new List<int>();

    private SimonRandom random;
    private GameState state = GameState.Idle;

    private int inputIndex;
    private int bestLevel;
    private bool listenersRegistered;

    private void Awake()
    {
        if (startButton != null)
            startButton.onClick.AddListener(OnStartClicked);

        if (colorButtons != null)
        {
            for (int i = 0; i < colorButtons.Length; i++)
            {
                int colorIndex = i;

                if (colorButtons[i] != null)
                {
                    colorButtons[i].onClick.AddListener(
                        () => OnColorClicked(colorIndex));
                }
            }
        }

        listenersRegistered = true;

        SetButtonsInteractable(false);
        RestoreAllColors();
        UpdateUI();
        SetStatus("Pulsa INICIAR para jugar.");
    }

    private void Start()
    {
        // Evita depender del valor inicial del Inspector.
        state = GameState.Idle;
        SetButtonsInteractable(false);
        UpdateUI();
        _ = LoadPlayerScore();
    }

    private void OnDestroy()
    {
        if (!listenersRegistered)
            return;

        if (startButton != null)
            startButton.onClick.RemoveListener(OnStartClicked);

        if (colorButtons != null)
        {
            for (int i = 0; i < colorButtons.Length; i++)
            {
                int colorIndex = i;

                if (colorButtons[i] != null)
                {
                    colorButtons[i].onClick.RemoveListener(
                        () => OnColorClicked(colorIndex));
                }
            }
        }
    }

    private async void OnStartClicked()
    {
        await StartGame();
    }

    public async Task LoadPlayerScore()
    {
        if (icpService == null)
        {
            Debug.LogError("[SIMON] No se asignó la referencia de ICPTestRunner.");
            return;
        }

        try
        {
            // Obtenemos la lista de métricas directamente desde el servicio
            Metrics metrics = await icpService.GetPlayerScore();

            if (metrics == null || this == null) return;

            // Buscamos las métricas dinámicamente por su nombre usando LINQ
            var pointsMetric = metrics.FirstOrDefault(m => m.Name == "00");
            var levelMetric  = metrics.FirstOrDefault(m => m.Name == "01");
            var gamesPlayed  = metrics.FirstOrDefault(m => m.Name == "__sys_total_matches__");

            // Pintamos los puntos (Métrica "00")
            if (pointsText != null)
            {
                long points = pointsMetric != null ? ExtractMetricValue(pointsMetric.Value) : 0;
                pointsText.text = $"Puntos: {points}";
            }

            // Pintamos el nivel alcanzado (Métrica "01") - Preparado para cuando agregues su TextLabel
            if (levelText != null)
            {
                long level = levelMetric != null ? ExtractMetricValue(levelMetric.Value) : 0;
                levelText.text = $"Nivel Max: {level}";
            }

            // Pintamos el total de partidas (__sys_total_matches__)
            if (gamesPlayedText != null)
            {
                long games = gamesPlayed != null ? ExtractMetricValue(gamesPlayed.Value) : 0;
                gamesPlayedText.text = $"Partidas: {games}";
            }

            Debug.Log("[SIMON] ¡Textos de interfaz actualizados con éxito desde la blockchain!");
        }
        catch (Exception ex)
        {
            Debug.LogError($"[SIMON] Error al actualizar la interfaz: {ex.Message}");
        }
    }

    // Helper robusto para extraer el número sin importar si vino como Sum, Max, Min o Replace
    private long ExtractMetricValue(MetricKind kind)
    {
        if (kind == null) return 0;

        switch (kind.Tag)
        {
            case MetricKindTag.Sum:
                return (long)kind.AsSum();
            case MetricKindTag.Max:
                return (long)kind.AsMax();
            case MetricKindTag.Min:
                return (long)kind.AsMin();
            case MetricKindTag.Replace:
                return (long)kind.AsReplace();
            default:
                return 0;
        }
    }

    public async Task StartGame()
    {
        if (state == GameState.Starting ||
            state == GameState.ShowingSequence ||
            state == GameState.WaitingForInput)
        {
            return;
        }

        if (!ValidateReferences())
            return;

        state = GameState.Starting;
        sequence.Clear();
        random = null;
        inputIndex = 0;

        SetButtonsInteractable(false);
        startButton.interactable = false;

        RestoreAllColors();
        UpdateUI();
        SetStatus("Conectando con ICP...");

        try
        {
            if (icpService == null)
                throw new InvalidOperationException(
                    "No se asignó ICPTestRunner.");

            Debug.Log("[SIMON] Solicitando semilla al canister...");

            ulong seed = await icpService.GetSeedFromBackend();

            // Evita continuar si el componente fue destruido
            // mientras esperábamos la respuesta de ICP.
            if (this == null)
                return;

            random = new SimonRandom(seed);

            Debug.Log($"[SIMON] Semilla recibida: {seed}");

            SetStatus("¡Presta atención!");
            Debug.Log("[SIMON] Partida iniciada.");

            StartNextRound();
        }
        catch (Exception ex)
        {
            if (this == null)
                return;

            Debug.LogError(
                $"[SIMON] Error al iniciar la partida: {ex}");

            state = GameState.Idle;
            SetButtonsInteractable(false);
            startButton.interactable = true;
            SetStatus("No se pudo iniciar. Intenta nuevamente.");
        }
    }

    private bool ValidateReferences()
    {
        if (startButton == null ||
            levelText == null ||
            statusText == null ||
            colorButtons == null ||
            colorButtons.Length != 4 ||
            colorImages == null ||
            colorImages.Length != 4 ||
            normalColors == null ||
            normalColors.Length != 4)
        {
            Debug.LogError(
                "[SIMON] Faltan referencias. " +
                "Revisa el Inspector y asigna los cuatro colores.");
            return false;
        }

        for (int i = 0; i < 4; i++)
        {
            if (colorButtons[i] == null ||
                colorImages[i] == null)
            {
                Debug.LogError(
                    $"[SIMON] Falta configurar el botón o la imagen {i}.");
                return false;
            }
        }

        return true;
    }

    private void StartNextRound()
    {
        if (state == GameState.GameOver || random == null)
            return;

        // Cada ronda agrega un color a la secuencia anterior.
        sequence.Add(random.NextColor());

        inputIndex = 0;

        UpdateUI();

        Debug.Log(
            $"[SIMON] Comenzando nivel {sequence.Count}. " +
            $"Longitud de secuencia: {sequence.Count}");

        StartCoroutine(ShowSequence());
    }

    private IEnumerator ShowSequence()
    {
        state = GameState.ShowingSequence;
        SetButtonsInteractable(false);
        RestoreAllColors();

        SetStatus($"Nivel {sequence.Count}: observa la secuencia.");

        yield return new WaitForSeconds(sequencePause);

        for (int i = 0; i < sequence.Count; i++)
        {
            int colorIndex = sequence[i];

            Debug.Log(
                $"[SIMON] Reproduciendo color {i + 1}: " +
                GetColorName(colorIndex));

            SetColorHighlighted(colorIndex, true);

            SetColorHighlighted(colorIndex, true);

            yield return new WaitForSeconds(flashDuration);

            SetColorHighlighted(colorIndex, false);

            yield return new WaitForSeconds(flashPause);
        
        }

        if (state != GameState.ShowingSequence)
            yield break;

        inputIndex = 0;
        state = GameState.WaitingForInput;

        SetButtonsInteractable(true);
        SetStatus("¡Tu turno! Repite la secuencia.");
    }

    private void OnColorClicked(int colorIndex)
    {
        if (state != GameState.WaitingForInput)
            return;

        if (colorIndex < 0 || colorIndex > 3)
            return;

        StartCoroutine(ShowInputFeedback(colorIndex));

        int expectedColor = sequence[inputIndex];

        if (colorIndex != expectedColor)
        {
            Debug.LogWarning(
                $"[SIMON] Error en la posición {inputIndex + 1}. " +
                $"Esperado: {GetColorName(expectedColor)}, " +
                $"recibido: {GetColorName(colorIndex)}");

            FinishGame(false);
            return;
        }

        inputIndex++;

        // La secuencia completa fue reproducida correctamente.
        if (inputIndex >= sequence.Count)
        {
            bestLevel = Mathf.Max(bestLevel, sequence.Count);

            UpdateUI();

            Debug.Log(
                $"[SIMON] ¡Nivel {sequence.Count} superado!");

            SetStatus("¡Correcto! Preparando el siguiente nivel.");

            SetButtonsInteractable(false);
            StartCoroutine(NextRoundAfterDelay());
        }
    }

    private IEnumerator ShowInputFeedback(int colorIndex)
    {
        SetColorHighlighted(colorIndex, true);

        yield return new WaitForSeconds(inputFeedbackDuration);

        SetColorHighlighted(colorIndex, false);
    }

    private IEnumerator NextRoundAfterDelay()
    {
        state = GameState.ShowingSequence;

        yield return new WaitForSeconds(sequencePause);

        StartNextRound();
    }

    private void FinishGame(bool completed)
    {
        if (state == GameState.GameOver)
            return;

        StopAllCoroutines();

        state = GameState.GameOver;

        SetButtonsInteractable(false);
        RestoreAllColors();

        UpdateUI();

        int reachedLevel = completed ? sequence.Count : sequence.Count - 1;
        reachedLevel = Mathf.Max(0, reachedLevel);

        SetStatus(
            completed
                ? $"¡Partida completada! Nivel {reachedLevel}."
                : $"¡Fallaste! Nivel alcanzado: {reachedLevel}.");

        Debug.Log("[SIMON] Partida finalizada.");
        Debug.Log($"[SIMON] Nivel alcanzado: {reachedLevel}");
        Debug.Log($"[SIMON] Mejor nivel local: {bestLevel}");

        // SimulateBackendReport(reachedLevel);
        ReportGameToBackend(reachedLevel, completed);
        _ = LoadPlayerScore();

        startButton.interactable = true;
    }

    // private void SimulateBackendReport(int reachedLevel)
    // {
    //     Debug.Log("[ICP MOCK] Preparando reporte de progreso...");
    //     Debug.Log($"[ICP MOCK] Nivel alcanzado: {reachedLevel}");
    //     Debug.Log($"[ICP MOCK] Longitud de secuencia: {sequence.Count}");
    //     Debug.Log($"[ICP MOCK] Resultado: {state}");
    //     Debug.Log(
    //         "[ICP MOCK] Reporte simulado. " +
    //         "No se envió ninguna transacción al canister.");
    // }

    private bool isReportingResult;

    private async System.Threading.Tasks.Task ReportGameToBackend(
        int reachedLevel,
        bool completed)
    {
        if (icpService == null)
        {
            Debug.LogError("[SIMON] No se asignó ICPTestRunner.");
            return;
        }

        Debug.Log("[SIMON] Reportando resultado al backend...");

        try
        {
            // Pasamos el nivel alcanzado al servicio de ICP
            await icpService.ReportGameResult(reachedLevel);

            Debug.Log("[SIMON] Reporte enviado correctamente.");
            
            // Opcional: Actualizar los textos de la UI inmediatamente después de reportar
            await icpService.GetPlayerScore();
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[SIMON] Error al reportar resultado: {ex}");
        }
    }

    private void SetButtonsInteractable(bool interactable)
    {
        if (colorButtons == null)
            return;

        foreach (Button button in colorButtons)
        {
            if (button != null)
                button.interactable = interactable;
        }
    }
    
    
    private void SetColorHighlighted(int index, bool highlighted)
    {
        if (colorImages == null ||
            normalColors == null ||
            index < 0 ||
            index >= colorImages.Length ||
            index >= normalColors.Length ||
            colorImages[index] == null)
        {
            return;
        }

        Color baseColor = normalColors[index];
        Color result;

        if (!highlighted)
        {
            result = baseColor;
            result.a = 1f;
        }
        else
        {
            switch (index)
            {
                case 0: // Rojo — La4
                    result = new Color(1f, 0.45f, 0.45f, 1f);

                    if (toneSynthesizer != null)
                        toneSynthesizer.PlayRed();
                    break;

                case 1: // Azul — Mi5
                    result = new Color(0.45f, 0.68f, 1f, 1f);

                    if (toneSynthesizer != null)
                        toneSynthesizer.PlayBlue();
                    break;

                case 2: // Amarillo — Do#5
                    result = new Color(1f, 1f, 0.65f, 1f);

                    if (toneSynthesizer != null)
                        toneSynthesizer.PlayYellow();
                    break;

                case 3: // Verde — Mi4
                    result = new Color(0.35f, 1f, 0.60f, 1f);

                    if (toneSynthesizer != null)
                        toneSynthesizer.PlayGreen();
                    break;

                default:
                    result = Color.white;
                    break;
            }
        }

        colorImages[index].color = result;
    }

    
    private void RestoreAllColors()
    {
        if (colorImages == null || normalColors == null)
        {
            Debug.LogError("[SIMON] Faltan colorImages o normalColors.");
            return;
        }

        int count = Mathf.Min(
            colorImages.Length,
            normalColors.Length
        );

        for (int i = 0; i < count; i++)
        {
            if (colorImages[i] == null)
            {
                Debug.LogError($"[SIMON] Falta Image en posición {i}.");
                continue;
            }

            Color c = normalColors[i];
            c.a = 1f;

            colorImages[i].color = c;
        }
    }


    private void UpdateUI()
    {
        if (levelText != null)
            levelText.text = $"NIVEL: {sequence.Count}";

        if (bestText != null)
            bestText.text = $"RÉCORD: {bestLevel}";
    }

    private void SetStatus(string message)
    {
        if (statusText != null)
            statusText.text = message;
    }

    private string GetColorName(int colorIndex)
    {
        switch (colorIndex)
        {
            case 0: return "ROJO";
            case 1: return "AZUL";
            case 2: return "AMARILLO";
            case 3: return "VERDE";
            default: return "DESCONOCIDO";
        }
    }
}
