using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void OnClickBlackJack()
    {
        SceneManager.LoadScene("Scenes/Blackjack/GameScene", LoadSceneMode.Single);
    }

    public void OnClickHighLow()
    {
        SceneManager.LoadScene("Scenes/HighLow/HighLow", LoadSceneMode.Single);
    }

    public void OnClickPoker()
    {
        SceneManager.LoadScene("Scenes/Poker/Poker", LoadSceneMode.Single);
    }

    public void OnClickPokerSeven()
    {
        SceneManager.LoadScene("Scenes/Poker/Poker7", LoadSceneMode.Single);
        
    }
}
