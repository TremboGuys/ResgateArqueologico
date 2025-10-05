using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class ButtonAnswer : MonoBehaviour {
    [SerializeField] private Button button;
    [SerializeField] private Image imageButton;
    private Dictionary<String, String> textBasedImage = new Dictionary<string, string> { { "alternativeA_0", "A" }, { "alternativeB_0", "B" }, { "alternativeC_0", "C" }, { "alternativeD_0", "D" } };

    void Start() {
        button.onClick.AddListener(() =>
        {
            ManagerQuiz.Instance.UserResponse(textBasedImage[imageButton.sprite.name]);
        });
    }
}