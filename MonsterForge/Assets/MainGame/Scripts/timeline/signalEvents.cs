using System.Collections;
using TMPro;
using UnityEngine;

public class signalEvents : MonoBehaviour
{
    BossAI bossAI;
    GameObject player;
    

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Boss") != null)
        {
            bossAI = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossAI>();
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    public void displayBossNameDramatic()
    {
        TextMeshProUGUI bnDrama = GameObject.FindGameObjectWithTag("dramaBossName").GetComponent<TextMeshProUGUI>();
        StartCoroutine(displayBossNameCoroutine(bnDrama));
    }
    private IEnumerator displayBossNameCoroutine(TextMeshProUGUI textBox)
    {
        textBox.text = "The";
        yield return new WaitForSeconds(0.5f);
        textBox.text = "The Tutor";
        yield return new WaitForSeconds(0.2f);
        textBox.text = null;
    }


    public void freezePlayer(bool freezed) // this would imply a cutscene is taking place.
    {
        
        if (freezed)
        {
            
            player.GetComponent<PlayerMovement>().freezeMovement();
            player.GetComponent<PlayerAttack>().disableAttack();
            UIManager.Instance.hideHealthUI(true);
        }
        else
        {
            
            player.GetComponent<PlayerMovement>().unfreezeMovement();
            player.GetComponent<PlayerAttack>().enableAttack();
            UIManager.Instance.hideHealthUI(false);
        }
    }
    public void startBossFight()
    {
        bossAI.changeState(new Decision_State(bossAI));

    }
}
