using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFishAI : MonoBehaviour
{
    [SerializeField] private EntityController controller;

    [SerializeField] private GameObject player;

    private void FixedUpdate()
    {
        Vector2 dir = transform.position - player.transform.position;
        controller.movement.SetMoveInput(dir);
    }
}
