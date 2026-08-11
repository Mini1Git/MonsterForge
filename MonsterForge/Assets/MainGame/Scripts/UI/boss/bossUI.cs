using TMPro;
using UnityEngine;

public class bossUI : MonoBehaviour
{
    public BossAI boss;
    TextMeshProUGUI bossUI_text;

    public void Awake()
    {
        bossUI_text = GetComponentInChildren<TextMeshProUGUI>();
        bossUI_text.text = null;
        boss = GameObject.FindAnyObjectByType<BossAI>();

    }
    public void Start()
    {
        GameManager.Instance.player.GetComponent<PlayerHealth>().playerDeath += hideBossName;
    }
    public void hideBossName()
    {
        bossUI_text.text = null;
    }
    public void showBossName()
    {
        bossUI_text.text = boss.bossName; // this is the text above the boss healthbar.
        boss.health.onBossDie -= showBossDefeat;
        boss.health.onBossDie += showBossDefeat;
    }
    public void showBossDefeat()
    {
        bossUI_text.text = $"{boss.bossName} has been defeated!";
    }
}
