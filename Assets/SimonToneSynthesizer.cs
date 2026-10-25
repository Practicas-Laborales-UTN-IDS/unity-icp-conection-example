
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SimonToneSynthesizer : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField, Range(0f, 1f)]
    private float volume = 0.5f;

    [SerializeField, Range(0.1f, 1f)]
    private float duration = 0.30f;

    [SerializeField, Range(0f, 0.1f)]
    private float attack = 0.008f;

    private AudioSource audioSource;

    private AudioClip blueClip;
    private AudioClip yellowClip;
    private AudioClip redClip;
    private AudioClip greenClip;

    private const int SampleRate = 44100;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0f;

        blueClip = CreateTone(659.255f, "Simon_Blue_E5");
        yellowClip = CreateTone(554.365f, "Simon_Yellow_CSharp5");
        redClip = CreateTone(440.000f, "Simon_Red_A4");
        greenClip = CreateTone(329.628f, "Simon_Green_E4");
    }

    private AudioClip CreateTone(float frequency, string clipName)
    {
        int sampleCount = Mathf.CeilToInt(SampleRate * duration);
        float[] samples = new float[sampleCount];

        // Mezcla de armónicos para un timbre electrónico.
        // La fundamental sigue siendo la nota indicada.
        float[] harmonicAmplitudes = { 1f, 0.35f, 0.18f, 0.08f };
        int[] harmonics = { 1, 2, 3, 5 };

        float amplitudeSum = 0f;

        foreach (float amplitude in harmonicAmplitudes)
            amplitudeSum += amplitude;

        for (int i = 0; i < sampleCount; i++)
        {
            float time = (float)i / SampleRate;
            float t = time / duration;

            // Ataque rápido y caída gradual del volumen.
            float attackTime = Mathf.Max(attack, 0.001f);
            float attackEnvelope = Mathf.Clamp01(time / attackTime);
            float decayEnvelope = Mathf.Exp(-3.5f * t);

            float envelope = attackEnvelope * decayEnvelope;
            float wave = 0f;

            for (int h = 0; h < harmonics.Length; h++)
            {
                wave += harmonicAmplitudes[h] *
                    Mathf.Sin(
                        2f * Mathf.PI * frequency *
                        harmonics[h] * time
                    );
            }

            wave /= amplitudeSum;

            samples[i] = wave * envelope * volume;
        }

        AudioClip clip = AudioClip.Create(
            clipName,
            sampleCount,
            1,
            SampleRate,
            false
        );

        clip.SetData(samples, 0);
        return clip;
    }

    public void PlayBlue() => Play(blueClip);
    public void PlayYellow() => Play(yellowClip);
    public void PlayRed() => Play(redClip);
    public void PlayGreen() => Play(greenClip);

    private void Play(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }
}
