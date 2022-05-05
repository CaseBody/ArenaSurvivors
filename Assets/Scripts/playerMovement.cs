using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
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
}
