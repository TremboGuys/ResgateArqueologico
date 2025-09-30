using UnityEngine;
using UnityEngine.UI;

public class IconSelected : MonoBehaviour
{
    [SerializeField] private Image imageUI;

    void Awake()
    {
        imageUI = GetComponent<Image>();
    }

    public void ChangeImage(string spriteName)
    {
        ImageUtils imageUtils = new();
        Sprite sprite = imageUtils.ChangeImage(spriteName);
    }
}