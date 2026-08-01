using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private GameObject[] heartIcons;
    [SerializeField] private Slider staminaBar;
    [SerializeField] private Slider hungerBar;
    [SerializeField] private TMP_Text clockText;

    private void Start()
    {
        GameManager.timeManager.onTickEvent.AddListener(UpdateClock);        
    }

    public void OnHealthUpdate(int health) 
    {
        for (int i = 0; i < heartIcons.Length; i++) 
        {
            heartIcons[i].SetActive(i < health);
        }
    }

    public void OnStaminaUpdate(float stamina, float maxStamina) 
    {
        staminaBar.maxValue = maxStamina;
        staminaBar.value = stamina;
    }

    public void OnHungerUpdate(float hunger, float maxHunger)
    {
        hungerBar.maxValue = maxHunger;
        hungerBar.value = hunger;
    }

    public void UpdateClock() 
    {
        string timeString = GameManager.timeManager.GetTime();
        clockText.text = timeString;
    }
}
