using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class HoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private TMP_Text textComponent;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color hoverColor = Color.yellow;
    [SerializeField] private float hoverScale = 1.05f;

    private Vector3 originalScale;

    void Start() {
        textComponent = GetComponent<TMP_Text>();

        originalScale = textComponent.transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textComponent.color = hoverColor;
        textComponent.transform.localScale = originalScale * hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textComponent.color = normalColor;
        textComponent.transform.localScale = originalScale;
    }
}