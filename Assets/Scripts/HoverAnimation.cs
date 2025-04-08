using UnityEngine;
using UnityEngine.UIElements;

public class HoverAnimation : MonoBehaviour
{
    public UIDocument doc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        var root = doc.rootVisualElement;

        var textoA = root.Q<Label>("PlayButton");
        var textoB = root.Q<Label>("PlayHoverButton");

        textoB.style.display = DisplayStyle.None;

        textoA.RegisterCallback<MouseEnterEvent>(evt => {
            textoA.style.display = DisplayStyle.None;
            textoB.style.display = DisplayStyle.Flex;
        });

        textoB.RegisterCallback<MouseLeaveEvent>(evt => {
            textoB.style.display = DisplayStyle.None;
            textoA.style.display = DisplayStyle.Flex;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
