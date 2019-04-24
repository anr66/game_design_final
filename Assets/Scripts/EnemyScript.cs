using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    // Max health
    public float health = 20f;
    float currentHealth;
    public GameObject healthBarPrefab;
    GameObject healthBar;

    // How much the enemy is "worth" when destroyed
    public float worth = 4f;

    void Awake() { 
        currentHealth = health;
        // this may need some tweaking to make it look right
        Vector3 healthBarOffset = new Vector3(0, 0, .25f);
        healthBar = Instantiate(healthBarPrefab, transform.position + healthBarOffset, Quaternion.identity, transform);

    }
    void Update() {
        
    }

    public void Hurt(float damage) { 
        currentHealth -= damage;

        // Is dead?
        if (currentHealth <= 0) { 
            Money.Amount += worth;
            Destroy(gameObject);
        } 
        Transform piviot = healthBar.transform.Find("HeathlyPiviot");
        Vector3 scale = piviot.localScale;
        scale.x = Mathf.Clamp(currentHealth/health, 0, 1);
        piviot.localScale = scale;
    }
}
