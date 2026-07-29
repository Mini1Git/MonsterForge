using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    public Weapon_SO currentWeapon;
    public float player_CurrentHealthGM;
    
    [SerializeField]
    private BossAI boss;
    private PlayerHealth playerHealth;
    private void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindAnyObjectByType<BossAI>();
        playerHealth = player.GetComponent<PlayerHealth>();
        player_CurrentHealthGM = playerHealth.maxHealth;
    }
   
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        playerHealth.OnHealthUpdate -= GameManager_OnHealthUpdate;
        playerHealth.OnHealthUpdate += GameManager_OnHealthUpdate;

    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Loaded scene: " + scene.name);

        // Call whatever function you want here
        setupScene();
        SetupUI();
    }

    private void SetupUI()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        UIManager.Instance.findNewHealthBars();
        UIManager.Instance.updateHealthUI();
    }

    private void setupScene()
    {
        boss = GameObject.FindAnyObjectByType<BossAI>(); // reset boss.
        if (boss)
        {
            
            boss.health.onBossDie -= playerWonFight;// in case theres a sub event alr.
            boss.health.onBossDie += playerWonFight;
        }
        
    }

    private void GameManager_OnHealthUpdate()
    {
        player_CurrentHealthGM = playerHealth.currentHealth;
    }

    private void playerWonFight()
    {
        Debug.LogWarning("You won the fight!");
        UIManager.Instance.bossFightEnd();
    }
    

}
