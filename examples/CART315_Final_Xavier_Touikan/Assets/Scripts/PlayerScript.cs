using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer _lineRenderer;
    public DistanceJoint2D _distanceJoint;
    public hookThrowerScript hts;

    // MOVEMENT
    private float horizontal;
    private float vertical;
    private float speed = 8f;
    private float acceleration = 2f;
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

    // UPAIRATTACK
    private bool canUpAir = true;
    private bool isUpAir = false;
    private bool upAirHit = false;
    private float upAirPower = 10f;
    private float upAirTime = 0.2f;
    private float upAirCooldown = 1f;
    private float upAirLag = 0.05f;

    // GRAPPLINGHOOK
    private bool isThrow = false;
    private bool isGrap = false;
    private float throwPower = 0f;
    private bool triggerHookController = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask dashLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private GameObject dashRefresh;

    
    void Start()
    {
        _distanceJoint.enabled = false;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        

        // ONLY HOOK
        // DoubleJump();

        //LongJump();
        //OriginalHook();
        BallHook();

        if (_lineRenderer.enabled)
        {
            _lineRenderer.SetPosition(1, transform.position);
        }
        //Dash
        if (Input.GetButtonDown("Dash") && canAttack && canDash)
        {
            StartCoroutine(DashAttack());
        }
        if (horizontal == -1 && canAttack)
        {
            if (!dashBufferLeft)
            {
                float timeSinceLastHit = Time.time - dashBufferLeftTime;
                if (timeSinceLastHit <= 0.2f && canDash && isGrap)
                {
                    StartCoroutine(DashAttack());
                }
                dashBufferLeftTime = Time.time;
            }
            dashBufferLeft = true;
        }
        if (horizontal == 1 && canAttack)
        {
            if (!dashBufferRight)
            {
                float timeSinceLastHit = Time.time - dashBufferRightTime;
                if (timeSinceLastHit <= 0.2f && canDash && isGrap)
                {
                    StartCoroutine(DashAttack());
                }
                dashBufferRightTime = Time.time;
            }
            dashBufferRight = true;
        }

        else if (horizontal == 0)
        {
            dashBufferLeft = false;
            dashBufferRight = false;
        }

        //UpAir
        // if (Input.GetKeyDown(KeyCode.UpArrow) && canAttack)
        // {
        //     StartCoroutine(UpAir());
        // }

        //If Grounded
        if (IsGrounded()){
            doubleJump = true;
            canAttack = true;
        }
        // if (isDashing && !dashAttackHit)
        // {
        //     Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 0.5f, enemyLayer);
        //     foreach(Collider2D enemy in hitEnemies)
        //     {
        //         Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
        //         enemyRb.velocity = new Vector2((enemyRb.velocity.x + rb.velocity.x)/5, dashAttackPower);
        //         dashAttackHit = true;
        //     }
        // }

        // if (isUpAir && !upAirHit)
        // {
        //     Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 0.5f, enemyLayer);
        //     foreach(Collider2D enemy in hitEnemies)
        //     {
        //         Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
        //         enemyRb.velocity = new Vector2((enemyRb.velocity.x + rb.velocity.x)/2, upAirPower);
        //         upAirHit = true;
        //     }
        // }
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, transform.position.y, mainCamera.transform.position.z);
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
        // if (IsGrounded())
        // {
        //     // if (momentum){
        //     //     rb.velocity = new Vector2(pastXVel*0.99f, rb.velocity.y);
        //     //     StartCoroutine(Momentum(combinedVel));
        //     // }
        //     // else
        //     // {
        //     //     rb.velocity = new Vector2((horizontal * speed), rb.velocity.y);
        //     // }
        //     //rb.velocity = new Vector2((horizontal * speed), rb.velocity.y);
        //     HandleAcceleration();
        // }
        // else
        // {
        //     momentum = true;
        //     if (((pastXVel >= 0f && horizontal <= 0f) || (pastXVel <= 0f && horizontal >= 0f)) && isGrap == false)
        //     {
        //         HandleAcceleration();
        //     }
        //     else if (Mathf.Abs(pastXVel) <= speed && !isGrap)
        //     {
        //         HandleAcceleration();
        //     }
        //     else
        //     {
        //         Debug.Log("VELO BUG?");
        //     }
        //     // if (((pastXVel >= 0f && horizontal < 0f) || (pastXVel <= 0f && horizontal > 0f)) && isGrap == false)
        //     // {
        //     //     // if(Mathf.Abs(pastXVel) < speed)
        //     //     // {
        //     //     //     newXVel = pastXVel + ((horizontal * speed)*0.5f);
        //     //     // }
        //     //     rb.velocity = new Vector2(((horizontal * speed)*0.5f), rb.velocity.y);
        //     //     //rb.velocity = new Vector2(newXVel, rb.velocity.y);
        //     //     Debug.Log("Air Drifting!");
        //     // }

        //     // if (isGrap == false)
        //     // {
        //     //     rb.velocity = new Vector2(((horizontal * speed)), rb.velocity.y);
        //     // }
        // }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
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

    private void LongJump()
    {
        // if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f)
        // {
        //     rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        // }
        if (Input.GetButton("Jump"))
        {
            jumping = true;
        }
        else
        {
            jumping = false;
        }
        if (!jumping && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        Debug.Log(jumping);
    }

    private void OriginalHook()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
            if(Physics2D.OverlapCircle(mousePos, 0.1f, groundLayer) && !isGrap)
            {
                _lineRenderer.SetPosition(0, mousePos);
                _lineRenderer.SetPosition(1, transform.position);
                _distanceJoint.connectedAnchor = mousePos;
                _distanceJoint.enabled = true;
                _lineRenderer.enabled = true;
                isGrap = true;
            }

        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _distanceJoint.enabled = false;
            _lineRenderer.enabled = false;
            isGrap = false;
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
            isGrap = true;
            hts.isHooked = false;
            //ONLYDASH MOVEMENT
            canAttack = true;
            canDash = true;
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
    private IEnumerator DashAttack()
    {
        canAttack = false;
        isDashing = true;
        canDash = false;
        float originalGravity = rb.gravityScale;
        if(vertical < 0.5){
            rb.gravityScale = 0f;
        }
        Vector2 dashDirection = new Vector2(horizontal, vertical).normalized;
        if(dashDirection.y > Mathf.Abs(dashDirection.x))
        {
            dashDirection = new Vector2(0f, 1f);
        }
        else if(dashDirection.x > 0f)
        {
            dashDirection = new Vector2(1f, 0f);
        }
        else if(dashDirection.x < 0f)
        {
            dashDirection = new Vector2(-1f, 0f);
        }
        else
        {
            dashDirection = new Vector2(0f, 0f);
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

    private IEnumerator UpAir()
    {
        canAttack = false;
        isUpAir = true;
        rb.velocity = new Vector2(transform.localScale.x, transform.localScale.y + upAirPower);
        yield return new WaitForSeconds(upAirTime);
        isUpAir = false;
        landLagTime = upAirLag;
        //yield return new WaitForSeconds(upAirCooldown);
        upAirHit = false;
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
    }
}
