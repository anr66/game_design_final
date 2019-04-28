using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public float health = 100f;
    float currentHealth;

    public GameObject healthBarPrefab;
    GameObject healthBar;


    private void Awake()
    {
        currentHealth = health;
        // this may need some tweaking to make it look right
        Vector3 healthBarOffset = new Vector3(0, 0.05f, 0);
        healthBar = Instantiate(healthBarPrefab, transform.position + healthBarOffset, Quaternion.identity, transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;

        // check if its enemy
        if (obj.tag == "Enemy")
        {
            currentHealth -= obj.GetComponent<EnemyScript>().damage;
            Transform piviot = healthBar.transform.Find("HealthyPiviot");
            if (piviot == null)
            {
                Debug.Log("poop");
            }
            Vector3 scale = piviot.localScale;
            scale.x = Mathf.Clamp(currentHealth / health, 0, 1);
            piviot.localScale = scale;

            Destroy(obj);

            CheckHealth();
        }
    }

    void CheckHealth()
    {
        // have we died?
        if (currentHealth <= 0)
        {
            Destroy(gameObject);

        }
    }
}
