using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] GameObject waterPrefab;
    [SerializeField] GameObject stonePrefab;
    [SerializeField] int width = 100, height = 100;
    void Start()
    {
        for (int i = 3; i < height; i++)
        {
            for (int j = - width/2; j < width/2; j++)
            {
                if (Random.Range(0f, 1f) > 0.9 - (i * 0.006f))
                {
                    if (Random.Range(0,5) % 5 != 0)
                        Instantiate(waterPrefab, new Vector3(j, -i, 0), Quaternion.identity);
                    else 
                        Instantiate(stonePrefab, new Vector3(j, -i, 0), Quaternion.identity);
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
