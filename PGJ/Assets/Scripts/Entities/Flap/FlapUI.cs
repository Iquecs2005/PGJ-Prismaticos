using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlapUI : MonoBehaviour
{
    [SerializeField] private FlapHunger flapHunger;
    [SerializeField] private Slider hungrySlider;
    [SerializeField] private Image image;

    [SerializeField] private Sprite happyFlapSprite;
    [SerializeField] private Sprite fineFlapSprite;
    [SerializeField] private Sprite starvingFlapSprite;

    [SerializeField] private float fineFlapRatio;
    [SerializeField] private float starvingFlapRatio;

    private void FixedUpdate()
    {
        if (flapHunger == null)
            return;

        float ratio = 1 - flapHunger.GetHungerRatio();

        hungrySlider.value = ratio;

        if (ratio < starvingFlapRatio) 
        {
            image.sprite = starvingFlapSprite;
        }
        else if (ratio < fineFlapRatio)
        {
            image.sprite = fineFlapSprite;
        }
        else 
        {
            image.sprite = happyFlapSprite;
        }
    }
}
