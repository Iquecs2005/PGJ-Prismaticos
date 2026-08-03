using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSFXPlayer : MonoBehaviour
{
    [SerializeField] private SFXGroup sfxGroup;

    [Header("Intervalo")]
    [SerializeField] private float minInterval = 3f;
    [SerializeField] private float maxInterval = 8f;
    [SerializeField] private bool positional = true;

    private void OnEnable()
    {
        StartCoroutine(PlayRoutine());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private IEnumerator PlayRoutine()
    {
        while (true)
        {
            float wait = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(wait);

            if (sfxGroup == null)
                continue;

            if (positional)
                SFXManager.PlaySFX(sfxGroup, transform.position, transform);
            else
                SFXManager.PlaySFX(sfxGroup);
        }
    }
}