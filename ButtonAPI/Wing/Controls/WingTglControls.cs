using System;
using UnityEngine;
using UnityEngine.UI;
using WorldAPI.ButtonAPI.Controls;

namespace WorldAPI.ButtonAPI.WIng.Controls;

public class WingTglControls : Root
{
    internal Action<bool> Listener { get; set; }
    public Image OnImage { get; internal set; }
    public Image OffImage { get; internal set; }

    public (Sprite, Sprite) SetImages(Sprite onSprite = null, Sprite offSprite = null)
    {
        OffImage.sprite = offSprite;
        OnImage.sprite = onSprite;
        return (onSprite, offSprite);
    }

    public (Sprite, Sprite) SetImages(bool checkForNull, Sprite onSprite = null, Sprite offSprite = null)
    {
        if (checkForNull)
        {
            if (offSprite == null) offSprite = APIBase.OffSprite;
            if (onSprite == null) onSprite = APIBase.OnSprite;
        }
        if (offSprite != null)
            OffImage.sprite = offSprite;
        if (onSprite != null)
            OnImage.sprite = onSprite;
        return (onSprite, offSprite);
    }

    public string SetToolTip(string tip)
    {

        return tip;
    }
}