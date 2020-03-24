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
    public Estadoplayer player_state { get => current_state; }

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
    public Transform WallCheck;
    public float WallDistance = 0.4f;
    public LayerMask wallMask;
    private bool flagwallrunning = false;
    private bool flagwallclimbing = false;
    private bool jumping = false;
    private Vector3 move;
    private bool jumped = false;
    private bool jumpedtwice = false;
    private bool dashed = false;
    public float speeddash = 20f;
    private bool dashing = false;
    private float journeyLength;

    private Controlosps4 Inputc;

    //um Handle serve para que sempre que uma variavel mude de valor faca um metodo
    //sempre que o input do analogico muda de valor, chama o HandleMove
    private void OnEnable()
    {
        Inputc = new Controlosps4();
        Inputc.PlayercontrolsPS4.Mover.performed += Handlemove;
        Inputc.PlayercontrolsPS4.Jump.performed += HandleJump;
        Inputc.PlayercontrolsPS4.Run.performed += HandleRun;
        Inputc.PlayercontrolsPS4.Dash.performed += HandleDash;

        //por alguma razao tens de se dar enable primeiro, e um bocado estupido
        Inputc.PlayercontrolsPS4.Mover.Enable();
        Inputc.PlayercontrolsPS4.Jump.Enable();
        Inputc.PlayercontrolsPS4.Run.Enable();
        Inputc.PlayercontrolsPS4.Dash.Enable();
    }

    //Isto sao coisas para nao dar memory leak
    private void OnDisable()
    {
        Inputc.PlayercontrolsPS4.Mover.performed -= Handlemove;
        Inputc.PlayercontrolsPS4.Jump.performed -= HandleJump;
        Inputc.PlayercontrolsPS4.Run.performed -= HandleRun;
        Inputc.PlayercontrolsPS4.Dash.performed -= HandleDash;
        Inputc.PlayercontrolsPS4.Mover.Disable();
        Inputc.PlayercontrolsPS4.Jump.Disable();
        Inputc.PlayercontrolsPS4.Run.Disable();
        Inputc.PlayercontrolsPS4.Dash.Disable();
    }

    //isto chama sempre que o botao e premido e largado(2x no total, na primeira da toggle on e na 2a toggle off
    private void HandleRun(InputAction.CallbackContext context)
    {
        Debug.Log("IS RUNNING");
        if (speed == speedInicial)
            speed = speedInicial + Runspeed;
        else
            speed = speedInicial;
    }

    //sempre que clicas no botao de salto
    //quando fores fazer o double jump, cria outro enum, estadoplayer.airborn_and_doublejumped ou algo do genero
    private void HandleJump(InputAction.CallbackContext obj)
    {
        if (current_state == Estadoplayer.GROUNDED || current_state == Estadoplayer.RUN || (current_state == Estadoplayer.AIRBORNE && jumpedtwice == false))
        {
            if (jumped == true && jumpedtwice == false)
            {
                Debug.Log("jumped");
                velocity.y = Mathf.Sqrt(jumHeight * -2f * gravity);
                var vn = velocity;
                vn = vn.normalized;
                velocity += new Vector3(vn.x * 2f, 0, vn.z * 2f);
                current_state = Estadoplayer.AIRBORNE;
                StartCoroutine(jumpcd());
                jumpedtwice = true;
            }
            if (jumped == false)
            {
                Debug.Log("jumped");
                velocity.y = Mathf.Sqrt(jumHeight * -2f * gravity);
                var vn = velocity;
                vn = vn.normalized;
                velocity += new Vector3(vn.x * 2f, 0, vn.z * 2f);
                current_state = Estadoplayer.AIRBORNE;
                StartCoroutine(jumpcd());
                jumped = true;
            }


        }
    }
    private void HandleDash(InputAction.CallbackContext obj)
    {
        if (dashed==false)
        {
            Debug.Log("Dash Antes");
            var movee = move.normalized;
            
            transform.position = Vector3.Lerp(transform.position, transform.position + (movee * 400), 4f);
            dashed = true;
            StartCoroutine(Dashcd());
            Debug.Log("Dash Depois");
        }



    }

    private IEnumerator jumpcd()
    {
        jumping = true;
        yield return new WaitForSeconds(0.5f);
        jumping = false;
    }

    private IEnumerator Dashcd()
    {
        dashing = true;
        yield return new WaitForSeconds(2f);
        dashing = false;
    }

    //sempre que o analogico muda de posicao
    private void Handlemove(InputAction.CallbackContext context)
    {
        float x = context.ReadValue<Vector2>().x;
        float y = context.ReadValue<Vector2>().y;

        move = transform.right * x + transform.forward * y;
        //Debug.Log(move + " x:" + x + " y" + y);
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
            //isto são os raycasts, vê já o que ta la em baixo que assim ler o resto vai ser mais facil
            check_state();
            float step = 0.0001f;
            Debug.Log(current_state);
            Debug.Log("velocity y" + velocity.y);
            //isto e para dar resolver o estado do jogador

            switch (current_state)
            {
                case Estadoplayer.GROUNDED:
                    jumpedtwice = false;
                    jumped = false;
                    dashed = false;
                    if (!jumping)
                        velocity.y = -1f;
                    else
                        controller.Move(velocity * Time.deltaTime);

                    controller.Move(move * speed * Time.deltaTime);
                    break;
                case Estadoplayer.RUN:
                    //é praticamente igual ao walk, ams se depois tivermos animacoes de correr e so meter aqui
                    if (!jumping)
                        velocity.y = -1f;
                    else
                        controller.Move(velocity * Time.deltaTime);

                    controller.Move(move * speed * Time.deltaTime);
                    break;
                case Estadoplayer.WRUNNING:
                    //WRUNNING é wall running logicamente
                    if (flagwallrunning == false)
                    {
                        velocity.y = 100f + velocity.y;
                        flagwallrunning = true;
                    }

                    velocity.y = velocity.y + step;
                    step = step + 0.001f;
                    break;
                case Estadoplayer.WCLIMBING:
                    //WCLIMBING é wall climbing
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
                    controller.Move(velocity * Time.deltaTime);
                    controller.Move(move * speed * Time.deltaTime);
                    break;
                default:
                    Debug.LogError("JOGADOR EM NENHUM  ESTADO");
                    //logicamente que op jogador tem de estar num estado
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

        //os teus queridos raycasts, em else ifs para passar a frente se encontrar o estado do jogador
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
        return Physics.Raycast(transform.position, transform.up * -1, out hit, 4, groundMask);
    }

    public bool CheckWallD()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.right * WallDistance, Color.green);
        return Physics.Raycast(transform.position, transform.right, out hit, 4, wallMask);
    }
    public bool CheckWallE()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.right * -WallDistance, Color.black);
        return Physics.Raycast(transform.position, transform.right * -1, out hit, 4, wallMask);
    }
    public bool CheckWallFrente()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * WallDistance, Color.green);
        return Physics.Raycast(transform.position, transform.right, out hit, 4, wallMask);
    }
}
