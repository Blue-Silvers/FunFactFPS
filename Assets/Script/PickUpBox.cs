using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickUpBox : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private PlayerMovement gunScript;

    float actualLife;
    [SerializeField] float maxLife;
    [SerializeField] float heal;
    [SerializeField] int enemyDamage;
    [SerializeField] int money;
    [SerializeField] int upgradePrice;
    [SerializeField] int healPrice;
    [SerializeField] int ammoPrice;
    [SerializeField] TextMeshProUGUI MoneyTxt, BobsTxt;
    float bobFontSizeStart;
    float bobFontSizeEvolve;
    [SerializeField] int ammoCharge;

    [SerializeField] HealthBar healthBar;
    [SerializeField] GameObject Shop, deathScreen;

    bool gameIsPaused;
    int nbUpgrade = 1;
    [SerializeField] TextMeshProUGUI nbUpgradeTxt;
    [SerializeField] GameObject upgradeButton, interactButton;

    GameObject[] Bobs;
    int nbOfBob;
    int nbOfDeath;

    int RdEscape;
    private void Start()
    {
        money = 0;
        MoneyTxt.text = money.ToString();
        actualLife = maxLife;
        healthBar.SetMaxHealth(maxLife);
        animator = GetComponent<Animator>();

        Bobs = GameObject.FindGameObjectsWithTag("Bob");
        nbOfBob = Bobs.Length;
        nbOfDeath = Bobs.Length;
        BobsTxt.text = nbOfBob.ToString();
        bobFontSizeStart = BobsTxt.fontSize;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
        }

        if(BobsTxt.fontSize != bobFontSizeStart)
        {
            BobsTxt.fontSize = Mathf.SmoothStep(BobsTxt.fontSize, bobFontSizeStart, 0.05f);
            BobsTxt.color = new Color(Mathf.SmoothStep(BobsTxt.color.r, 1, 0.05f), Mathf.SmoothStep(BobsTxt.color.g, 1, 0.05f), Mathf.SmoothStep(BobsTxt.color.b, 1, 0.05f), 1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            RdEscape = Random.Range(1, 10);
            if (RdEscape != 1)
            {
                actualLife -= enemyDamage;
                healthBar.SetHealth(actualLife);
                if (actualLife <= 0)
                {
                    Invoke("Death",1f);
                }
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E) && (other.gameObject.tag == "Ammo" || other.gameObject.tag == "Heal" || other.gameObject.tag == "Money"))
        {


            if (other.gameObject.tag == "Ammo")
            {
                gunScript.Reload(ammoCharge);
            }
            else if (other.gameObject.tag == "Heal")
            {
                actualLife += heal;

                if (actualLife > maxLife)
                {
                    actualLife = maxLife;
                }

                healthBar.SetHealth(actualLife);
            }
            else
            {
                money += 10;
                MoneyTxt.text = money.ToString();
            }

            Destroy(other.gameObject);
            Invoke("DontShowInteract", 0.1f);
        }
        else if (Input.GetKey(KeyCode.E) && other.gameObject.tag == "Upgrade")
        {

            Time.timeScale = 0f;
            Shop.SetActive(true);
        }

        if (other.gameObject.tag == "Ammo" || other.gameObject.tag == "Heal" || other.gameObject.tag == "Money" || other.gameObject.tag == "Upgrade")
        {
            interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ammo" || other.gameObject.tag == "Heal" || other.gameObject.tag == "Money" || other.gameObject.tag == "Upgrade")
        {
            interactButton.SetActive(false);
        }
    }

    void DontShowInteract()
    {
        interactButton.SetActive(false);
    }

    public void ActualisateNbOfBob()
    {
        Bobs = GameObject.FindGameObjectsWithTag("Bob");
        nbOfBob = Bobs.Length;
        if (nbOfBob < nbOfDeath)
        {
            nbOfDeath -= 1;
            money += 10;
            MoneyTxt.text = money.ToString();
            BobsTxt.text = nbOfBob.ToString();
            BobsTxt.fontSize = BobsTxt.fontSize + 30;
            BobsTxt.color = Color.red;
        }
    }

    //Shop
    public void Resume()
    {
        Shop.SetActive(false);
        Time.timeScale = 1.0f;
        gameIsPaused = false;
    }

    public void BuyUpgrade()
    {
        if (money >= upgradePrice)
        {
            nbUpgrade += 1;
            money -= upgradePrice;
            MoneyTxt.text = money.ToString();

            if (nbUpgrade > 3)
            {
                upgradeButton.SetActive(false);
            }

            gunScript.Upgrade();
            nbUpgradeTxt.text = nbUpgrade.ToString();
        }
    }
    public void BuyHeal()
    {
        if (money >= healPrice)
        {
            money -= healPrice;

            actualLife += heal;
            MoneyTxt.text = money.ToString();

            if (actualLife > maxLife)
            {
                actualLife = maxLife;
            }

            healthBar.SetHealth(actualLife);
        }
    }
    public void BuyAmmo()
    {
        if (money >= ammoPrice)
        {
            money -= ammoPrice;

            MoneyTxt.text = money.ToString();

            gunScript.Reload(ammoCharge);
        }
    }

    void Death()
    {
        deathScreen.SetActive(true);
    }
}
