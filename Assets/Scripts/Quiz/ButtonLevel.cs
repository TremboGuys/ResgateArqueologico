using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonLevel : MonoBehaviour {
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI textButton; 
    void Start() {
        Debug.Log(textButton.text);
        button.onClick.AddListener(() =>
        {
            ManagerLevel.Instance.ChooseLevel();
        });
    }
}
