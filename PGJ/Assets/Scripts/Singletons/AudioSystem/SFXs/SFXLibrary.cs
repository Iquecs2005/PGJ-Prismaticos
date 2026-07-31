using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXLibrary : MonoBehaviour
{
    [SerializeField] private SFXGroup[] sfxList;

    public SFXGroup GetSFXData(string sfxName) 
    {
        print(sfxList[0].name);
        return sfxList[0];
    }
}
