using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1: MonoBehaviour
{
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    public float yLoc = 0;
    
    float bedSpeed = .05f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A) && yLoc > -4f) {
            //yLoc -= bedSpeed;
            rb.AddForce(transform.right * 20f); // Randomly go Left or Right
        }
        if(Input.GetKey(KeyCode.Q) && yLoc < 4f) {
            //yLoc += bedSpeed;
            rb.AddForce(transform.right * -20f);
        }
        this.transform.position = new Vector2(-9, transform.position.y);
        
    }
}
