using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using Photon.Pun;

public class playerMovement : MonoBehaviour
{
    private Rigidbody rigidbody;
    private PhotonView view;
    private bool wIsPressed;
    private bool aIsPressed;
    private bool sIsPressed;
    private bool dIsPressed;
    public float runSpeed = 7.5f;

    public Animator anim;

    //camera
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        cam = Camera.main;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            wIsPressed = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            aIsPressed = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            sIsPressed = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dIsPressed = true;
        }

        if (view.IsMine)
        {
            //camera
            Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
            Plane p = new Plane(Vector3.up, transform.position);
            if (p.Raycast(mouseRay, out float hitDist))
            {
                Vector3 hitPoint = mouseRay.GetPoint(hitDist);
                transform.LookAt(hitPoint);
            }
        }
    }

    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            //Anim
            CheckAnim();

            // Movement
            if (wIsPressed)
            {
                transform.position += Vector3.forward * Time.deltaTime * runSpeed;
                wIsPressed = false;
            }

            if (aIsPressed)
            {
                transform.position += Vector3.left * Time.deltaTime * runSpeed;
                aIsPressed = false;
            }

            if (sIsPressed)
            {
                transform.position += Vector3.back * Time.deltaTime * runSpeed;
                sIsPressed = false;
            }

            if (dIsPressed)
            {
                transform.position += Vector3.right * Time.deltaTime * runSpeed;
                dIsPressed = false;
            }
        }
    }

    private void CheckAnim()
    {
        if (wIsPressed && !sIsPressed)
        {

            if (transform.localRotation.y > 0.30 && transform.localRotation.y < 0.93)
            {
                anim.SetInteger("Anim", 4);
            }
            else if (transform.localRotation.y < -0.30)
            {
                anim.SetInteger("Anim", 3);
            }
            else if (transform.localRotation.y > 0.93 && transform.localRotation.y < 1.1)
            {
                anim.SetInteger("Anim", 2);
            }
            else
            {
                anim.SetInteger("Anim", 1);
            }
        }
        else if (sIsPressed && !wIsPressed)
        {
            if (transform.localRotation.y > 0.30 && transform.localRotation.y < 0.93)
            {
                anim.SetInteger("Anim", 3);
            }
            else if (transform.localRotation.y < -0.30)
            {
                anim.SetInteger("Anim", 4);
            }
            else if (transform.localRotation.y > 0.93 && transform.localRotation.y < 1.1)
            {
                anim.SetInteger("Anim", 1);
            }
            else
            {
                anim.SetInteger("Anim", 2);
            }
        }
        else if (aIsPressed && !dIsPressed)
        {
            if (transform.localRotation.y > 0.30 && transform.localRotation.y < 0.93)
            {
                anim.SetInteger("Anim", 2);
            }
            else if (transform.localRotation.y < -0.30)
            {
                anim.SetInteger("Anim", 1);
            }
            else if (transform.localRotation.y > 0.93 && transform.localRotation.y < 1.1)
            {
                anim.SetInteger("Anim", 3);
            }
            else
            {
                anim.SetInteger("Anim", 4);
            }
        }
        else if (dIsPressed && !sIsPressed)
        {
            if (transform.localRotation.y > 0.30 && transform.localRotation.y < 0.93)
            {
                anim.SetInteger("Anim", 1);
            }
            else if (transform.localRotation.y < -0.30)
            {
                anim.SetInteger("Anim", 2);
            }
            else if (transform.localRotation.y > 0.93 && transform.localRotation.y < 1.1)
            {
                anim.SetInteger("Anim", 4);
            }
            else
            {
                anim.SetInteger("Anim", 3);
            }
        }
        else
        {
            anim.SetInteger("Anim", 0);
        }

    }
}
