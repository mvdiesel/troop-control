using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
 
public class DragBehaviour : MonoBehaviour
{
    [SerializeField] private Target targetController;
    [SerializeField] private Rigidbody targetRigidbody;
    [SerializeField] private WorldBordersController worldBorderController;
    
    private Vector3 screenPoint;
    private Vector3 offset;
    
    float minX = -24,
        maxX = 24,
        minY = 0.5f,
        maxY = 10,
        minZ = -24,
        maxZ = 24;

    void Start()
    {
        CacheBorderValues();
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
 
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
 
    }
 
    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
 
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

        curPosition.x = Mathf.Clamp(curPosition.x, minX, maxX);
        curPosition.y = Mathf.Clamp(curPosition.y, minY, maxY);
        curPosition.z = Mathf.Clamp(curPosition.z, minZ, maxZ);
        
        targetRigidbody.velocity = Vector3.one;
        transform.position = curPosition;
        targetController.RaycastToGround();
    }

    void OnMouseUp()
    {
        targetRigidbody.velocity = Vector3.zero;
    }

    void CacheBorderValues()
    {
        var borderValues = worldBorderController.GetWorldBorderValues();

        minX = borderValues[0];
        maxX = borderValues[1];
        minY = borderValues[2];
        maxY = borderValues[3];
        minZ = borderValues[4];
        maxZ = borderValues[5];
    }
}
