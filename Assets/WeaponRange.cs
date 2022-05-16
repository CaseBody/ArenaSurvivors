using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRange : MonoBehaviour
{

    List<GameObject> currentCollisions = new List<GameObject>();

    public void Hit(int damage)
    {
        foreach (var col in currentCollisions)
        {
            var hp = col.GetComponent<HpManager>();

            if (hp != null)
            {
                // Logica om HP te verminderen komt hier.
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentCollisions.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        currentCollisions.Remove(collision.gameObject);
    }


}
