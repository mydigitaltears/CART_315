using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1ManagerScript : MonoBehaviour
{
    public GameObject Item1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Item1Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Item1Spawn()
    {
        float x = Random.Range(-8f, 0f);
        Instantiate(Item1, new Vector3(-7f, 5f, 0f), Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Item1Spawn());
    }
}
