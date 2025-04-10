using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance { get; private set; }

    [Header("Menu Sounds")]
    [SerializeField] private AudioClip menuHoverSound;

    private AudioSource audioSource;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        } else {
            Destroy(gameObject);
        }
    }

    public void PlayMenuHover() {
        if (menuHoverSound != null) {
            audioSource.PlayOneShot(menuHoverSound);
        }
    }
}