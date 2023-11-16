using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    //speed of the proyectile
    public float speed = 20f;
    public Rigidbody2D rb;
    [SerializeField] private float maxTime;
    private float actualTime;
    private bool isTimeActive;

    private void Start()
    {
        rb.velocity = transform.right * speed;
        ActivateCounter();
    }

     private void Update()
     {
         //ShootLeft();
        // ShootRight();
        if(isTimeActive)
        {
            ChangeCounter();
        }
     }

    /// <summary>
    /// Methods to makle the proyectiles disapear after a while
    /// </summary>

    private void ChangeCounter()
    {
        actualTime -= Time.deltaTime;
        if(actualTime <=0)
        {
            Destroy(gameObject);

            isTimeActive = false;
        }
    }
    private void ChangeTimer(bool status)
    {
        isTimeActive = status;
    }
    private void ActivateCounter()
    {
        actualTime = maxTime;
        ChangeTimer(true);
    }
    private void DeactivateCounter()
    {
        ChangeTimer(false);
    }


 

    /// <summary>
    /// Methods for changing directions of the proyectile while shooting
    /// </summary>

    /*

    public void ShootLeft()
    {
        //Debug.Log("-----------ShootLeft-----------");
        rb.velocity = transform.right * -speed;

    }

    public void ShootRight()
    {
        //Debug.Log("-----------ShootRight-----------");

        rb.velocity = transform.right * speed;

    }*/


    ///Enemy takes damage from the spell
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);

        EnemyController enemy =  hitInfo.GetComponent<EnemyController>();
        if(enemy != null)
        {
            enemy.EnemyTakesDamage();
        }

        Destroy(gameObject);

    }
}
