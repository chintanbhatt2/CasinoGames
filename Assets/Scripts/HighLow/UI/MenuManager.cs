using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject NewGame, PlayGame;


    private void Awake()
    {
        GameManager.OnGameStateChange += StateChange;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= StateChange;
    }


    private void StateChange(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.Start:
                HandleStart();
                break;
            case GameManager.GameState.Play:
                HandlePlay();
                break;
            case GameManager.GameState.End:
                HandleEnd();
                break;
        }
    }


    private void HandleStart()
    {
        NewGame.SetActive(true);
        PlayGame.SetActive(false);
        
    }

    private void HandlePlay()
    {
        NewGame.SetActive(false);
        PlayGame.SetActive(true);
    }

    private void HandleEnd()
    {
        NewGame.SetActive(true);
        PlayGame.SetActive(false);      
    }


    public void OnClickBet()
    {
        GameManager.Instance.Pot += 20;
        BankUIController.Instance.UpdateMoney(-20);

    }
    
    public void OnClickMainMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenu", LoadSceneMode.Single);
    }
    public void OnClickNewGame()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Play);
    }

    public void OnClickHigh()
    {
        GameObject river = GameObject.Find("River");
        Debug.Assert(river != null);
        river.GetComponent<RiverController>().DrawCard();
        CardData[] lastTwo = river.GetComponent<RiverController>().GetLastCardData();
        if (lastTwo[0].CardNumber < lastTwo[1].CardNumber)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Win);
        }
        else
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Lose);
        }
    }

    public void OnClickLow()
    {
        GameObject river = GameObject.Find("River");
        Debug.Assert(river != null);
        river.GetComponent<RiverController>().DrawCard();
        CardData[] lastTwo = river.GetComponent<RiverController>().GetLastCardData();
        if (lastTwo[0].CardNumber > lastTwo[1].CardNumber)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Win);
        }
        else
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Lose);
        }
    }


}
