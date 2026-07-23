using TMPro;
using UnityEngine;

public class bossUI : MonoBehaviour
{
    public BossAI bossAI;
    TextMeshProUGUI bossUI_text;

    public void Awake()
    {
        bossUI_text = GetComponentInChildren<TextMeshProUGUI>();
        bossUI_text.text = null;

    }
    public void showBossName()
    {
        bossUI_text.text = bossAI.bossName; // this is the text above the boss healthbar.
    }
}
