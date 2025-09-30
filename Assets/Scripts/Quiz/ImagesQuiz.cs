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
        Debug.Log(sprite);
        Debug.Log(imagePanel);
        Debug.Log(imageScore);
        imagePanel.sprite = sprite;
        imageScore.sprite = sprite;
    }
}