using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private float xPos;
    private float yPos;
    private Vector3 _direction = Vector2.right;

    public float speed = 0.1f;
    public float leftWall = 4.0f;
    public float rightWall = -4.0f;
    // Start is called before the first frame update
    void Start()
    {
        xPos = this.transform.position.x;
        yPos = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += _direction * this.speed;

        if (this.transform.position.x > rightWall){
            _direction = Vector2.left;
        }
        if (this.transform.position.x < leftWall){
            _direction = Vector2.right;
        }
    }
}
