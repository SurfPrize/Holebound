using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    //[SerializeField] float movespeed = 6;
    //[SerializeField] float jumpheight = 2;
    //[SerializeField] float gravity = 20;
    //private bool flag=false;
    //private bool Estanaparede;

    //[Range(0, 10), SerializeField] float airControl = 5;

    //Vector3 moveDirection = Vector3.zero;
    //CharacterController controller;



    //// Start is called before the first frame update
    //void Start()
    //{
    //    controller = GetComponent<CharacterController>();
    //}


    //private void FixedUpdate()
    //{

    //    var input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    //    input *= movespeed;
    //    input = transform.TransformDirection(input);

    //    if (controller.isGrounded)
    //    {
    //        moveDirection = input;

    //        if (Input.GetButton("Jump"))
    //        {
    //            moveDirection.y = Mathf.Sqrt(2 * gravity * jumpheight);
    //        }

    //    }
    //    else
    //    {

    //        input.y = moveDirection.y;
    //        moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
    //    }

    //    if (Estanaparede==true)
    //    {
    //        //Debug.Log("Esta na parede");
    //        if (flag==false)
    //        {
    //            gravity = 0f;
    //            input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0 );
    //            input *= movespeed;
    //            input = transform.TransformDirection(input);
    //            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
    //            Debug.Log(gravity);
    //            flag = true;
    //        }
    //        //else if (flag==true)
    //        //{
    //        //    gravity = 3f;
    //        //    flag = true;
    //        //}



    //        input = transform.TransformDirection(input);
    //    }
    //    else if (Estanaparede == false)
    //    {
    //        flag = false;
    //        //Debug.Log("Saiu esta na parede");
    //        gravity = 20f;
    //        input = transform.TransformDirection(input);
    //    }
    //    moveDirection.y -= gravity * Time.deltaTime;

    //    controller.Move(moveDirection * Time.deltaTime);
    //}


    //// Update is called once per frame
    //void Update()
    //{

    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Parede")
    //    {
    //        //Debug.Log("dawd");
    //        Estanaparede = true;
    //    }

    //}
    //private void OnTriggerExit(Collider other)
    //{

    //        Estanaparede = false;

    //}


    [SerializeField] float movespeed = 6;
    [SerializeField] float jumpheight = 2;
    public float gravity = 20;
    private bool flag = false;
    private bool Estanaparede;

    [Range(0, 10), SerializeField] float airControl = 5;

    Vector3 moveDirection = Vector3.zero;
    CharacterController controller;
    Vector3 input;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    private void FixedUpdate()
    {        
        if (Estanaparede == true)
        {           
            if (flag == false)
            {
                gravity = 1f;
                input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
                input *= movespeed;
                input = transform.TransformDirection(input);
                moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
                Debug.Log(gravity);
                flag = true;
            }
            //else if (flag==true)
            //{
            //    gravity = 3f;
            //    flag = true;
            //}



  
        }
        else if (Estanaparede == false)
        {
            input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            input *= movespeed;
            input = transform.TransformDirection(input);

            if (controller.isGrounded)
            {
                moveDirection = input;

                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = Mathf.Sqrt(8 * gravity * jumpheight);
                }

            }
            else
            {

                input.y = moveDirection.y;
                moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
            }


            flag = false;
            //Debug.Log("Saiu esta na parede");
            gravity = 20f;
            input = transform.TransformDirection(input);
        }
        moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);
    }


    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Parede")
        {
            //Debug.Log("dawd");
            Estanaparede = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {

        Estanaparede = false;

    }





}
