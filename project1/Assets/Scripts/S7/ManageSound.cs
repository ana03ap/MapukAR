using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Image soundOnIcon;       // Ícono de sonido activado
    [SerializeField] private Image soundOffIcon;      // Ícono de sonido apagado
    [SerializeField] private AudioSource musicSource; // Fuente de audio para la música

    private bool muted = false;

    void Start()
    {
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
        }

        Load();            
        ApplyMute();       
        UpdateButtonIcon();
    }

    // Botón grande para comenzar música
    public void OnStartButtonPressed()
    {
        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    // Botón para mutear/desmutear
    public void OnMuteButtonPressed()
    {
        muted = !muted;
        ApplyMute();
        Save();
        UpdateButtonIcon();
    }

    // ✅ Botón Next: detener la música
    public void OnNextButtonPressed()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();  // Detiene la música por completo
        }
    }

    private void ApplyMute()
    {
        if (musicSource != null)
        {
            musicSource.mute = muted;
        }
    }

    private void UpdateButtonIcon()
    {
        if (soundOnIcon != null) soundOnIcon.enabled = !muted;
        if (soundOffIcon != null) soundOffIcon.enabled = muted;
    }

    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
}
