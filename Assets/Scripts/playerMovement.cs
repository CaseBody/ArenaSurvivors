using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Photon.Pun;

public class playerMovement : MonoBehaviour
{
    private Rigidbody rigidbody;
    //private PhotonView view;
    private bool wIsPressed;
    private bool aIsPressed;
    private bool sIsPressed;
    private bool dIsPressed;
    public float runSpeed = 7.5f;

    //camera
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        //view = GetComponent<PhotonView>();
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

        //camera
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        Plane p = new Plane(Vector3.up, transform.position);
        if (p.Raycast(mouseRay, out float hitDist))
        {
            Vector3 hitPoint = mouseRay.GetPoint(hitDist);
            transform.LookAt(hitPoint);
        }
    }

        private void FixedUpdate()
    {
        //check if w,a,s,d aren't pressed, stop without sliding
        if (wIsPressed == false && aIsPressed == false && sIsPressed == false && dIsPressed == false)
        {
            rigidbody.velocity = Vector3.zero;
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
    }
}
