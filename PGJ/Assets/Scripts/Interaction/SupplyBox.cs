using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SupplyBox : BaseInteractable
{
    [Header("Supply Box Variables")]
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int amount = 1;
    [SerializeField] private float spawnSpread = 0.4f;

    protected override void InteractionComplete()
    {
        SpawnPickup();

        base.InteractionComplete();
    }

    private void SpawnPickup() 
    {
        if (itemPrefab == null)
            return;

        Vector3 origin = spawnPoint != null ? spawnPoint.position : transform.position;

        for (int i = 0; i < amount; i++)
        {
            Vector2 offset = Random.insideUnitCircle * spawnSpread;
            GameObject spawned = Instantiate(itemPrefab, origin + (Vector3)offset, Quaternion.identity);

            Comida comida = spawned.GetComponent<Comida>();
            if (comida != null) 
                comida.Launch();
        }
    }
}