using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    //private Rigidbody rigidbody;
    //collisions
    Collider _collider;
    RaycastHit _hit;
    private bool isTouching;

    void Start()
    {
        _collider = GetComponent<Collider>();
    }

    void Update()
    {
        
    }
}
