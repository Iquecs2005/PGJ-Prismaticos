using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXSource : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private float destructionTimer;

    private void Update()
    {
        if (destructionTimer > 0) 
        {
            destructionTimer -= Time.deltaTime;
            if (destructionTimer <= 0)
                Destroy(gameObject);
        }
    }

    public void Play(SFXData sfx, bool directional = false)
    {
        audioSource.Stop();

        audioSource.clip = sfx.clip;
        audioSource.volume = sfx.volume;
        float pitchShift = Random.Range(sfx.minPitchShift, sfx.maxPitchShift);
        audioSource.pitch = pitchShift;
        destructionTimer = sfx.clip.length / Mathf.Max(0.001f, pitchShift) + 0.5f;

        if (!directional) 
            audioSource.spatialBlend = 0;

        audioSource.Play();
    }
}
