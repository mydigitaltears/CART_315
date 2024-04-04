using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmScript : MonoBehaviour
{
    [SerializeField]
    private GameObject GameManager;
    [SerializeField]
    private KeyCode keycode = KeyCode.None;
    private SpriteRenderer sr;

    private bool extended = false;
    private bool collision = false;

    private float rotation;
    private float xPos;
    private float yPos;

    private Color green = new Color(0f, 1f, 0f);
    private Color red = new Color(1f, 0f, 0f);
    private Color white = new Color(1f, 1f, 1f);

    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        xPos = this.transform.position.x;
        yPos = this.transform.position.y;
        rotation = this.transform.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(keycode) && !extended)
        {
            extended = true;
            move(1.0f);
            StartCoroutine(PullBack());
        }
    }
    IEnumerator PullBack()
    {
        yield return new WaitForSeconds(0.2f);
        if (!collision)
            sr.color = red;
        move(-1.0f);
        yield return new WaitForSeconds(0.2f);
        extended = false;
        collision = false;
        sr.color = white;
    }

    private void move(float amount)
    {
        if (rotation == 270)
            xPos += amount;
        else if (rotation == 90)
            xPos -= amount;
        else if (rotation == 0)
            yPos += amount;
        else
            yPos -= amount;
        transform.position = new Vector2(xPos, yPos);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Item")
        {
            collision = true;
            sr.color = green;
            GameManager.GetComponent<GameManagerScript>().score += 1;
            Debug.Log(GameManager.GetComponent<GameManagerScript>().score);
            Destroy(other.gameObject);
        }
    }
}

