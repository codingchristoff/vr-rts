using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using 

public class DoubleTurret : MonoBehaviour, ITurret
{

    public float range = 3f;
    public float fireRate = 1f;
    public GameObject bulletPrefab;
    public Transform barrelExit, barrelExit2;

    private Transform target;
    private float fireCounter = 0;    

    // Update is called once per frame
    void Update()
    {
        FindNextTarget();

        if (target != null) AimAtTarget();
        Shoot();
    }

    void FindNextTarget()
    {
        //So only enemy is targeted
        int layerMask = LayerMask.GetMask(Tags.ENEMY);

        //Check range
        Collider[] enemies = Physics.OverlapSphere(transform.position, range, layerMask);

        //Check if in range
        if (enemies.Length > 0)
        {
            //Assume first enemy is closest
            target = enemies[0].gameObject.transform;

            //Loop through all enemies
            foreach (Collider enemy in enemies)
            {
                //Calculate the distance of the enemy to the tower
                float distance = Vector3.Distance(transform.position, enemy.transform.position);

                //See who is closer
                if (distance < Vector3.Distance(transform.position, target.position))
                {
                    target = enemy.gameObject.transform;
                }
            }
        }
        else
        {
            target = null;
        }
    }

    void AimAtTarget()
    {
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = rotation;        
    }

   public void Shoot()
    {
        if(fireCounter <= 0)
        {
            GameObject newBullet = Instantiate(bulletPrefab, barrelExit.position, Quaternion.identity);
            GameObject newBullet2 = Instantiate(bulletPrefab, barrelExit2.position, Quaternion.identity);
            newBullet.GetComponent<Bullet>().target = target;
            newBullet2.GetComponent<Bullet>().target = target;
            fireCounter = fireRate;
        }
        else
        {
            fireCounter -= Time.deltaTime;
        }
    }
}
