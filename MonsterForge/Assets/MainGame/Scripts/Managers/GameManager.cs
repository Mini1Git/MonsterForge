using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;
    public GameObject player;
    public Weapon_SO currentWeapon;
    public float player_CurrentHealthGM;
    public int global_deathCounter = 0;


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
        findAndSetupPlayer();
        boss = GameObject.FindAnyObjectByType<BossAI>();
        player_CurrentHealthGM = playerHealth.maxHealth;
    }
    private void Start()
    {
       
    }

    private void PlayerHealth_playerDeath()
    {
        Debug.LogWarning("Player died GAME MANAGER");
        UIManager.Instance.playerDiedUI();
        //load back to blacksmith.

    }

    public void findAndSetupPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.currentHealth = player_CurrentHealthGM; // keep track of player's hp.
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
        
        
        // Call whatever function you want here
        findAndSetupPlayer();
        setupScene();
        SetupUI();
        Debug.Log("Loaded scene: " + scene.name);


        
        playerHealth.playerDeath += PlayerHealth_playerDeath;
        
    }

    public void respawn()
    {
        Debug.Log("RESPAWN CLICK!");
        global_deathCounter++;
        Debug.Log(global_deathCounter);
        SceneManager.LoadScene(0);
        UIManager.Instance.hideHealthUI(false);
        playerHealth.setPlayerHealth(playerHealth.maxHealth);
    }
    private void SetupUI()
    {
        
        UIManager.Instance.setupDeath(); // setup death screen if a boss is present.
        
        

        UIManager.Instance.findNewHealthBars();
        UIManager.Instance.updateHealthUI();
    }

    private void setupScene()
    {
        Debug.Log("SETTING UP SCENE!");
        DialogueManager.Instance.newScene = true; // reset dialogue manager's npc.
        

        boss = GameObject.FindAnyObjectByType<BossAI>(); // reset boss.
        if (boss)
        {
            
            boss.health.onBossDie -= playerWonFight;// in case theres a sub event alr.
            boss.health.onBossDie += playerWonFight;
        }
        //setup player health stuff.

        playerHealth.OnHealthUpdate -= GameManager_OnHealthUpdate;
        playerHealth.OnHealthUpdate += GameManager_OnHealthUpdate;

    }

    private void GameManager_OnHealthUpdate()
    {
        Debug.Log("UPDATE HEALTH");
        setHealth(playerHealth.currentHealth);
    }

    private void setHealth(float health)
    {
        player_CurrentHealthGM = health;
    }
    private void playerWonFight()
    {
        Debug.LogWarning("You won the fight!");
        UIManager.Instance.bossFightEnd();
    }
    

}
