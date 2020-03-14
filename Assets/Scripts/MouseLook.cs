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

    private void OnEnable()
    {
        Inputc = new Controlosps4();
        Inputc.PlayercontrolsPS4.Olhar.performed += HandleMove;
        Inputc.PlayercontrolsPS4.Olhar.Enable();
    }

    private void OnDisable()
    {
        Inputc.PlayercontrolsPS4.Olhar.performed -= HandleMove;
        Inputc.PlayercontrolsPS4.Olhar.Disable();
    }

    private void HandleMove(InputAction.CallbackContext context)
    {
        moveAxis = context.ReadValue<Vector2>();
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

        float mouseX = moveAxis.x * mouseSensitivity * Time.deltaTime;
        float mouseY = -moveAxis.y * mouseSensitivity * Time.deltaTime;

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
