using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    [SerializeField] private GameObject sfxSourcePrefab;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public static void PlaySFX(SFXGroup sfxGroup)
    {
        PlaySFX(sfxGroup, false, Vector3.zero);
    }

    public static void PlaySFX(SFXGroup sfxGroup, Vector3 pos, Transform parent = null) 
    {
        PlaySFX(sfxGroup, true, pos, parent);
    }

    private static void PlaySFX(SFXGroup sfxGroup, bool directional, Vector3 pos, Transform parent = null) 
    {
        if (instance == null)
        {
            Debug.LogError("SFXManager.PlaySFX: No SFXManager on scene. Please put GameManager Prefab on scene");
            return;
        }
        if (sfxGroup == null)
        {
            Debug.LogError("SFXManager.PlaySFX: Trying to play Null SFX Data");
            return;
        }

        SFXData data = sfxGroup.GetSFXData();
        if (parent != null)
            pos += parent.position;
        GameObject source = Instantiate(instance.sfxSourcePrefab, pos, Quaternion.identity, parent);
        source.GetComponent<SFXSource>().Play(data, directional);
    }
}
