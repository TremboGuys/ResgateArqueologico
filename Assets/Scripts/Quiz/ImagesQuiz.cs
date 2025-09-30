using UnityEngine;
using UnityEngine.UI;

public class ImagesQuiz : MonoBehaviour
{
    [SerializeField] private Image imagePanel;
    [SerializeField] private Image imageScore;
    readonly ImageUtils imageUtils = new();

    public void ChangeImages(string spriteName)
    {
        Sprite sprite = imageUtils.ChangeImage(spriteName);
        imagePanel.sprite = sprite;
        imageScore.sprite = sprite;
    }
}