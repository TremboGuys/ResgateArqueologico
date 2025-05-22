using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuAudioLink : MonoBehaviour {
    [SerializeField] private Slider volumeSlider;
    private bool hasSynced = false;

    void Update() {
        if (!hasSynced && OverallAudio.Instance != null && volumeSlider != null) {
            OverallAudio.Instance.LinkSlider(volumeSlider);
            hasSynced = true;
        }
    }
}