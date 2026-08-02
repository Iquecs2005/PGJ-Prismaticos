using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerController : MonoBehaviour
{
    [field: Header("Trigger Events")]
    [field: SerializeField] public UnityEvent<Collider2D> onEnterEvent { get; private set; }
    [field: SerializeField] public UnityEvent<Collider2D> onExitEvent { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onEnterEvent.Invoke(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onExitEvent.Invoke(collision);
    }
}
