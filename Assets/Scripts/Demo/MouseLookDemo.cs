using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookDemo : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    private float xRotation = 0f;
    public GameObject playergameobject;
    private PlayerMovement playermovent;
    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playermovent = playergameobject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    private void Update()
    {        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, transform.localEulerAngles.z);

        if (playermovent.CheckWallD() && !playermovent.CheckGround())
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, 20f), Time.deltaTime * 1f);

            //transform.localRotation = Quaternion.Euler(xRotation, 0f, 10f);
        }
        else if (playermovent.CheckWallE() && !playermovent.CheckGround())
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, -20f), Time.deltaTime * 1f);
            //transform.localRotation = Quaternion.Euler(xRotation, 0f, -10f);
        }
        else if (!playermovent.CheckWallE() && !playermovent.CheckWallD() && playermovent.CheckGround() && (transform.localEulerAngles.z != 0))
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
