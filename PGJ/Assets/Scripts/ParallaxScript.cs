using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    [SerializeField] private GameObject cameraObject;
    [SerializeField] private ParallaxLayer[] parallaxEffect;

    private void Start()
    {
        foreach (var parallaxEffect in parallaxEffect)
        {
            parallaxEffect.StoreStartPos();
        }
    }

    private void FixedUpdate()
    {
        foreach (var parallaxEffect in parallaxEffect) 
        {
            parallaxEffect.UpdatePostion(cameraObject);
        }
    }
}

[System.Serializable]
class ParallaxLayer
{
    [SerializeField] private GameObject layerObject;
    [SerializeField] private float parallaxEffect;

    private Vector2 startPos;
    private float xLength;

    public void StoreStartPos() 
    {
        startPos = layerObject.transform.position;
        SpriteRenderer sr = layerObject.GetComponent<SpriteRenderer>();
        if (sr == null)
            sr = layerObject.GetComponentInChildren<SpriteRenderer>();

        xLength = sr.bounds.size.x;
    }

    public void UpdatePostion(GameObject cameraObject) 
    {
        float distanceX = cameraObject.transform.position.x * parallaxEffect;
        float distanceY = cameraObject.transform.position.y * parallaxEffect;
        float xMovement = cameraObject.transform.position.x * (1 - parallaxEffect);

        Vector2 pos = startPos + new Vector2(distanceX, distanceY);
        layerObject.transform.position = pos;

        if (xMovement > startPos.x + xLength)
        {
            startPos.x += xLength;
        }
        else if (xMovement < startPos.x - xLength)
        {
            startPos.x -= xLength;
        }
    }
}
