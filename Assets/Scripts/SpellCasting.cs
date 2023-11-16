using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCasting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject fireballPF;
    public GameObject IceSpikePF;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootFire();
        } else if (Input.GetButtonDown("Fire2"))
        {
            ShootIce();
        }
    }

    /// <summary>
    /// Fires each type of spell
    /// </summary>    
    void ShootFire()
    {
        Instantiate(fireballPF, firePoint.position, firePoint.rotation);
    }
    void ShootIce()
    {
        Instantiate(IceSpikePF, firePoint.position, firePoint.rotation);
    }
}
