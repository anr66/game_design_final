using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour {

    public float range = 0.3f;
    public float fireRate = 1.0f;
    public GameObject bulletPrefab;
    public Transform barrelExit;

    Transform target; 
    // For testing to see if we are actually aquiring the right target, see Part 2 near 21:12 timestamp
    float fireCounter = 0f;

    void Update() { 
        FindNextTarget();

        if (target != null) { 
            AimAtTarget();
            Shoot();
        }
    }

    void FindNextTarget() { 
        // See who is in range
        int layerMask = 1 << 8; // the tutorial said to do this I swear
        Collider[] enemies = Physics.OverlapSphere(transform.position, range, layerMask);
        
        // Check if we are in range
        if (enemies.Length > 0) { 
            // Assume first enemy is the closest
            target = enemies[0].gameObject.transform;
            
            // Check the rest of the enemies
            foreach (Collider enemy in enemies)
            { 
                // Distance between tower and next enemy
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < Vector3.Distance(transform.position, target.position))
                { 
                    target = enemy.gameObject.transform;
                }
            }
        }

        else
        { 
            // if enemies is empty, we have no target
            target = null;
        }
    }

    void AimAtTarget() { 
        // Create a vector pointing from our tower, down at the enemy
        Vector3 lookPos = target.position + transform.position;

        lookPos.y = 0f;

        // Not sure what a Quaternion is
        //Quaternion rotation = Quaternion.LookRotation(lookPos);
        //transform.rotation = rotation;
    }

    void Shoot()
    { 
        if (fireCounter <= 0)
        { 
            // Instantiate creates any object, Quaternion.idenity means "Default" 
            GameObject newestBullet = Instantiate(bulletPrefab, barrelExit.position, Quaternion.identity);
            newestBullet.GetComponent<BulletScript>().target = target;
            fireCounter = fireRate;
        }

        else
        { 
            fireCounter -= Time.deltaTime;
        }
    }
}
