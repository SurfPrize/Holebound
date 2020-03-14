using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour
{
    public enum Estadoplayer
    {
        GROUNDED,
        AIRBORNE,
        RUN,
        WRUNNING,
        WCLIMBING
    }

    public enum Wallpos
    {
        WALL_RIGHT,
        WALL_LEFT,
        NONE
    }
    private Wallpos _current_wall;
    private Estadoplayer current_state;
    public  Estadoplayer player_state { get => current_state; }

    public Wallpos current_wall
    {
        get
        {
            if (current_state != Estadoplayer.WRUNNING)
            {
                return Wallpos.NONE;
            }
            else
            {
                return _current_wall;
            }
        }
        set
        {
            _current_wall = value;
        }
    }
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
    private float Runspeed = 5f;
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
    private Vector3 velocity;
    private bool isGrounded;
    private bool isWallD;
    private bool isWallE;
    private bool isWallFoward;
    public Transform WallCheck;
    public float WallDistance = 0.4f;
    public LayerMask wallMask;
    private bool iswallclimbing = false;
    private bool iswallrunning = false;
    private bool flagwallrunning = false;
    private bool flagwallclimbing = false;

    private Controlosps4 Inputc;

    private void OnEnable()
    {
        Inputc = new Controlosps4();
        Inputc.PlayercontrolsPS4.Mover.performed += Handlemove;
        Inputc.PlayercontrolsPS4.Jump.performed += HandleJump;
        Inputc.PlayercontrolsPS4.Run.performed += HandleRun;
        Inputc.PlayercontrolsPS4.Mover.Enable();
        Inputc.PlayercontrolsPS4.Jump.Enable();
        Inputc.PlayercontrolsPS4.Run.Enable();
    }

    private void OnDisable()
    {
        Inputc.PlayercontrolsPS4.Mover.performed -= Handlemove;
        Inputc.PlayercontrolsPS4.Jump.performed -= HandleJump;
        Inputc.PlayercontrolsPS4.Run.performed -= HandleRun;
        Inputc.PlayercontrolsPS4.Mover.Disable();
        Inputc.PlayercontrolsPS4.Jump.Disable();
        Inputc.PlayercontrolsPS4.Run.Disable();
    }

    private void HandleRun(InputAction.CallbackContext obj)
    {
        if (current_state == Estadoplayer.RUN)
        {
            speed = speedInicial + Runspeed;
        }
        else
        {
            speed = speedInicial;
        }
    }

    private void HandleJump(InputAction.CallbackContext obj)
    {
        if (current_state == Estadoplayer.GROUNDED)
        {
            velocity.y = Mathf.Sqrt(jumHeight * -2f * gravity);
            var vn = velocity;
            vn = vn.normalized;
            velocity += new Vector3(vn.x * 2f, 0, vn.z * 2f);
            current_state = Estadoplayer.AIRBORNE;
        }
    }

    private void Handlemove(InputAction.CallbackContext context)
    {
        float x = context.ReadValue<Vector2>().x;
        float z = context.ReadValue<Vector2>().y;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        // Debug.Log("Speed:" + speed);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }


    private void Start()
    {
        //StartCoroutine(stopbhop());
        speedInicial = speed;
    }
    private void Update()
    {

        if (isLocalPlayer)
        {
            check_state();
            Debug.Log(current_state);
            float step = 0.0001f;

            //Parkour andar na parede
            switch (current_state)
            {
                case Estadoplayer.GROUNDED:
                    if (velocity.y == 0)
                    {
                        velocity.y = -1f;
                    }
                    velocity.y += gravity * Time.deltaTime;
                    controller.Move(velocity * Time.deltaTime);
                    break;
                case Estadoplayer.RUN:
                    
                    controller.Move(velocity * Time.deltaTime);
                    break;
                case Estadoplayer.WRUNNING:

                    if (flagwallrunning == false)
                    {
                        //Debug.Log(velocity.y);
                        velocity.y = 100f + velocity.y;
                        //Debug.Log(velocity.y);
                        flagwallrunning = true;
                    }

                    velocity.y = velocity.y + step;
                    step = step + 0.001f;
                    break;
                case Estadoplayer.WCLIMBING:

                    if (flagwallclimbing == false)
                    {
                        //Debug.Log(velocity.y);
                        velocity.y = 999f + velocity.y;
                        //Debug.Log(velocity.y);
                        flagwallclimbing = true;

                    }

                    velocity.y = velocity.y + step;
                    step = step + 0.001f;
                    break;
                case Estadoplayer.AIRBORNE:
                    velocity.y += gravity * Time.deltaTime;
                    break;
                default:
                    Debug.LogError("JOGADOR EM NENHUM  ESTADO");
                    break;
            }
            //----------------------------------------------------------------------------------------------------------
            //if (current_state==Estadoplayer.GROUNDED)
            //{

            //}
            //else if (!isGrounded)
            //{
            //    if ((isWallD || isWallE) && !iswallclimbing)
            //    {
            //        iswallrunning = true;
            //    }
            //    if (isWallFoward)
            //    {
            //        iswallclimbing = true;
            //    }
            //}
            //----------------------------------------------------------------------------------------------------------
            //if (isWallD)
            //{

            //}
            //else if (!isWallD)
            //{
            //    if (!isWallE)
            //    {
            //        iswallrunning = false;
            //        flagwallrunning = false;

            //    }
            //}
            //----------------------------------------------------------------------------------------------------------

            ////----------------------------------------------------------------------------------------------------------
            //if (isRunning)
            //{
            //    speed = speedInicial + Runspeed;
            //}
            //else if (!isRunning)
            //{
            //    speed = speedInicial;
            //}
            //----------------------------------------------------------------------------------------------------------
            //if (!iswallrunning)
            //{
            //    velocity.y += gravity * Time.deltaTime;
            //    controller.Move(velocity * Time.deltaTime);
            //}
            //else if (iswallrunning)
            //{

            //    float step = 0.0001f;

            //    if (flagwallrunning == false)
            //    {
            //        //Debug.Log(velocity.y);
            //        velocity.y = 100f + velocity.y;
            //        //Debug.Log(velocity.y);
            //        flagwallrunning = true;

            //    }

            //    velocity.y = velocity.y + step;
            //    step = step + 0.001f;


            //}
            //----------------------------------------------------------------------------------------------------------
            //if (iswallclimbing)
            //{
            //    float step = 0.0001f;

            //    if (flagwallclimbing == false)
            //    {
            //        //Debug.Log(velocity.y);
            //        velocity.y = 999f + velocity.y;
            //        //Debug.Log(velocity.y);
            //        flagwallclimbing = true;

            //    }

            //    velocity.y = velocity.y + step;
            //    step = step + 0.001f;
            //}
            //Debug.Log("IsWallclimbing: " + iswallclimbing);
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
    public void check_state()
    {
        isGrounded = CheckGround();
        isWallD = CheckWallD();
        isWallE = CheckWallE();
        isWallFoward = CheckWallFrente();

        if (CheckGround())
        {
            current_state = Estadoplayer.GROUNDED;
        }
        else if (CheckWallD())
        {
            current_state = Estadoplayer.WRUNNING;
            current_wall = Wallpos.WALL_RIGHT;
        }
        else if (CheckWallE())
        {
            current_state = Estadoplayer.WRUNNING;
            current_wall = Wallpos.WALL_LEFT;
        }
        else if (CheckWallFrente())
        {
            current_state = Estadoplayer.WCLIMBING;
        }
        else
        {
            current_state = Estadoplayer.AIRBORNE;
        }

    }


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
