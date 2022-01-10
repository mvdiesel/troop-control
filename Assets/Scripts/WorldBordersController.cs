using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBordersController : MonoBehaviour
{
    [SerializeField] private List<Transform> worldBorderObjects;
    private List<float> worldBorderValues = new List<float>();
    
    // Start is called before the first frame update
    void Awake()
    {
        CalculateWorldBorderValues();
    }

    private void CalculateWorldBorderValues()
    {
        float minX = float.MaxValue,
            maxX = float.MinValue,
            minY = float.MaxValue,
            maxY = float.MinValue,
            minZ = float.MaxValue,
            maxZ = float.MinValue;
        
        foreach (var borderObject in worldBorderObjects)
        {
            var position = borderObject.position;

            if (position.x < minX)
                minX = position.x;
            if (position.x > maxX)
                maxX = position.x;
            if (position.y < minY)
                minY = position.y;
            if (position.y > maxY)
                maxY = position.y;
            if (position.z < minZ)
                minZ = position.z;
            if (position.z > maxZ)
                maxZ = position.z;

        }
        
        worldBorderValues.Add(minX);
        worldBorderValues.Add(maxX);
        worldBorderValues.Add(minY);
        worldBorderValues.Add(maxY);
        worldBorderValues.Add(minZ);
        worldBorderValues.Add(maxZ);
    }

    public List<float> GetWorldBorderValues()
    {
        return worldBorderValues;
    }
}
