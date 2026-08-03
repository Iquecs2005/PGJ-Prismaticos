using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : HealthController
{
    protected override void Die()
    {
        OnDeath?.Invoke();
        gameObject.SetActive(false);
        GameManager.gameOverManager?.OnGameOver(GameOverType.JackWasEaten);
    }
}
