using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("HUD")]
    public List<HealthBar_UI> healthBars;




    public void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        Instance = this;

    }
    public void Start()
    {
        if (healthBars.Count == 0)
        {
            Debug.LogWarning("There are no healthbars! Add one to the list in UI Manager!");
        }
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

