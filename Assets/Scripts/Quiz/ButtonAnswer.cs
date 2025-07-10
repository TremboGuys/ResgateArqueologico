using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonAnswer : MonoBehaviour {
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI textButton;

    void Start() {
        button.onClick.AddListener(() =>
        {
            ManagerQuiz.Instance.UserResponse(textButton.text);
        });
    }
}