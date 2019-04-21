using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour {

    public float range = 3.0f;
    Transform target; 
    // For testing to see if we are actually aquiring the right target, see Part 2 near 21:12 timestamp
    
    void Update() { 
        FindNextTarget();
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
            foreach (Collider enemy in enemies) { 
                // Distance between tower and next enemy
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < Vector3.Distance(transform.position, target.position)) { 
                    target = enemy.gameObject.transform;
                }
            }
        } else { 
            // if enemies is empty, we have no target
            target = null;
        }
    }
}
