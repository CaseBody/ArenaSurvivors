using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using Photon.Pun;

public class playerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private PhotonView view;
    //movement
    private bool wIsPressed;
    private bool aIsPressed;
    private bool sIsPressed;
    private bool dIsPressed;
    public float runSpeed = 6;
    //dash
    Vector3 dashPosition;
    private bool spaceIsPressed;
    private bool isDashing;
    private int dashTeller;
    private int dashCooldown;
    public float dashSpeed = 10f;

    public Animator anim;

    //camera
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        cam = Camera.main;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
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
            //Anim
            CheckAnim();

            if (!wIsPressed && !sIsPressed && !dIsPressed && !aIsPressed && !isDashing)
            {
                rb.velocity = Vector3.zero;
            }

            if(wIsPressed && aIsPressed)
            {
                if (!isDashing)
                {
                    rb.velocity = new Vector3((runSpeed * -1) * 0.7f, 0, runSpeed * 0.7f);
                }
                wIsPressed = false;
                aIsPressed = false;
            }
            if (wIsPressed && dIsPressed)
            {
                if (!isDashing)
                    rb.velocity = new Vector3(runSpeed * 0.7f, 0, runSpeed * 0.7f);
                wIsPressed = false;
                dIsPressed = false;
            }
            if (sIsPressed && aIsPressed)
            {
                if (!isDashing)
                    rb.velocity = new Vector3((runSpeed * -1) * 0.7f, 0, (runSpeed * -1) * 0.7f);
                sIsPressed = false;
                aIsPressed = false;
            }
            if (sIsPressed && dIsPressed)
            {
                if (!isDashing)
                    rb.velocity = new Vector3(runSpeed * 0.7f, 0, (runSpeed * -1) * 0.7f);
                sIsPressed = false;
                dIsPressed = false;
            }

            // Movement
            if (wIsPressed)
            {
                if (!isDashing)
                    rb.velocity = Vector3.forward * runSpeed;
                wIsPressed = false;
            }

            if (aIsPressed)
            {
                if (!isDashing)
                    rb.velocity = Vector3.left * runSpeed;
                aIsPressed = false;
            }

            if (sIsPressed)
            {
                if (!isDashing)
                    rb.velocity = Vector3.back * runSpeed;
                sIsPressed = false;
            }

            if (dIsPressed)
            {
                if (!isDashing)
                    rb.velocity = Vector3.right * runSpeed;
                dIsPressed = false;
            }

            if (spaceIsPressed && !isDashing && dashCooldown == 0)
            {
                Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
                Plane p = new Plane(Vector3.up, rb.transform.position);
                if (p.Raycast(mouseRay, out float hitDist))
                {
                    Vector3 hitPoint = mouseRay.GetPoint(hitDist);
                    dashPosition = hitPoint;
                    dashTeller = 10;
                    isDashing = true;
                    rb.velocity = Vector3.zero;
                    dashCooldown = 500;
                }
            }

            if (spaceIsPressed)
            {
                spaceIsPressed = false;
            }

            if (isDashing)
            {
                Debug.Log(dashPosition);

                if (dashTeller > 0)
                {
                    dashTeller--;
                    rb.MovePosition(Vector3.MoveTowards(rb.position, new Vector3(dashPosition.x, rb.position.y, dashPosition.z), 0.8f));
                }
                else
                {
                    isDashing = false;
                }
            }

            if (dashCooldown > 0)
            {
                dashCooldown--;
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
