using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager Instance;
    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        UpdateGameState(GameState.Start);
    }

    private void Update()
    {
        
    }


    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.Start:
                HandleStart();
                break;
            case GameState.Bet:
                HandleBet();
                break;
            case GameState.PlayerTurn:
                HandlePlayerTurn();
                break;
            case GameState.GameOver:
                HandleGameOver();
                break;
        }

        OnGameStateChanged(newState);
    }

    private void HandleStart()
    {

    }

    private void HandleBet()
    {

    }

    private void HandlePlayerTurn()
    {

    }

    private void HandleGameOver()
    {

    }

    public enum GameState
    {
        Start,
        Bet,
        PlayerTurn,
        GameOver,

    }


}
