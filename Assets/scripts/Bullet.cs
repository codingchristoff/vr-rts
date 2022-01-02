using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;
    public float speed = 10f;
    public float damage = 5f;

    void Awake()
    {
        transform.LookAt(target);
    }
    
    // Update is called once per frame
    void Update()
    {
        //Check if target exists
        if (target != null)
        {
            //Move to the target position
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.tag != Tags.ENEMY)
        {
            obj.SendMessage(Tags.HURT, damage);
            Destroy(gameObject);
        }    
        else if (obj.tag == Tags.GROUND)
        {
            Destroy(gameObject);
        }
    }
}
