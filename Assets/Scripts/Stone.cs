using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    [SerializeField] Sprite[] stoneSprites;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = stoneSprites[Random.Range(0, stoneSprites.Length)];
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Node>().StopGrowth();
    }
}
