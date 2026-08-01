using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityVision : MonoBehaviour
{
    [Header("Entity References")]
    [SerializeField] private CircleCollider2D visionCollider;

    [Header("Vision Variables")]
    [SerializeField] private float innerRadius;
    [SerializeField] private float outerRadius;
    [SerializeField] private float visionAngle;

    public bool seeing;

    private void OnValidate()
    {
        visionCollider.offset = Vector2.zero;
        visionCollider.radius = outerRadius;
        EffectiveCircleRadius();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        float distance = Vector2.Distance(collision.transform.position, transform.position);

        if (distance < innerRadius) 
        {
            seeing = true;
            return;
        }

        Vector2 dir = collision.transform.position - transform.position;
        float angleBetween = Vector2.Angle(transform.right, dir);
        if (distance < outerRadius && angleBetween < visionAngle)
        {
            seeing = true;
            return;
        }
                
        seeing = false;
    }

    private float EffectiveCircleRadius() 
    {
        float closeCircleRadius = Mathf.Min(outerRadius, innerRadius);
        closeCircleRadius = Mathf.Max(0, closeCircleRadius);
        return closeCircleRadius;
    }

    private Vector3 RotateVector(float angle) 
    {
        return Quaternion.AngleAxis(angle, Vector3.forward) * transform.right;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, EffectiveCircleRadius());

        const int nPoints = 32;

        Vector3[] pointsList = new Vector3[nPoints];
        pointsList[0] = transform.position;

        float currentAngle = -visionAngle;
        float angleStep = visionAngle * 2 / nPoints;

        for (int i = 1; i < nPoints; i++)
        {
            Vector3 newPointPos = transform.position + outerRadius * RotateVector(currentAngle);
            pointsList[i] = newPointPos;
            currentAngle += angleStep;
        }

        Gizmos.DrawLineStrip(pointsList, true);
    }
}
