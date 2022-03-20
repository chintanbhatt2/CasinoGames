using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    private List<GameObject> m_Cards = new List<GameObject>();


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
            case GameManager.GameState.Draw:
                HandleDraw();
                break;
            case GameManager.GameState.Compare:
                HandleCompare();
                break;
            case GameManager.GameState.End:
                HandleEnd();
                break;
        }
    }

    private void HandleEnd()
    {
        
    }

    private void HandleCompare()
    {
        
    }

    private void HandleDraw()
    {
        
    }

    private void HandlePlay()
    {
        
    }

    private void HandleStart()
    {
        
    }
    


    public void DrawCard()
    {
        m_Cards.Add(GameManager.Instance.GetCardFromDeck());
    }
    
    

}
