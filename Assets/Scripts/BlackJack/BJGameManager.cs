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
            case GameState.Lose:
                HandleLose();
                break;
            case GameState.Win:
                HandleWin();
                break;
        }

        OnGameStateChange?.Invoke(newState);
    }

    private void HandleLose()
    {
        Debug.Log("You Lose!");
    }

    private void HandleWin()
    {
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
