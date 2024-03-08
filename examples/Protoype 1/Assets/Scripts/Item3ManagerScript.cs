using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item3ManagerScript : MonoBehaviour
{
    public GameObject Item3;
    public GameObject GameManager;
    private bool started = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetComponent<GameManagerScript>().score == 20 && !started)
        {
            StartCoroutine(Item1Spawn());
            started = true;
        }
        
    }

    private IEnumerator Item1Spawn()
    {
        float x = Random.Range(-8f, 0f);
        Instantiate(Item3, new Vector3(0f, 5f, 0f), Quaternion.identity);
        yield return new WaitForSeconds(3f);
        StartCoroutine(Item1Spawn());
    }
}
