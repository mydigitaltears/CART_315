using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hookThrowerScript : MonoBehaviour
{
    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject hookBall;
    public GameObject hookBallInst;
    private Vector2 worldPosition;
    private Vector2 direction;
    private float angle;
    private float throwPower = 30f;
    public bool isHooked = false;
    public Vector2 hookPosition;
    private bool isThrow = false;
    private bool triggerIsDown = false;
    private bool controllerIsUsed = false;
    private Vector3 lastMousePos;
    private Vector2 lastHandTransform = new Vector2 (0f, 0f);
    private float inputSpeed = 20f;
    public bool isAlive = false;

    void Start()
    {
        hookBallInst = hookBall;
    }
    void Update()
    {
        handleRotation();
        handleThrowing();
        if(!isHooked && isThrow && isAlive){
            if(hookBallInst.GetComponent<hookBallScript>().isHooked)
            {
                isHooked = true;
                isThrow = false;
                hookPosition = hookBallInst.GetComponent<hookBallScript>().hookPosition;
                Debug.Log("isHooked!");
                Destroy(hookBallInst.gameObject);
                isAlive = false;
            }
        }
        if(isThrow && isAlive)
        {
            float distance = hookBallInst.transform.position.y - transform.position.y;
            if(distance > 15f){
                Destroy(hookBallInst.gameObject);
                isAlive = false;
            }
        }
    }

    private void handleRotation()
    {
        // rotate towards mouse
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.mousePosition != lastMousePos)
        {
            lastMousePos = Input.mousePosition;
            direction = (worldPosition - (Vector2)hand.transform.position).normalized;
            lastHandTransform = direction;
            controllerIsUsed = false;
            Debug.Log("MovingMouse");
        }

        if (Input.GetAxisRaw("JoyH") != 0.0f || Input.GetAxisRaw("JoyV") != 0.0f)
        {
            Vector2 joyInput = new Vector2(Input.GetAxisRaw("JoyH"), -Input.GetAxisRaw("JoyV")).normalized;
            direction += joyInput * Time.deltaTime * inputSpeed;
            direction.x = Mathf.Clamp(direction.x, -1f, 1f);
            direction.y = Mathf.Clamp(direction.y, -1f, 1f);
            direction = new Vector2(direction.x, direction.y).normalized;
            lastHandTransform = direction;
            controllerIsUsed = true;
            Debug.Log("UsingController");
        }

        hand.transform.right = lastHandTransform;
        // flip at 90 deg
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Vector3 localScale = new Vector3(1f,1f,1f);
        if (angle > 90 || angle < -90)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = 1f;
        }
        hand.transform.localScale = localScale/7f;
    }

    private void handleThrowing()
    {
        // if (Input.GetKey(KeyCode.Mouse0))
        // {
        //     if(isThrow){
        //         Destroy(hookBallInst);
        //     }
        //     isThrow = false;
        //     if(throwPower <= 15f){
        //         throwPower += 0.05f;
        //     }
        // }
        // if (Input.GetKeyUp(KeyCode.Mouse0))
        // {
        //     hookBallInst = hookBall;
        //     hookBallInst.GetComponent<hookBallScript>().throwPower = throwPower;
        //     hookBallInst = Instantiate(hookBall, transform.position, hand.transform.rotation);
        //     throwPower = 0f;
        //     isHooked = false;
        //     isThrow = true;
        // }
        if (Input.GetKeyDown(KeyCode.Mouse0) || (Input.GetAxisRaw("Hook") == 1 && !triggerIsDown))
        {
            hookBallInst = hookBall;
            hookBallInst.GetComponent<hookBallScript>().throwPower = throwPower;
            hookBallInst = Instantiate(hookBall, transform.position, hand.transform.rotation);
            //throwPower = 0f;
            isHooked = false;
            isThrow = true;
            isAlive = true;      
            if(Input.GetAxisRaw("Hook") == 1)
            {
                triggerIsDown = true;
            }      
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) || (Input.GetAxisRaw("Hook" ) == 0 && triggerIsDown))
        {
            if(isThrow){
                Destroy(hookBallInst.gameObject);
                isAlive = false;
            }
            isThrow = false;            
            if(Input.GetAxisRaw("Hook") == 0)
            {
                triggerIsDown = false;
            }      
        }
    }
}
