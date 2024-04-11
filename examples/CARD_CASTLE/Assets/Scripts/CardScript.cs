using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool activated = false;
    Vector3 mousePosition;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //offset = rb.transform.position - mousePosition;
        // if (!activated){
        //     transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // }
        //if (Input.GetMouseButtonDown (0)){
        //    activated = true;
        //}

    }

    void FixedUpdate()
    {
        //if (!activated){
        //    rb.MovePosition(mousePosition);
        //}
    }
}
