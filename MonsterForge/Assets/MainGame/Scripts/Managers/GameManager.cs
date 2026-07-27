using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    public Weapon_SO currentWeapon;

    private void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Loaded scene: " + scene.name);

        // Call whatever function you want here
        SetupUI();
    }

    private void SetupUI()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        UIManager.Instance.findNewHealthBars();
        UIManager.Instance.updateHealthUI();
    }



}
