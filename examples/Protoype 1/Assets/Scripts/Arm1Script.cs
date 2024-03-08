using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm1Script : MonoBehaviour
{
    public GameObject GameManager;
    private SpriteRenderer sr;
    private bool extended = false;
    private bool collision = false;
    private float xPos;
    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        xPos = this.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q) && !extended)
        {
            extended = true;
            xPos += 1.0f;
            transform.position = new Vector2(xPos, 0);
            StartCoroutine(PullBack());
        }
    }
    IEnumerator PullBack()
    {
        yield return new WaitForSeconds(0.2f);
        if (!collision)
        {
            sr.color = new Color(1f, 0f, 0f);
        }
        xPos -= 1.0f;
        transform.position = new Vector2(xPos, 0);
        yield return new WaitForSeconds(0.2f);
        extended = false;
        collision = false;
        sr.color = new Color(1f, 1f, 1f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Item")
        {
            collision = true;
            sr.color = new Color(0f, 1f, 0f);
            GameManager.GetComponent<GameManagerScript>().score += 1;
            Debug.Log(GameManager.GetComponent<GameManagerScript>().score);
            Destroy(other.gameObject);
        }
    }
}

