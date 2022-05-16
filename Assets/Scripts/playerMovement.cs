using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using Photon.Pun;

public class playerMovement : MonoBehaviour
{
    private PhotonView view;
    //movement
    private bool wIsPressed;
    private bool aIsPressed;
    private bool sIsPressed;
    private bool dIsPressed;
    public float runSpeed = 7.5f;
    //dash
    Vector3 dashPosition;
    private bool spaceIsPressed;
    private bool isDashing;
    private int dashTeller;
    private int dashCooldown;
    public float dashSpeed = 20f;

    //attack
    private bool mouseIsPressed;
    private bool isAttacking;
    private bool hasHit;
    private float attackCooldown;
    public WeaponRange attackRangeManager;
    private BoxCollider attackRange;
    public int attackTime;

    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject weapon;

    List<GameObject> currentCollisions = new List<GameObject>();


    public Animator anim;

    //camera
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        cam = Camera.main;
        anim = GetComponent<Animator>();
        hasHit = false;
        attackRange = attackRangeManager.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon == null)
        {
            AddWeapon("2h Sword");
        }

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
        if (Input.GetMouseButton(0))
        {
            mouseIsPressed = true;
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
        
        //movement
        if (view.IsMine)
        {
            //Anim
            CheckAnim();

            //Diagonal
            if (wIsPressed && aIsPressed)
            {
                transform.position += Vector3.forward * Time.deltaTime * (runSpeed * 0.7f);
                transform.position += Vector3.left * Time.deltaTime * (runSpeed * 0.7f);
                wIsPressed = false;
                aIsPressed = false;
            }
            if (wIsPressed && dIsPressed)
            {
                transform.position += Vector3.forward * Time.deltaTime * (runSpeed * 0.7f);
                transform.position += Vector3.right * Time.deltaTime * (runSpeed * 0.7f);
                wIsPressed = false;
                dIsPressed = false;
            }
            if (sIsPressed && aIsPressed)
            {
                transform.position += Vector3.back * Time.deltaTime * (runSpeed * 0.7f);
                transform.position += Vector3.left * Time.deltaTime * (runSpeed * 0.7f);
                sIsPressed = false;
                aIsPressed = false;
            }
            if (sIsPressed && dIsPressed)
            {
                transform.position += Vector3.back * Time.deltaTime * (runSpeed * 0.7f);
                transform.position += Vector3.right * Time.deltaTime * (runSpeed * 0.7f);
                sIsPressed = false;
                dIsPressed = false;
            }

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


            //Dash
            if (spaceIsPressed && !isDashing && dashCooldown == 0)
            {
                Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
                Plane p = new Plane(Vector3.up, transform.position);
                if (p.Raycast(mouseRay, out float hitDist))
                {
                    Vector3 hitPoint = mouseRay.GetPoint(hitDist);
                    transform.LookAt(hitPoint);
                    dashPosition = hitPoint;
                    dashTeller = 100;
                    isDashing = true;
                    dashCooldown = 500;
                }
            }

            if (spaceIsPressed)
            {
                spaceIsPressed = false;
            }


            if (isDashing)
            {
                if (dashTeller > 0)
                {
                    dashTeller--;
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(dashPosition.x, transform.position.y, dashPosition.z), dashSpeed * Time.deltaTime);
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

            if (mouseIsPressed && attackCooldown == 0)
            {
                isAttacking = true;
                hasHit = false;
                attackCooldown = 1;
            }

            if (isAttacking)
            {
                attackCooldown -= 1 * Time.deltaTime;

                if (hasHit == false && attackCooldown <= ((attackCooldown / 100) * 25))
                {
                    Hit();
                }

                if (attackCooldown <= 0)
                {
                    attackCooldown = 0;
                    isAttacking = false;
                    anim.SetInteger("Attack", 0);
                }
            }

            mouseIsPressed = false;
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

        if (isAttacking)
        {
            anim.SetInteger("Attack", 1);
        }

    }

    public void AddWeapon(string name)
    {
        weapon = PhotonNetwork.Instantiate(name, transform.position, Quaternion.identity);

        var script = weapon.GetComponent<WeaponScript>();
        
        if (script.attachLeft)
        {
            weapon.transform.position = leftHand.transform.position;
            weapon.transform.rotation = leftHand.transform.rotation;
            weapon.transform.parent = leftHand.transform;
        }
        else
        {
            weapon.transform.position = rightHand.transform.position;
            weapon.transform.rotation = rightHand.transform.rotation;
            weapon.transform.parent = rightHand.transform;
        }

        if (script.weaponRange > 1)
        {
            attackRange.size = new Vector3(attackRange.size.x, attackRange.size.y, script.weaponRange);
            attackRange.center = new Vector3(attackRange.center.x, attackRange.center.y, script.weaponRange / 4);
        }
        else
        {
            attackRange.size = new Vector3(attackRange.size.x, attackRange.size.y, script.weaponRange);
            attackRange.center = new Vector3(attackRange.center.x, attackRange.center.y, 0);
        }

        anim.runtimeAnimatorController = script.animator as RuntimeAnimatorController;
    }

    public void Hit()
    {
        attackRangeManager.Hit(weapon.GetComponent<WeaponScript>().damage);
    }
}
