using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManagerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject Item;
    [SerializeField]
    private GameObject GameManager;

    [SerializeField]
    private Vector3 position = new Vector3{};

    [SerializeField]
    private int scoreToStart;
    [SerializeField]
    private float waitingTime;
    [SerializeField]
    private float itemSpeed;
    
    private bool started = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetComponent<GameManagerScript>().score == scoreToStart && !started)
        {
            StartCoroutine(ItemSpawn());
            started = true;
        }
    }

    private IEnumerator ItemSpawn()
    {   
        Item.GetComponent<ItemScript>().speed = itemSpeed;
        Instantiate(Item, position, Quaternion.identity);
        yield return new WaitForSeconds(waitingTime);
        StartCoroutine(ItemSpawn());
    }
}
