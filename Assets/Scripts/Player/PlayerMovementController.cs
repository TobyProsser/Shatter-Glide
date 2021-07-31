using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float speed;
    public float lift;

    Rigidbody2D rb;
    Vector2 moveDirection;


    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    private void LateUpdate()
    {

        //Rotate player towards move direction
        moveDirection = rb.velocity;         
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        //Continously move player forward
        rb.velocity = new Vector2(speed, rb.velocity.y);

        //If mouse button is down, add force upwards
        if (Input.GetMouseButton(0)) rb.AddForce(new Vector2(0, 1) * lift * Time.deltaTime);
    }
}
