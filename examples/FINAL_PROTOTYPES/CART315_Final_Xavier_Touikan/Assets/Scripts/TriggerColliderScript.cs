using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerColliderScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        //Check to see if the tag on the collider is equal to Enemy
        if (other.tag == "Player")
        {
            Debug.Log("Triggered by Enemy");
            Destroy(gameObject);
        }
    }
}
