using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Vector3 targetPositionOnMap;

    void Start()
    {
        RaycastToGround();
    }

    public void RaycastToGround()
    {
        RaycastHit hit;
        float distance = 100f;
        Vector3 targetLocation;
        var layerMask = ~(1 << 6);
 
        if(Physics.Raycast(transform.position, Vector3.down, out hit, distance, layerMask)) 
        {
            targetLocation = hit.point;
            targetPositionOnMap = targetLocation;
            Debug.Log("Changed target position on map!");
            Debug.Log("New target position is: " + targetPositionOnMap);
        }
    }

    public Vector3 GetTargetPositionOnMap()
    {
        return targetPositionOnMap;
    }
}
