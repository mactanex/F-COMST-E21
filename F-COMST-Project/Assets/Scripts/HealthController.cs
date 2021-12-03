using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{

    public float currentPlayerHealth = 100.0f;
    [SerializeField] private float maxPlayerHealth = 100.0f;
    [SerializeField] private float regenRate = 1;
    private bool canRegen = false;

    [Header("Add Blood splatter image here")]
    [SerializeField] private Image redBloodSplatterImage = null;

    [Header("Add radial gradient here")]
    [SerializeField] private Image radialGradient = null;
    [SerializeField] private float hurtTimer = 0.1f;

    [Header("Health timer")]
    [SerializeField] private float healCooldown = 3.0f;
    [SerializeField] private float maxHealCooldown = 3.0f;
    [SerializeField] private bool startCooldown = false;

    public GameOverScript GameOverScreen;
    public Camera Camera;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(currentPlayerHealth <= 0)
        {
            Camera.GetComponent<Animator>().SetBool("Death", true);
            Cursor.lockState = CursorLockMode.None;
            InGameMenu.GameIsPaused = true;
            StartCoroutine(GameOver());
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            TakeDamage(20);
        }
        if (startCooldown)
        {
            healCooldown -= Time.deltaTime;
            if (healCooldown <= 0)
            {
                canRegen = true;
                startCooldown = false;
            }
        }

        if (canRegen)
        {
            if (currentPlayerHealth <= maxPlayerHealth - 0.01)
            {
                currentPlayerHealth += Time.deltaTime * regenRate;
                UpdateHealth();
            }
            else
            {
                currentPlayerHealth = maxPlayerHealth;
                healCooldown = maxHealCooldown;
                canRegen = false;
            }
        }
    }

    void UpdateHealth()
    {
        Color splatterAlpha = redBloodSplatterImage.color;
        splatterAlpha.a = 1 - (currentPlayerHealth / maxPlayerHealth);
        redBloodSplatterImage.color = splatterAlpha;
    }

    IEnumerator DamageFlash()
    {
        radialGradient.enabled = true;
        yield return new WaitForSeconds(hurtTimer);
        radialGradient.enabled = false;
        AudioManager.Play("Hurt");
    }

    public void TakeDamage(float damage)
    {
        currentPlayerHealth -= damage;

        if(currentPlayerHealth >= 0)
        {
            canRegen = false;
            StartCoroutine(DamageFlash());
            UpdateHealth();
            healCooldown = maxHealCooldown;
            startCooldown = true;
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1);
        GameOverScreen.GameOver();
    }
}
