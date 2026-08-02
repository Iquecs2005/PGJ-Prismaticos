using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemDisplay : MonoBehaviour
{
    public Image image;
    public TMP_Text countText;

    public void SetItem(ItemData item, int count)
    {
        gameObject.SetActive(true);

        if (image != null)
        {
            image.sprite = item != null ? item.icon : null;
            image.enabled = item != null;
        }

        if (countText != null)
            countText.text = count + "x";
    }

    public void Clear()
    {
        gameObject.SetActive(false);
    }
}