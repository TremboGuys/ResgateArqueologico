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
        if (spriteName == "ZoolitoTubarao")
        {
            RectTransform imagePanelRectTransform = imagePanel.GetComponent<RectTransform>();
            imagePanelRectTransform.sizeDelta = new Vector2(400f, 270f);

            RectTransform imageScoreRectTransform = imageScore.GetComponent<RectTransform>();
            imageScoreRectTransform.sizeDelta = new Vector2(600f, 500f);
        }
        else if (spriteName == "DamaWarka")
        {
            RectTransform imageRectTransform = imagePanel.GetComponent<RectTransform>();
            imageRectTransform.sizeDelta = new Vector2(340f, 460f);
        }
        imagePanel.sprite = sprite;
        imageScore.sprite = sprite;
    }
}