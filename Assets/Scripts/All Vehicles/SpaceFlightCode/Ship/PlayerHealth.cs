using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerHealth : MonoBehaviour {

    public Image currentHealthBar;
    public float currentHealth = 100;
    public float maxHealth = 100;
    public bool canTakeDamage;
    public float damageDelay = .5f;
    public GameObject gameOverText;
    public GameObject fixedCrosshair;
    public GameObject mouseCrosshair;
    public GameObject speedReadout;
    public GameObject timeReadout;
    public GameObject orbsCollected;
    public GameObject winGameText;
    public GameObject healthBar;
    public static float timeLeft = 200f;


    void Awake()
    {
        gameOverText.SetActive(false);
        winGameText.SetActive(false);
    }

    void Update()
    {
        float ratio = currentHealth / maxHealth;
        currentHealthBar.rectTransform.localScale = new Vector3(ratio,1,1);
        canTakeDamage = false;
        Invoke("CanTakeDamage", damageDelay);
        if (currentHealth <= 0)
        {
            GameOver();
        }

        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            GameOver();
        }
        if (EnergyOrb.orbCounter == 10)
        {
            GameWon();
        }
    }

    void TakeDamage()
    {
         currentHealth -= 20;
         Update();

    }

    void HealDamage()
    {
         currentHealth = 100;
         Update();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Laser")
        {
            return;
        }

        if (other.tag == "EnergyOrb")
        {
            return;
        }

        if (other.tag == "ShieldPickup")
        {
            return;
        }

        if (other.tag == "Forcefield")
        {
            return;
        }

        if (other.tag == "HealthPack")
        {
            HealDamage();
        }

        if (canTakeDamage)
        {
            TakeDamage();
        }        
    }

    void CanTakeDamage()
    {
        canTakeDamage = true;
    }

    void GameOver()
    {
        Destroy(GameObject.FindGameObjectWithTag("PlayerShip"));
        gameOverText.SetActive(true);
        fixedCrosshair.SetActive(false);
        mouseCrosshair.SetActive(false);
        speedReadout.SetActive(false);
        timeReadout.SetActive(false);
        orbsCollected.SetActive(false);
        healthBar.SetActive(false);
    }

    void GameWon()
    {
        Destroy(GameObject.FindGameObjectWithTag("PlayerShip"));
        fixedCrosshair.SetActive(false);
        mouseCrosshair.SetActive(false);
        speedReadout.SetActive(false);
        timeReadout.SetActive(false);
        orbsCollected.SetActive(false);
        winGameText.SetActive(true);
        healthBar.SetActive(false);
    }
}
