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
        Sprite sprite = Resources.Load<Sprite>("Images/" + spriteName);

        if (sprite != null)
        {
            imageUI.sprite = sprite;
            ManagerLogin.Instance.photoPath = spriteName;
        }
        else
        {
            Debug.LogWarning("Sprite not found: " + spriteName);
        }
    }
}