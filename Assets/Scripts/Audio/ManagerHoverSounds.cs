using UnityEngine;

public class ManagerHoverSounds : MonoBehaviour {
    public static ManagerHoverSounds Instance { get; private set; }

    [Header("Menu Sounds")]
    [SerializeField] private AudioClip menuHoverSound;

    private AudioSource source;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            source = GetComponent<AudioSource>();
            Debug.Log("Este é o source: " + source);
            OverallAudio.Instance.RegisterAudioSource(source);
        } else {
            Destroy(gameObject);
        }
    }

    public void PlayMenuHover() {
        if (menuHoverSound != null) {
            source.PlayOneShot(menuHoverSound);
        }
    }
}