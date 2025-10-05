using UnityEngine;

public class ManagerHoverSounds : MonoBehaviour {
    public static ManagerHoverSounds Instance { get; private set; }

    [Header("Menu Sounds")]
    [SerializeField] private AudioClip menuHoverSound;

    private AudioSource source;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            PersistentManager.Register("ManagerHoverSounds", gameObject);
            source = GetComponent<AudioSource>();
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