using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RootConfig : ScriptableObject
{
    // [SerializeField] GameObject[] childrenPrefab;
    [SerializeField] GameObject leftChildPrefab;
    [SerializeField] GameObject centerChildPrefab;
    [SerializeField] GameObject rightChildPrefab;
    [SerializeField] Sprite[] growingSprites;
    [SerializeField] int direction;
    [SerializeField] Sprite[] growingSpritesChildEO, growingSpritesChildNS, growingSpritesChildOE;
    [SerializeField] Sprite fullGrowthSprite;

    public Node[] GetChildrenPrefab() {
        return new Node[] {leftChildPrefab.GetComponent<Node>(), centerChildPrefab.GetComponent<Node>(), rightChildPrefab.GetComponent<Node>()};
    }
    public GameObject GetLeftChildPrefab() {return leftChildPrefab;}
    public GameObject GetRightChildPrefab() {return rightChildPrefab;}
    public GameObject GetCenterChildPrefab() {return centerChildPrefab;}
    public Sprite[] GetGrowingSprites(){return growingSprites;}
    public int GetDirection() {return direction;}
   public Sprite[] GetGrowingSpritesChildEO(){return growingSpritesChildEO;}
   public Sprite[] GetGrowingSpritesChildNS(){return growingSpritesChildNS;}
   public Sprite[] GetGrowingSpritesChildOE(){return growingSpritesChildOE;}
   public Sprite GetFullGrowthSprite(){return fullGrowthSprite;}

}
