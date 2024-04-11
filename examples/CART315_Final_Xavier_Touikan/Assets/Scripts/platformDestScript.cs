using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformDestScript : MonoBehaviour
{
    private bool destPlatRespawn = true;
    private float destPlatCooldown = 2.0f;
    [SerializeField] GameObject destPlat;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DestPlatRespawn(Vector2 respawnLocation)
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(transform.parent.gameObject);
        yield return new WaitForSeconds(destPlatCooldown);
        Debug.Log("new plat?");
        Instantiate(destPlat, respawnLocation, Quaternion.identity);
        destPlatRespawn = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hook")
        {
            Debug.Log("hook destplat");
            if(destPlatRespawn)
            {
                StartCoroutine(DestPlatRespawn(transform.position));
            }
            destPlatRespawn = false;
        }
    }
}
