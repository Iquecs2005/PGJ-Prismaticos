using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlapInteraction : BaseInteractable
{
    [Header("Flap Interaction variables")]
    [SerializeField] private FlapHunger hunger;

    protected override void InteractionComplete()
    {
        hunger.Feed();
        base.InteractionComplete();
    }
}
