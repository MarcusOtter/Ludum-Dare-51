using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawn : MonoBehaviour
{
    [SerializeField] private Transform objectToSpawn;
    void Start()
    {
        var p = new GameObject("Parent");
        for(int x = 0; x < 25; x++)
        {
            for (int z = 0; z < 25; z++)
            {
                var dot = Instantiate(objectToSpawn);
                dot.parent = p.transform;
                objectToSpawn.position = new Vector3(x, 0, z);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
