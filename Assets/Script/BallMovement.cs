using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public LayerMask gLayer;
    private Rigidbody rb;
    public float speed; 
    private Vector3 targetPos;
    public bool touch;
    public bool touchAndHold;
    public float holdTimeThreshold = 0.5f;
    private float holdTimer;
    public float smoothTime = 0.3f;
    private Vector3 currentVelocity;

    private bool isMoving;
    private bool isCooldown; 
    public float clickCooldown = 0.5f; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isCooldown) return; 

        if (Input.GetMouseButtonDown(0)) 
        {
            touch = true;
            holdTimer = 0f;
        }

        if (Input.GetMouseButton(0)) 
        {
            holdTimer += Time.deltaTime;
            if (holdTimer > holdTimeThreshold)
            {
                touchAndHold = true;
                touch = false;
                MoveBallOnHold();
            }
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            if (touchAndHold)
            {
                //MoveBallOnHold();
            }
            else if (touch)
            {
                MoveBall();
            }
            
            StartCoroutine(StartClickCooldown());
            
            touch = false;
            touchAndHold = false;
        }
    }

    private void MoveBall()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, gLayer))
        {
            targetPos = hit.point;
            targetPos.y = this.transform.position.y;

            Vector3 direction = (targetPos - rb.position).normalized;
            rb.AddForce(direction * speed, ForceMode.Impulse);
        }
    }

    private void MoveBallOnHold()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, gLayer) && !isMoving)
        {
            targetPos = hit.point;
            targetPos.y = transform.position.y; 
            StartCoroutine(MoveToTarget());
        }
    }

    private IEnumerator MoveToTarget()
    {
        isMoving = true;
        Vector3 startPos = rb.position;

        while (Vector3.Distance(rb.position, targetPos) > 0.1f)
        {
            if (!touchAndHold)
            {
                rb.velocity = Vector3.zero;
                isMoving = false;
                yield break;
            }
            
            rb.position = Vector3.SmoothDamp(rb.position, targetPos, ref currentVelocity, smoothTime, speed, Time.deltaTime);
            yield return null;
        }
        
        rb.position = targetPos;
        rb.velocity = Vector3.zero; 
        isMoving = false;
    }

    private IEnumerator StartClickCooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(clickCooldown); 
        isCooldown = false;
    }
}
