using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1Script : MonoBehaviour
{
    private float xPos = 0.0f;
    public float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        xPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (this.transform.position.y <= -6)
        {
            Destroy(gameObject);
        }
    }
}
