using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedController : MonoBehaviour
{
    private SpriteRenderer sr;
    public float xLoc = 0;
    public float score;
    
    float bedSpeed = .1f;
    float r = 0.001f;
    float g = 0.1f;
    float b = 0.1f;
    float oldR = 0.1f;
    int counter = 0;
    float redTest = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        score = 0f;
        sr = this.GetComponent<SpriteRenderer>();
        sr.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Z) && xLoc > -9f) {
            xLoc -= bedSpeed;
            //transform.rotation = new Quaternion(0,0,5,0);
        }
        if(Input.GetKey(KeyCode.X) && xLoc < 9f) {
            xLoc += bedSpeed;
        }
        this.transform.position = new Vector2(xLoc, transform.position.y);
        counter += 1;
        if (counter == 100){
            oldR = r;
            r = Random.Range(0.01f, 1.0f);
            g = Random.Range(0.01f, 1.0f);
            b = Random.Range(0.01f, 1.0f);
            counter = 0;
        }
        redTest = Mathf.Lerp(oldR, r, 0.1f);
        sr.color = new Color(redTest, g, b);
        
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "Sleepy") score += 1;
        else score -= 1;
        
        Destroy(other.gameObject);
    }
}
