using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;

    public float jumHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;
    bool isWall;
    public Transform WallCheck;
    public float WallDistance = 0.4f;
    public LayerMask wallMask;
    private bool iswallrunning = false;
    private bool flag = false;
    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isWall = Physics.CheckSphere(WallCheck.position, WallDistance, wallMask);
        if (isGrounded && velocity.y < 0)
        {            
            velocity.y = -2f;
        }
        if (isGrounded || !isWall)
        {
            iswallrunning = false;
            flag = false;
            
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        //Parkour andar na parede
        if (isWall && !isGrounded)
        {
            iswallrunning = true;
        }
        if (iswallrunning)
        {
            Vector3 direction = new Vector3(1,1,1);
            if (flag==false)
            {
                Debug.Log(velocity.y);
                velocity.y = 2f + velocity.y;
                Debug.Log(velocity.y);
                flag = true;
                direction = new Vector3();
                direction.Normalize();
            }
            transform.position += direction * 4 * Time.deltaTime;
            velocity.y = velocity.y + 0.01f;
            Debug.Log(Time.deltaTime);
            //Debug.Log(velocity.y);
        }
        Debug.Log(gameObject.transform.position.y);
        //Debug.Log("Está na parede:"+isWall);
        //Debug.Log("Está no chao:" + isGrounded);
        //Debug.Log(iswallrunning);
    }
}
