using System;
using UnityEngine;

public class ImageUtils
{
    public Sprite ChangeImage(string spriteName)
    {
        Sprite sprite = Resources.Load<Sprite>("Images/" + spriteName);

        if (sprite == null)
        {
            throw new NullReferenceException("Sprite not found: " + spriteName);
        }
        return sprite;
    }
}