using System;
using System.Collections.Generic;
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
    public void hideHealthUI(bool hide)
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
    }
}
