using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public int maxHealth;
    int curHealth;

    public Slider healthSlider;

    private void Start()
    {
        curHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
    }

    private void Update()
    {
        if (curHealth <= 0) Destroy(this.gameObject);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.name == "ShootingParticle(Clone)")
        {
            curHealth -= 1;
            healthSlider.value = curHealth;
        }

        print(other.name);
    }
}
