using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Image soundOnIcon;
    [SerializeField] private Image soundOffIcon;
    [SerializeField] private AudioSource musicSource;

    private bool muted = false;
    private bool hasBeenActivated = false;

    void Start()
    {
        // Al entrar a la escena, todo debe estar apagado
        muted = false;
        hasBeenActivated = false;

        musicSource.Stop();
        musicSource.mute = false;

        HideIcons(); // Oculta ambos íconos
    }

    public void OnStartButtonPressed()
    {
        if (!hasBeenActivated)
        {
            hasBeenActivated = true;
            musicSource.Play();
            UpdateButtonIcon(); // Muestra el ícono correspondiente
        }
    }

    public void OnMuteButtonPressed()
    {
        if (!hasBeenActivated) return;

        muted = !muted;
        ApplyMute();
        UpdateButtonIcon();
    }

    public void OnNextButtonPressed()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }

        // Reset completo de estado
        muted = false;
        hasBeenActivated = false;
        musicSource.mute = false;

        HideIcons();
    }

    private void ApplyMute()
    {
        musicSource.mute = muted;
    }

    private void UpdateButtonIcon()
    {
        if (soundOnIcon != null) soundOnIcon.enabled = !muted;
        if (soundOffIcon != null) soundOffIcon.enabled = muted;
    }

    private void HideIcons()
    {
        if (soundOnIcon != null) soundOnIcon.enabled = false;
        if (soundOffIcon != null) soundOffIcon.enabled = false;
    }
}
