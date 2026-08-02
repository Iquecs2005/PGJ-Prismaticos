using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlapInteraction : BaseInteractable
{
    [Header("Flap Interaction variables")]
    [SerializeField] private FlapHunger hunger;

    protected override void InteractionComplete()
    {
        print("Flap Feeded");
        hunger.Feed(120);
        print("This should be integrated with the inventory");

        base.InteractionComplete();
    }
}
