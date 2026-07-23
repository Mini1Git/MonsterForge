using UnityEngine;

public class signalEvents : MonoBehaviour
{
    BossAI bossAI;
    GameObject player; 

    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Boss") != null)
        {
            bossAI = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossAI>();
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void freezePlayer(bool freezed) // this would imply a cutscene is taking place.
    {
        if (freezed)
        {
            player.GetComponent<PlayerMovement>().freezeMovement();
            UIManager.Instance.hideHealthUI(true);
        }
        else
        {
            player.GetComponent<PlayerMovement>().unfreezeMovement();
            UIManager.Instance.hideHealthUI(false);
        }
    }
    public void startBossFight()
    {
        bossAI.changeState(new Decision_State(bossAI));

    }
}
