using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    int waterReserve;
    [SerializeField] Sprite[] waterSprites;
    [SerializeField] AudioClip[] waterClips;
    int spriteIndex;
    int waterStep = 105;

    void Start()
    {
        int groundLevel;
        if (transform.position.y < 34)
            groundLevel = 0;
        else if (transform.position.y < 66)
            groundLevel = 4;
        else 
            groundLevel = 8;
        waterReserve = Random.Range(waterStep, waterStep*5);
        spriteIndex = waterReserve / waterStep - 1;
        GetComponent<SpriteRenderer>().sprite = waterSprites[spriteIndex + groundLevel];
    }
    public int GetWaterReserve()
    {
        return waterReserve;
    }

    public void OnTriggerEnter2D(Collider2D collision) 
    {
        collision.GetComponent<Node>().AddWater(waterReserve);
        AudioSource.PlayClipAtPoint(waterClips[spriteIndex], new Vector3(0,0,0), 1);
        Destroy(gameObject);
    }
}
