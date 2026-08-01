using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public bool isInteractable = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isInteractable && other.CompareTag("Player"))
        {
            Interact();
        }
    }

    void Interact()
    {
        Debug.Log("Interagindo com " + gameObject.name);
    }
}