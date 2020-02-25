using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : NetworkBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    private float xRotation = 0f;
    public GameObject playergameobject;
    private PlayerMovement playermovent;
    [SerializeField]
    private NetworkIdentity player;
    // Start is called before the first frame update
    private void Start()
    {
        
        if (!player.isLocalPlayer)
        {
            return;
        }
        Cursor.lockState = CursorLockMode.Locked;
        playermovent = playergameobject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!player.isLocalPlayer)
        {
            return;
        }
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);      

        transform.localRotation = Quaternion.Euler(xRotation, 0f, transform.eulerAngles.z);
        
        if (playermovent.CheckWallD() && !playermovent.CheckGround())
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 10f), Time.deltaTime * 1f);

            //transform.localRotation = Quaternion.Euler(xRotation, 0f, 10f);
        }
        else if (playermovent.CheckWallE() && !playermovent.CheckGround())
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -10f), Time.deltaTime * 1f);
            //transform.localRotation = Quaternion.Euler(xRotation, 0f, -10f);
        }
        else if (!playermovent.CheckWallE() && !playermovent.CheckWallD() && playermovent.CheckGround() && transform.eulerAngles.z !=0f)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0f), Time.deltaTime * 3f);
        }
        
        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
