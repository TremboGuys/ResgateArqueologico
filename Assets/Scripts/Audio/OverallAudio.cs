using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class OverallAudio : MonoBehaviour
{
    public static OverallAudio Instance { get; private set; }
    private float masterVolume = 1f;
    [SerializeField] private Slider volumeSlider;
    private List<AudioSource> allAudioSources = new List<AudioSource>();

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        HasPlayerPref();
        SetMasterVolume(PlayerPrefs.GetFloat("volumeMaster", 1f));
    }

    void HasPlayerPref() {
        if (!PlayerPrefs.HasKey("volumeMaster")) {
            PlayerPrefs.SetFloat("volumeMaster", 1f);
        }
        if (volumeSlider != null) {
            volumeSlider.value = PlayerPrefs.GetFloat("volumeMaster");
        }
    }

    public void LinkSlider(Slider slider) {
        volumeSlider = slider;

        if (volumeSlider != null) {
            volumeSlider.onValueChanged.RemoveAllListeners();
            volumeSlider.onValueChanged.AddListener(SetMasterVolume);

            volumeSlider.value = masterVolume;
            SetMasterVolume(masterVolume);
        }
    }

    public void RegisterAudioSource(AudioSource source) {
        if (!allAudioSources.Contains(source)) {
            allAudioSources.Add(source);
            source.volume = masterVolume;
        }
    }

    public void UnregisterAudioSource(AudioSource source) {
        if (allAudioSources.Contains(source)) {
            allAudioSources.Remove(source);
        }
    }
    
    public void SetMasterVolume(float volume) {
        masterVolume = volume;
        if (volume != PlayerPrefs.GetFloat("volumeMaster")) {
            PlayerPrefs.SetFloat("volumeMaster", volume);
            PlayerPrefs.Save();
        }

        foreach (AudioSource source in allAudioSources) {
            if (source != null) {
                source.volume = volume;
            }
        }
    }

    public float GetMasterVolume() {
        return masterVolume;
    }
}