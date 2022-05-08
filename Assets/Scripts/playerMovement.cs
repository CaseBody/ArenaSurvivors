using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Photon.Pun;

public class playerMovement : MonoBehaviour
{
    private Rigidbody rigidbody;
    private PhotonView view;
    //movement
    private bool wIsPressed;
    private bool aIsPressed;
    private bool sIsPressed;
    private bool dIsPressed;
    public float runSpeed = 7.5f;
    public float schuineRunSpeed = 7.5f;
    //dash
    Vector3 dashPosition;
    private bool spaceIsPressed;
    private bool isDashing;
    private int dashTeller;
    public float dashSpeed = 10f;

    //camera
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //movement
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
        //dash
        if (Input.GetKey(KeyCode.Space))
        {
            spaceIsPressed = true;
        }

        //camera
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
        //movement
        if (view.IsMine)
        {
            if(wIsPressed && aIsPressed)
            {
                runSpeed = schuineRunSpeed;
            }
            if (wIsPressed && dIsPressed)
            {
                runSpeed = schuineRunSpeed;
            }
            if (sIsPressed && aIsPressed)
            {
                runSpeed = schuineRunSpeed;
            }
            if (sIsPressed && dIsPressed)
            {
                runSpeed = schuineRunSpeed;
            }

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

            Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
            Plane p = new Plane(Vector3.up, transform.position);
            if (p.Raycast(mouseRay, out float hitDist))
            {
                Vector3 hitPoint = mouseRay.GetPoint(hitDist);
                if (spaceIsPressed && !isDashing)
                {
                    dashPosition = hitPoint;
                    dashTeller = 100;
                    isDashing = true;
                }
                if (isDashing)
                {
                    if (dashTeller > 0)
                    {
                        dashTeller--;
                        //transform.MoveTowards(new Vector3(dashPosition.x, transform.position.y, dashPosition.y) * runSpeed);
                        transform.position = Vector3.MoveTowards(dashPosition.normalized, transform.position, dashPosition.z * runSpeed);
                    }
                    else
                    {
                        isDashing = false;
                    }
                }
                //Debug.Log(hitPoint);
            }
        }
    }
}

//transform.MoveTowards = new Vector3(Input.mousePosition.x, transform.position.y, Input.mousePosition.z);

//Vector3 dashPosition;
//private bool spaceIsPressed;
//private bool isDashing;
//private int dashTeller;
//public float dashSpeed = 10f;
