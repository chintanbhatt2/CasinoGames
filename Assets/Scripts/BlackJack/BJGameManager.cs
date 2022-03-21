using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR;
using Random = UnityEngine.Random;


/// <summary>
/// Game manager for Blackjack, contains all information about cards, shoes, and game states.
/// </summary>
public class BJGameManager : MonoBehaviour
{

    public static event Action<GameState> OnGameStateChange;
    
    public static BJGameManager Instance;
    
    public int ShoeCount;

    public int Pot = 0;
    
    private List<CardDeck> m_Decks = new List<CardDeck>();
    public GameState State;
    [SerializeField] private GameObject CardPrefab;
    public enum GameState
    {
        Start,
        Bet,
        Deal,
        PlayerTurn,
        DealerTurn,
        Win,
        Lose,
        Draw,
    }

    public void Awake()
    {
        Instance = this;
        Assert.IsNotNull(CardPrefab);
    }
    public void Start()
    {
        if (ShoeCount == 0)
        {
            ShoeCount = 4;
        }

        Pot = 0;

        for (int i = 0; i < ShoeCount; i++)
        {
            m_Decks.Add(new CardDeck());
            m_Decks[i].FillDeck();
        }
        Instance.UpdateGameState(GameState.Start);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.Deal:
                HandleDeal();
                break;
            case GameState.Lose:
                HandleLose();
                break;
            case GameState.Draw:
                HandleDraw();
                break;
            case GameState.Win:
                HandleWin();
                break;
        }

        OnGameStateChange?.Invoke(newState);
    }

    private void HandleDeal()
    {
        Pot = 20;
        BankUIController.Instance.UpdateMoney(-20);
    }
    
    private void HandleLose()
    {
        Debug.Log("You Lose!");
    }

    private void HandleDraw()
    {
        BankUIController.Instance.UpdateMoney(Pot/2);
    }
    
    private void HandleWin()
    {
        BankUIController.Instance.UpdateMoney(Pot);
        Debug.Log("You Win!");
    }

    public GameObject GetCardObjectFromDeck()
    {
        GameObject card = Instantiate(CardPrefab);
        int randomDeckSelector = Random.Range(0, m_Decks.Count - 1);
        card.GetComponent<CardController>().SetCard(m_Decks[randomDeckSelector].GetRandomCardData());
        return card;
    }
    
    
    
    
}
