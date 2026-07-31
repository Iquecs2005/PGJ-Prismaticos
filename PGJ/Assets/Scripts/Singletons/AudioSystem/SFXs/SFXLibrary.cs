using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXLibrary : MonoBehaviour
{
    [SerializeField] private SFXData[] sfxList;

    public SFXData GetSFXData(string sfxName) 
    {
        print(sfxList[0].name);
        return sfxList[0];
    }
}
