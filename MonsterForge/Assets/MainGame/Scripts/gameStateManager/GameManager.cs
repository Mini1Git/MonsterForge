using UnityEngine;

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

    public void Start()
    {
        
    }


}
