using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_ErrorPopup_Text : MonoBehaviour
{
    public Sprite icon_greenTick;
    public Sprite icon_redCrossMark;

    public Image image;
    public TextMeshProUGUI label;

    private static Color green = new Color(0.49f, 0.96f, 0.42f);
    private static Color red = new Color(0.86f, 0.29f, 0.29f);

    public void SetStatus(bool positive)
    {
        if(positive)
        {
            image.sprite = icon_greenTick;
            image.color = green;
            label.color = green;
        }
        else
        {
            image.sprite = icon_redCrossMark;
            image.color = red;
            label.color = red;
        }
    }
}
