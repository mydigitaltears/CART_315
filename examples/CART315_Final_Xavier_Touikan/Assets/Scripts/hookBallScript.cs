using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hookBallScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    public float throwPower = 0f;
    public bool isHooked = false;
    public Vector2 hookPosition;
    // Start is called before the first frame update
    void Start()
    {
        ThrowBall();
        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        if(Physics2D.OverlapCircle(transform.position, 0.5f, groundLayer))
        {
            hookPosition = (Vector2)transform.position;
            isHooked = true;
        }
        else
        {
            isHooked = false;
        }
    }
    public void ThrowBall()
    {
        rb.velocity = transform.right * throwPower;
    }
}
