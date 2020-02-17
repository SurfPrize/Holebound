using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Camera playercam;

    [SerializeField]
    [Range(0.1f,20)]
    private float speed = 12f;

    [SerializeField]
    [Range(-0.1f, -20)]
    private float gravity = -9.81f;

    [SerializeField]
    [Range(0.5f, 8)]
    private float jumHeight = 3f;

    public Transform groundCheck;

    [SerializeField]
    [Range(0.1f, 3)]
    private float groundDistance = 0.4f;

    public LayerMask groundMask;
    Vector3 velocity;
<<<<<<< HEAD
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
=======
    

    private bool isGrounded;

   

    private void Update()
    {
        if (isLocalPlayer)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
>>>>>>> f5e44e3d8e897948c2b5f68e011cbc4c95cc47d8

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumHeight * -2f * gravity);
            }
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
        else
        {
            playercam.enabled = false;
        }
<<<<<<< HEAD
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
=======
>>>>>>> f5e44e3d8e897948c2b5f68e011cbc4c95cc47d8
    }
}
