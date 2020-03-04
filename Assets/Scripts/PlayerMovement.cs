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
    [Range(0.1f, 20)]
    private float speed = 12f;
    private float speedInicial;
    [SerializeField]
    [Range(0.1f, 20)]
    private float Sprintspeed = 5f;
    private bool isRunning;
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
    bool isGrounded;
    bool isWallD;
    bool isWallE;
    bool isWallFoward;
    public Transform WallCheck;
    public float WallDistance = 0.4f;
    public LayerMask wallMask;
    private bool iswallclimbing = false;
    private bool iswallrunning = false;
    private bool flagwallrunning = false;
    private bool flagwallclimbing = false;

    private void Start()
    {
        //StartCoroutine(stopbhop());
        speedInicial = speed;
    }
    private void Update()
    {

        if (isLocalPlayer)
        {
            isGrounded = CheckGround();
            isWallD = CheckWallD();
            isWallE = CheckWallE();
            isWallFoward = CheckWallFrente();
            Debug.Log("Is grounded: " + isGrounded);
            Debug.Log("Is wallD: " + isWallD);
            Debug.Log("Is WallE: " + isWallE);
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
            // Debug.Log("Speed:" + speed);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            //Parkour andar na parede

            //----------------------------------------------------------------------------------------------------------
            if (isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    Debug.Log("Saltou");
                    velocity.y = Mathf.Sqrt(jumHeight * -2f * gravity);
                    var vn = velocity;
                    vn = vn.normalized;
                    velocity += new Vector3(vn.x * 2f, 0, vn.z * 2f);
                }
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    isRunning = true;
                }
                if (velocity.y==0)
                {
                    velocity.y = -1f;
                }
                iswallclimbing = false;

            }
            else if (!isGrounded)
            {
                if ((isWallD || isWallE) && !iswallclimbing)
                {
                    iswallrunning = true;
                }
                if (isWallFoward)
                {
                    iswallclimbing = true;
                }
            }
            //----------------------------------------------------------------------------------------------------------
            if (isWallD)
            {

            }
            else if (!isWallD)
            {
                if (!isWallE)
                {
                    iswallrunning = false;
                    flagwallrunning = false;
                    
                }
            }
            //----------------------------------------------------------------------------------------------------------
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isRunning = false;

            }
            //----------------------------------------------------------------------------------------------------------
            if (isRunning)
            {
                speed = speedInicial + Sprintspeed;
            }
            else if (!isRunning)
            {
                speed = speedInicial;
            }
            //----------------------------------------------------------------------------------------------------------
            if (!iswallrunning)
            {
                velocity.y += gravity * Time.deltaTime;
                controller.Move(velocity * Time.deltaTime);
            }
            else  if (iswallrunning)
            {

                float step = 0.0001f;

                if (flagwallrunning == false)
                {
                    //Debug.Log(velocity.y);
                    velocity.y = 100f + velocity.y;
                    //Debug.Log(velocity.y);
                    flagwallrunning = true;

                }

                velocity.y = velocity.y + step;
                step = step + 0.001f;
                

            }
            //----------------------------------------------------------------------------------------------------------
            if (iswallclimbing)
            {
                float step = 0.0001f;

                if (flagwallclimbing == false)
                {
                    //Debug.Log(velocity.y);
                    velocity.y = 999f + velocity.y;
                    //Debug.Log(velocity.y);
                    flagwallclimbing = true;

                }

                velocity.y = velocity.y + step;
                step = step + 0.001f;
            }
            Debug.Log("IsWallclimbing: " + iswallclimbing);
        }
        else
        {
            playercam.enabled = false;
        }
        //Debug.Log("Is wallrunning:" + iswallrunning);
    }
    //private IEnumerator stopbhop()
    //{
    //    if (isGrounded)
    //        velocity = Vector3.zero;

    //    yield return new WaitForSeconds(0.3f);
    //    StartCoroutine(stopbhop());
    //}


    public bool CheckGround()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.up * -groundDistance, Color.blue);
        return Physics.Raycast(transform.position, transform.up * -1, out hit, 2, groundMask);
    }

    public bool CheckWallD()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.right * WallDistance, Color.green);
        return Physics.Raycast(transform.position, transform.right, out hit, 1, wallMask);
    }
    public bool CheckWallE()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.right * -WallDistance, Color.black);
        return Physics.Raycast(transform.position, transform.right * -1, out hit, 1, wallMask);
    }
    public bool CheckWallFrente()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * WallDistance, Color.green);
        return Physics.Raycast(transform.position, transform.right, out hit, 1, wallMask);
    }
}
