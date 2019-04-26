using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Transform target;
    public float speed = 10.0f;
    public float damage = 5.0f;

    void Update() {
        if (target != null) { // if the target has already been destroyed by another tower, target will be null
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        } else { // And if there is no target for the bullet, we destroy the bullet.
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other) { 
        GameObject obj = other.gameObject;
        if (obj.tag == "Enemy") { 
            obj.SendMessage("Hurt", damage);
            Destroy(gameObject);
        } else if (obj.tag == "Table") { 
            Destroy(gameObject);
        }
    }
}
