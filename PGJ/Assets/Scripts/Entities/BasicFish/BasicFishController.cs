using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFishController : EntityController
{
    [field: Header("Basic Fish References")]
    [field: SerializeField] public BasicFishAI ai { get; protected set; }
    [field: SerializeField] public EntityVision vision { get; protected set; }
}
