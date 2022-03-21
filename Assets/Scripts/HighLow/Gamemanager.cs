using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Start,
        Play,
        Win,
        Lose,
        End,
    }

    public int Pot = 0;

    public static event Action<GameState> OnGameStateChange;

    public static GameManager Instance;

    private List<CardData> m_Cards = new List<CardData>();

    public GameState State;

    public int RoundCount;

    public int MaximumRounds;

    [SerializeField] private GameObject CardPrefab;


    public void Awake()
    {
        Instance = this;
        Assert.IsNotNull(CardPrefab);
        Assert.IsTrue(MaximumRounds <= 13);
        
        GameManager.OnGameStateChange += StateChange;
        CardData.Suit randomSuit = (CardData.Suit)(Random.Range(0, 3));
        for (int i = 1; i <= 13; i++)
        {
            m_Cards.Add(new CardData(randomSuit, i));
        }
        SceneManager.LoadScene("Scenes/HighLow/HighLowUI", LoadSceneMode.Additive);
        SceneManager.LoadScene("BankOverlay", LoadSceneMode.Additive);

    }

    public void Start()
    {
        RoundCount = 0;
        Pot = 0;
        Instance.UpdateGameState(GameState.Start);
    }

    private void StateChange(GameState state)
    {
        switch (state)
        {
            case GameState.Lose:
                HandleLoss();
                break;
            case GameState.Win:
                HandleWin();
                break;    
            case GameState.Play:
                HandlePlay();
                break;
            case GameState.Start:
                HandleStart();
                break;
        }
    }

    private void HandlePlay()
    {
        BankUIController.Instance.UpdateMoney(-20);
        Pot += 20;
    }

    private void HandleStart()
    {
        Pot = 0;
    }
    
    private void HandleLoss()
    {
        //Remove money from player's bank
        Debug.Log("You lose!");
        Pot = 0;
        if (RoundCount >= MaximumRounds)
        {
            RoundCount = 0;
            UpdateGameState(GameState.End);
        }
        else
        {
            UpdateGameState(GameState.Play);
        }
    }

    private void HandleWin()
    {
        //Add money to player's bank
        Debug.Log("You win!");
        BankUIController.Instance.UpdateMoney(Pot);
        
        Pot = 0;
        if (RoundCount >= MaximumRounds)
        {
            RoundCount = 0;
            UpdateGameState(GameState.End);
        }        
        else
        {
            UpdateGameState(GameState.Play);
        }    
    }
    
    public void UpdateGameState(GameState state)
    {
        State = state;
        OnGameStateChange?.Invoke(state);
    }

    public GameObject DrawRandomCardObject()
    {
        GameObject card = Instantiate(CardPrefab);
        int random = Random.Range(0, m_Cards.Count - 1);
        CardData drawnCard = m_Cards[random];
        m_Cards.RemoveAt(random);
        card.GetComponent<CardController>().SetCard(drawnCard);
        return card;
    }
    
}
