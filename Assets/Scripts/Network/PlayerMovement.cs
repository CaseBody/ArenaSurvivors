using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class PlayerMovement2 : MonoBehaviour
{
    private bool leftPressed;
    private bool rightPressed;
    private bool upPressed;
    private bool downPressed;
    private Rigidbody rigidbody;
    private PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            upPressed = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            leftPressed = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            downPressed = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rightPressed = true;
        }
    }

    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            if (upPressed)
            {
                upPressed = false;

                rigidbody.AddForce(transform.forward * 11, ForceMode.Force);
            }

            if (downPressed)
            {
                downPressed = false;

                rigidbody.AddForce((transform.forward * 11) * -1, ForceMode.Force);
            }

            if (rightPressed)
            {
                rightPressed = false;

                rigidbody.AddForce(transform.right * 11, ForceMode.Force);
            }

            if (leftPressed)
            {
                leftPressed = false;

                rigidbody.AddForce((transform.right * 11) * -1, ForceMode.Force);
            }
        }
    }
}
