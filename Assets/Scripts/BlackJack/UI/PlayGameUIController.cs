using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayGameUIController : MonoBehaviour
{

    [SerializeField]
    public TextMeshProUGUI CountText;
    
    public void OnClickHit()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerController>().OnClickHit();
        CountText.text =
            "Current score: " + player.GetComponent<PlayerController>().GetPointValue().ToString();
    }

    public void OnClickStay()
    {
        BJGameManager.Instance.UpdateGameState(BJGameManager.GameState.DealerTurn);
    }

    public void OnClickFold()
    {
        BJGameManager.Instance.UpdateGameState(BJGameManager.GameState.Lose);
    }

    public void OnClickBet()
    {
        if (BankUIController.Instance.MoneyData.Money < 20) return;
        
        BankUIController.Instance.UpdateMoney(-20);
        BJGameManager.Instance.Pot += 20;
    }
    

    
}
