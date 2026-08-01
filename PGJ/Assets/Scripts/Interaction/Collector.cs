using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectible collectible = collision.gameObject.GetComponent<ICollectible>();
        if(collectible != null)
        {
            collectible.Collect();
        }
    }
}
