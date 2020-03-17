using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : NetworkBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    private float xRotation = 0f;
    public GameObject playergameobject;
    private PlayerMovement playermovement;
    [SerializeField]
    private NetworkIdentity player;
    private Controlosps4 Inputc;
    private Vector2 moveAxis;


    // Start is called before the first frame update

    //para dar enable aos controlos, um handle é um metodo que é feito sempre que uma variavel muda para um valor diferente do anterior
    private void OnEnable()
    {
        Inputc = new Controlosps4();
        Inputc.PlayercontrolsPS4.Olhar.performed += HandleMove;
        Inputc.PlayercontrolsPS4.Olhar.Enable();
    }

    //para nao dar memory leak
    private void OnDisable()
    {
        Inputc.PlayercontrolsPS4.Olhar.performed -= HandleMove;
        Inputc.PlayercontrolsPS4.Olhar.Disable();
    }

    //sempre que se mexe o analogico direito
    private void HandleMove(InputAction.CallbackContext context)
    {
        moveAxis = context.ReadValue<Vector2>();
        //nao mudei muito, aquilo da return a um x e y que equivalem ao x e y do analogico
    }

    private void Start()
    {

        if (!player.isLocalPlayer)
        {
            return;
        }
        playermovement = playergameobject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!player.isLocalPlayer)
        {
            return;
        }

        //dei invert ao Y para fazer mais sentido, pelo menos para mim, podes mudar se quiseres :S
        float mouseX = moveAxis.x * mouseSensitivity * Time.deltaTime;
        float mouseY = -moveAxis.y * mouseSensitivity * Time.deltaTime;


        //tive de mudar o clamp visto que nao tava a dar com esta cena, sorry ;_;
        Quaternion result = transform.localRotation * Quaternion.Euler(Vector3.right * mouseY);
        

        if (result.x > 0.7f)
        {
            transform.localRotation = new Quaternion(0.7f,transform.localRotation.y, transform.localRotation.z, transform.localRotation.w) ;
        }
        else if (result.x < -0.7f)
        {
            transform.localRotation = new Quaternion(-0.7f, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);
        }
        else
        {
            transform.localRotation = result;
        }

        //aqui ta tudo igual exepto que vai buscar a var current wall q da a parede do jogador
        float xRotation = transform.localRotation.x;

        if (playermovement.player_state==PlayerMovement.Estadoplayer.WRUNNING) {
            if (playermovement.current_wall==PlayerMovement.Wallpos.WALL_RIGHT)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, 20f), Time.deltaTime * 1f);

                //transform.localRotation = Quaternion.Euler(xRotation, 0f, 10f);
            }
            else if (playermovement.current_wall == PlayerMovement.Wallpos.WALL_LEFT)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, -20f), Time.deltaTime * 1f);
                //transform.localRotation = Quaternion.Euler(xRotation, 0f, -10f);
            }
        }
        else if ((playermovement.player_state==PlayerMovement.Estadoplayer.GROUNDED|| playermovement.player_state == PlayerMovement.Estadoplayer.AIRBORNE) && (transform.localEulerAngles.z != 0))
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f), Time.deltaTime);
            if (transform.localRotation.z < 0.01f && transform.localRotation.z > -0.01f)
            {
                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            }
        }
        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
