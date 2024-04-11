using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScriptONLYHOOK : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer _lineRenderer;
    public DistanceJoint2D _distanceJoint;
    public hookThrowerScript hts;

    // MOVEMENT
    private float horizontal;
    private float vertical;
    private float rJoyH;
    private float speed = 8f;
    private float acceleration = 1f;
    private float pastXVel = 0f;
    private float jumpingPower = 16f;
    private bool jumping = false;
    private bool isFacingRight = true;
    private bool doubleJump = true;
    private bool canAttack = true;
    private float landLagTime = 0f;
    private bool momentum = false;
    private float newXVel = 0f;

    // DASHATTACk
    private bool isDashing = false;
    private bool dashAttackHit = false;
    private float dashingPower = 24f;
    private float dashAttackPower = 10f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    private float dashAttackLag = 0.3f;
    private bool dashBufferLeft = false;
    private bool dashBufferRight = false;
    private float dashBufferLeftTime;
    private float dashBufferRightTime;
    private bool canDash = true;

    // DASHREFRESH
    private float dashRefreshRespawnCooldown = 1.0f;
    private bool canDashRefreshRespawn = true;

    // DESTPLAT
    private float destPlatCooldown = 2.0f;
    private bool destPlatRespawn = true;
    private bool canDestroyPlat = false;

    // GRAPPLINGHOOK
    private bool isThrow = false;
    private bool isGrap = false;
    private float throwPower = 0f;
    private bool triggerHookController = false;

    private Vector2 respawn = new Vector2(-10f, -10f);

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask lavaLayer;
    [SerializeField] private LayerMask windLayer;
    [SerializeField] private LayerMask dashLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private GameObject dashRefresh;
    [SerializeField] private GameObject destPlat;

    
    void Start()
    {
        _distanceJoint.enabled = false;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        rJoyH = Input.GetAxisRaw("RJoyH");

        // ONLY HOOK
        DoubleJump();
        //LongJump();
        BallHook();

        if (_lineRenderer.enabled)
        {
            _lineRenderer.SetPosition(1, transform.position);
        }
        if ((rJoyH >= 0.5f || rJoyH <= -0.5f || Input.GetButtonDown("DashLeft") || Input.GetButtonDown("DashRight"))&& canAttack && canDash && isGrap)
        {
            if (rJoyH >= 0.5f || Input.GetButtonDown("DashRight"))
            {
                StartCoroutine(DashAttack(new Vector2(1f, 0f)));
            }
            else if (rJoyH <= -0.5f || Input.GetButtonDown("DashLeft"))
            {
                StartCoroutine(DashAttack(new Vector2(-1f, 0f)));
            }  
        }
        
        if (isGrap)
        {
            canAttack = true;
        }

        //If Grounded
        if (IsGrounded()){
            //doubleJump = true;
            canAttack = true;
        }
        if (IsLava()){
            transform.position = respawn;
            _distanceJoint.enabled = false;
            _lineRenderer.enabled = false;
            isGrap = false;
            Destroy(hts.hookBallInst.gameObject);
            hts.isAlive = false;
        }
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
        //Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        pastXVel = rb.velocity.x;
        float combinedVel = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y);
        
        //ONLY DASH
        if (IsGrounded())
        {
            // if (momentum){
            //     rb.velocity = new Vector2(pastXVel*0.99f, rb.velocity.y);
            //     StartCoroutine(Momentum(combinedVel));
            // }
            // else
            // {
            //     rb.velocity = new Vector2((horizontal * speed), rb.velocity.y);
            // }
            //rb.velocity = new Vector2((horizontal * speed), rb.velocity.y);
            HandleAcceleration();
        }

        // AIR MOVEMENT
        else if(IsWind())
        {
            momentum = true;
            if (((pastXVel >= 0f && horizontal <= 0f) || (pastXVel <= 0f && horizontal >= 0f)) && !isGrap)
            {
                HandleAcceleration();
            }
            else if (Mathf.Abs(pastXVel) <= speed && !isGrap)
            {
                HandleAcceleration();
            }
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, new Vector2(1f, 0.1f), 0f, groundLayer);
    }
    private bool IsLava()
    {
        return Physics2D.OverlapBox(groundCheck.position, new Vector2(1f, 0.1f), 0f, lavaLayer);
    }
    private bool IsWind()
    {
        return Physics2D.OverlapBox(groundCheck.position, new Vector2(1f, 0.1f), 0f, windLayer);
    }

    private void HandleAcceleration()
    {
        rb.velocity = new Vector2(pastXVel + (horizontal * acceleration), rb.velocity.y);
        if (Mathf.Abs(pastXVel) <= speed && horizontal != 0)
        {
            rb.velocity = new Vector2(pastXVel + (horizontal * acceleration), rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2((horizontal * speed), rb.velocity.y);
        }
    }
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void DoubleJump()
    {
        if (Input.GetButtonDown("Jump") && (IsGrounded() || doubleJump))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            if (doubleJump){
                doubleJump = false;
            }
        }
    }
    private void BallHook()
    {
        if (hts.isHooked)
        {
            Debug.Log("hooked!");
            _lineRenderer.SetPosition(0, hts.hookPosition);
            _lineRenderer.SetPosition(1, transform.position);
            _distanceJoint.connectedAnchor = hts.hookPosition;
            _distanceJoint.enabled = true;
            _lineRenderer.enabled = true;
            if(IsGrounded() && (Mathf.Abs(hts.hookPosition.x - transform.position.x) < 5f))
            {
                rb.velocity = new Vector2(rb.velocity.x, 6f);
                _distanceJoint.distance -= _distanceJoint.distance/10f;
            }
            isGrap = true;
            hts.isHooked = false;
            doubleJump = true;
            if (Input.GetAxisRaw("Hook") == 1)
            {
                triggerHookController = true;
            }
        }
        if ((Input.GetKeyUp(KeyCode.Mouse0) || (Input.GetAxisRaw("Hook") == 0 && triggerHookController)) && isGrap)
        {
            _distanceJoint.enabled = false;
            _lineRenderer.enabled = false;
            isGrap = false;
            if (Input.GetAxisRaw("Hook") == 0)
            {
                triggerHookController = false;
            }
        }
    }
    private IEnumerator DashAttack(Vector2 dashDirection)
    {
        canAttack = false;
        isDashing = true;
        canDash = false;
        float originalGravity = rb.gravityScale;
        if(dashDirection.y < 0.5){
            rb.gravityScale = 0f;
        }

        //rb.velocity = new Vector2(transform.localScale.x * dashingPower, vertical * dashingPower);
        rb.velocity = dashDirection * dashingPower;
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        landLagTime = dashAttackLag;
        yield return new WaitForSeconds(dashingCooldown);
        dashAttackHit = false;
        canDash = true;
    }
    private IEnumerator Momentum(float Vel)
    {
        float momentumTime = Mathf.Abs(Vel) / 100f;
        yield return new WaitForSeconds(momentumTime);
        momentum = false;
    }
    private IEnumerator DashRefreshRespawn(Vector2 respawnLocation)
    {
        yield return new WaitForSeconds(dashRefreshRespawnCooldown);
        Instantiate(dashRefresh, respawnLocation, Quaternion.identity);
        canDashRefreshRespawn = true;
    }

    private IEnumerator DestPlatRespawn(Vector2 respawnLocation, Collider2D other)
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(other.transform.parent.gameObject);
        yield return new WaitForSeconds(destPlatCooldown);
        Instantiate(destPlat, respawnLocation, Quaternion.identity);
        destPlatRespawn = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Check to see if the tag on the collider is equal to Enemy
        if (other.tag == "DashRefresh")
        {
            canDash = true;
            canAttack = true;
            doubleJump = true;
            Debug.Log("destroying refresh");
            if(canDashRefreshRespawn)
            {
                StartCoroutine(DashRefreshRespawn(other.transform.position));
            }
            canDashRefreshRespawn = false;
            Destroy(other.gameObject);
        }

        if (other.tag == "DestPlat")
        {
            Debug.Log(destPlatRespawn);
            if(destPlatRespawn)
            {
                StartCoroutine(DestPlatRespawn(other.transform.position, other));
            }
            destPlatRespawn = false;
        }
    }
}
