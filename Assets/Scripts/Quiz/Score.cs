using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text scoreText;

    public void ShowScore(string titleText, int numHits)
    {
        this.titleText.text = "Quiz " + titleText + " - Concluído";
        this.scoreText.text = "Acertou " + numHits + "/10" + " (" + numHits * 10 + "%)"; ;
    }
}