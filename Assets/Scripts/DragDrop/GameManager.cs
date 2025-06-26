using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<DropZone> timelineSlots;

    public void CheckOrder()
    {
        bool allCorrect = true;

        foreach (DropZone slot in timelineSlots)
        {
            if (slot.transform.childCount == 0)
            {
                allCorrect = false;
                continue;
            }

            var card = slot.GetComponentInChildren<CardData>();
            if (card == null || card.correctDropId != slot.dropId)
            {
                allCorrect = false;
            }
        }

        if (allCorrect)
        {
            Debug.Log("✅ Você completou a linha do tempo corretamente!");
        }
        else
        {
            Debug.Log("❌ Ainda há erros. Tente novamente.");
        }
    }
}