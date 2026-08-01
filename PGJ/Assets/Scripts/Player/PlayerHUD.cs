using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private GameObject[] heartIcons;
    [SerializeField] private Slider staminaBar;
    [SerializeField] private Slider hungerBar;

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
}
