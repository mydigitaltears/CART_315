using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer _lineRenderer;
    public DistanceJoint2D _distanceJoint;
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    private bool doubleJump = true;
    private bool canAttack = true;
    private float landLagTime = 0f;
    private bool momentum = false;

    // DASHATTACk
    private bool isDashing = false;
    private bool dashAttackHit = false;
    private float dashingPower = 24f;
    private float dashAttackPower = 10f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    private float dashAttackLag = 0.3f;

    // UPAIRATTACK
    private bool canUpAir = true;
    private bool isUpAir = false;
    private bool upAirHit = false;
    private float upAirPower = 10f;
    private float upAirTime = 0.2f;
    private float upAirCooldown = 1f;
    private float upAirLag = 0.05f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private TrailRenderer tr;

    void Start()
    {
        _distanceJoint.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _lineRenderer.SetPosition(0, mousePos);
            _lineRenderer.SetPosition(1, transform.position);
            _distanceJoint.connectedAnchor = mousePos;
            _distanceJoint.enabled = true;
            _lineRenderer.enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _distanceJoint.enabled = false;
            _lineRenderer.enabled = false;
        }

        if (_lineRenderer.enabled)
        {
            _lineRenderer.SetPosition(1, transform.position);
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && (IsGrounded() || doubleJump))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            if (doubleJump){
                doubleJump = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canAttack)
        {
            StartCoroutine(DashAttack());
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && canAttack)
        {
            StartCoroutine(UpAir());
        }
        
        if (IsGrounded()){
            doubleJump = true;
            canAttack = true;
        }

        if (isDashing && !dashAttackHit)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 0.5f, enemyLayer);
            foreach(Collider2D enemy in hitEnemies)
            {
                Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
                enemyRb.velocity = new Vector2((enemyRb.velocity.x + rb.velocity.x)/5, dashAttackPower);
                Debug.Log("DA-hit!");
                dashAttackHit = true;
            }
        }

        if (isUpAir && !upAirHit)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 0.5f, enemyLayer);
            foreach(Collider2D enemy in hitEnemies)
            {
                Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
                enemyRb.velocity = new Vector2((enemyRb.velocity.x + rb.velocity.x)/2, upAirPower);
                Debug.Log("UA-hit!");
                upAirHit = true;
            }
        }

        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, transform.position.y, mainCamera.transform.position.z);

        Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        float pastXVel = rb.velocity.x;
        float combinedVel = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y);
        
        if (IsGrounded())
        {
            if (momentum){
                rb.velocity = new Vector2(pastXVel*0.99f, rb.velocity.y);
                StartCoroutine(Momentum(combinedVel));
            }
            else
            {
                rb.velocity = new Vector2((horizontal * speed), rb.velocity.y);   
            }
        }
        else
        {
            momentum = true;
            // if ((pastXVel >= 0f && horizontal <= 0f) || (pastXVel <= 0f && horizontal >= 0f)){
            //     rb.velocity = new Vector2(rb.velocity.x + ((horizontal * speed)*0.5f), rb.velocity.y);
            //     Debug.Log("moving");
            // }
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
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

    private IEnumerator DashAttack()
    {
        canAttack = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        landLagTime = dashAttackLag;
        yield return new WaitForSeconds(dashingCooldown);
        dashAttackHit = false;
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
}
