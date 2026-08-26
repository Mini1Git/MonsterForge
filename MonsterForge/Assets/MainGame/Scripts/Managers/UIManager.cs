using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject playerDeath_UI;
    [Header("HUD")]
    public List<HealthBar_UI> healthBars;

    public bool startFadeIn = false;
    public float fadeInDelay;
    public float fadeSpeed = 1;
    public Transform oldTransform;
    public void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
        playerDeath_UI = GameObject.FindGameObjectWithTag("playerDeathUI");
    }
    public void Start()
    {
       
        if (healthBars.Count == 0)
        {
            Debug.LogWarning("There are no healthbars! Add one to the list in UI Manager!");
        }
    }

    public void playerDiedUI() {



        Debug.LogWarning("Player had died (from UIMANAGER");
        playerDeath_UI.transform.position = new Vector2(oldTransform.position.x, oldTransform.position.y);
        playerDeath_UI.SetActive(true);
        playerDeath_UI.GetComponent<CanvasGroup>().interactable = true; // so u can click buttons.
        playerDeath_UI.GetComponent<CanvasGroup>().alpha = 0f;
        
        
        startFadeIn = true;
        


    }
    public void Update()
    {
        if (startFadeIn)
        {
            StartCoroutine(fadein());
        }
    }
    public IEnumerator fadein()
    {
        
        while (playerDeath_UI.GetComponent<CanvasGroup>().alpha < 1f)
        {
            yield return new WaitForSeconds(fadeInDelay);
            playerDeath_UI.GetComponent<CanvasGroup>().alpha += fadeSpeed * Time.deltaTime;
        }
        playerDeath_UI.GetComponent<CanvasGroup>().alpha = 1f;
        

    }
    public void setupDeath()
    {
        
        if (playerDeath_UI == null)
        {
            Debug.Log("Cannot find player death UI");
        }
        playerDeath_UI.GetComponent<CanvasGroup>().interactable = false;
        playerDeath_UI.transform.position = new Vector2(10000,oldTransform.position.y); // move out of the way lol.
    }
    public void hideHealthUI(bool hide) // tho this hides all HEALTHBARS. Good for cutscenes.
    {
        if (hide)
        {
            foreach (HealthBar_UI healthBar in healthBars)
            {
                healthBar.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (HealthBar_UI healthBar in healthBars)
            {
                healthBar.gameObject.SetActive(true);
            }
        }
    }
    public void updateHealthUI()
    {

        foreach (HealthBar_UI healthBar in healthBars)
        {
            healthBar.updateHealth();
        }
    }

    public void findNewHealthBars()
    {

        healthBars = new List<HealthBar_UI>(
            FindObjectsByType<HealthBar_UI>(FindObjectsSortMode.None)
        );

        
        //kinda of a bug, the issue is that once respawned, theres an extra element.
        Debug.Log($"Found {healthBars.Count} health bars.");

    }

    public void bossFightEnd()
    {
        List<HealthBar_UI> removeList = new List<HealthBar_UI>(); // elements to remove. Resets everytime.
        foreach (HealthBar_UI hpBar in healthBars)
        {
            if (hpBar.isBossHealthBar) // verify its a boss
            {
                GameObject.Destroy(hpBar.gameObject);
                removeList.Add(hpBar);
                
                
            }
            
        }
        foreach(HealthBar_UI bossBar in removeList)
        {
            healthBars.Remove(bossBar);
        }
        
    }
    //function to enable victory screen.
}

