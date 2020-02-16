using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouselook : MonoBehaviour
{
    //sensiblidade rato
    [SerializeField] float turnspeed=90f;
    // quanto alto consegue olhar angulo
    [SerializeField] float headUpperAngleLimit = 85f;
    //quanto baixo consegue olhar angulo
    [SerializeField] float headLowerAngleLimit = -80f;

    float yaw = 0f;
    float pitch = 0f;

    Quaternion bodyStartOrientation;
    Quaternion headStartOrientation;

    Transform head;




    void Start()
    {
        head = GetComponentInChildren<Camera>().transform;

        bodyStartOrientation = transform.localRotation;
        headStartOrientation = head.transform.localRotation;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        var horizontal = Input.GetAxis("Mouse X") * Time.deltaTime * turnspeed;
        var vertical = Input.GetAxis("Mouse Y") * Time.deltaTime * turnspeed;

        yaw += horizontal;
        pitch += vertical;
        pitch = Mathf.Clamp(pitch, headLowerAngleLimit, headUpperAngleLimit);

        var bodyrotation = Quaternion.AngleAxis(yaw, Vector3.up);
        var headrotation = Quaternion.AngleAxis(pitch, Vector3.left);

        

        transform.localRotation = bodyrotation * bodyStartOrientation;
        head.localRotation = headrotation * headStartOrientation;

        
    }

   
}
