using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EntityController
{
    [field: Header("Enemy Controller References")]
    [field: SerializeField] public BaseAI ai { get; protected set; }
    [field: SerializeField] public EntityVision vision { get; protected set; }
}
