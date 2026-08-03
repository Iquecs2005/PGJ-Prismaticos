using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHUD : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] private Image[] heartIcons;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    [Header("Outros")]
    [SerializeField] private Slider staminaBar;
    [SerializeField] private Slider hungerBar;
    [SerializeField] private TMP_Text clockText;

    private void Start()
    {
        GameManager.timeManager.onTickEvent.AddListener(UpdateClock);
    }

    public void OnHealthUpdate(int health, int maxHealth)
    {
        int heartCount = heartIcons.Length;
        int filled = maxHealth > 0
            ? Mathf.RoundToInt((float)health / maxHealth * heartCount)
            : 0;
        filled = Mathf.Clamp(filled, 0, heartCount);

        Debug.Log($"[PlayerHUD] health={health}/{maxHealth} | heartCount={heartCount} | filled={filled} | fullHeart={(fullHeart ? fullHeart.name : "NULL")} | emptyHeart={(emptyHeart ? emptyHeart.name : "NULL")}");

        for (int i = 0; i < heartCount; i++)
        {
            if (heartIcons[i] == null)
            {
                Debug.LogWarning($"[PlayerHUD] heartIcons[{i}] esta NULL");
                continue;
            }

            heartIcons[i].sprite = i < filled ? fullHeart : emptyHeart;
            Debug.Log($"[PlayerHUD] heart {i} ({heartIcons[i].name}) -> {(heartIcons[i].sprite ? heartIcons[i].sprite.name : "NULL")}");
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