using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    //Serialized for debug
    Node[] children = new Node[3];
    [SerializeField] RootConfig rootConfig;
    [SerializeField] float growthTime = 1;
    Coroutine growthCoroutine;

    SpriteRenderer spriteRenderer;
    bool isGrowing = true;
    bool isStopped = false;
    bool canSplit = false;
    Vector3 startMousePosition;
    Sprite[] growingSprites;
    Sprite fullGrowthSprite;
    GameObject selector;
    int direction;
    int waterReserve = 0; 
    int stoppedConsumption = 1;
    int baseConsumption = 3;
    int growingConsumption = 6;
    int depth = 0;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        growingSprites = rootConfig.GetGrowingSprites();
        fullGrowthSprite = rootConfig.GetFullGrowthSprite();
        direction = rootConfig.GetDirection();
        selector = this.gameObject.transform.GetChild(0).gameObject;
        growthCoroutine = StartCoroutine(Grow());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddWater(int water)
    {
        waterReserve += water;
    }

    public int[] Visit()
    {
        int waterIn;
        if (waterReserve > 10) 
            waterIn = 10;
        else
            waterIn = waterReserve;
        waterReserve -= waterIn;
        int waterOut = baseConsumption;
        if (isGrowing)
            waterOut = growingConsumption;
        else if (isStopped)
            waterOut = stoppedConsumption;
        int childDepth = 0;
        if (isGrowing)
            waterOut += growingConsumption;
        
        foreach (var child in children)
        {
            if (child != null)
            {
                int [] inOut = child.Visit();
                waterIn += inOut[0];
                waterOut += inOut[1];
                if (inOut[2] > childDepth)
                    childDepth = inOut[2];
            }
        }
        return new int[] {waterIn, waterOut, childDepth+depth};
    }

    void OnMouseDown()
    {
        startMousePosition = Input.mousePosition;
        selector.SetActive(true);
    }
    void OnMouseUp()
    {
        selector.SetActive(false);
        Vector3 endMousePosition = Input.mousePosition;
        float angle = Vector3.Angle(Vector3.right, endMousePosition - startMousePosition);
        if (endMousePosition.y > startMousePosition.y && angle > 70 && angle < 110)
        {
            isStopped = true;
        }
        if (isGrowing)
            return;
        if (angle > 10 && angle < 70 && children[2] == null)
        {
            isStopped = false;
            GrowChild(2);
            // rightChild.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        else if (angle > 70 && angle < 110 && children[1] == null)
        {
            isStopped = false;
            GrowChild(1);
            // centerChild.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        else if (angle > 110 && angle < 170 && children[0] == null)
        {
            isStopped = false;
            GrowChild(0);

            // leftChild.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }

    IEnumerator Grow() 
    {
        for (int i = 0; i < 4; i++)
        {
            spriteRenderer.sprite = growingSprites[i];
            depth++;
            yield return new WaitForSeconds(growthTime);
        }
        isGrowing = false;
        canSplit = true;
        // gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        if (!isStopped)
        {
            GrowChild(direction);
            yield return new WaitForSeconds(growthTime * 5);
            spriteRenderer.sprite = fullGrowthSprite;
        }

    }

    void GrowChild(int childDirection) 
    {
        // if (direction !=  childDirection)
        //     for (int i = 0; i < 2; i++)
        //     {
                
        //     }
        if (childDirection == direction)
        {
            children[childDirection] = Instantiate(
            rootConfig.GetChildrenPrefab()[childDirection],
            transform.position + new Vector3((childDirection-1), -1f, 0),
            // Quaternion.Euler(0,0,Random.Range(-6, 6))
            Quaternion.identity
            ) as Node;

        }
        else
        {
            children[childDirection] = Instantiate(
            rootConfig.GetChildrenPrefab()[childDirection],
            transform.position + new Vector3((childDirection-1)/2f, -0.5f, 0),
            //TODO farlo sembrare bello
            Quaternion.Euler(0,0,Random.Range(-6, 6))
            // Quaternion.identity
            ) as Node;
            }
        // yield return null;
    }

    public void StopGrowth()
    {
        isGrowing = false;
        isStopped = true;
        StopCoroutine(growthCoroutine);
    }

    // public void OnTriggerEnter2D(Collider2D collision)
    // {
    //     Node node = collision.GetComponent<Node>();
    //     // if (!node == null)
    //         node.StopGrowth();
    // }
}
