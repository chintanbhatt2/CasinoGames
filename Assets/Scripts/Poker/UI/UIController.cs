using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject NewGameUI;
    public GameObject PlayGameUI;
    
    
    private void Awake()
    {
        GameManager.OnGameStateChange += StateChange;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= StateChange;
    }

    private void Start()
    {
        NewGameUI.SetActive(true);
        PlayGameUI.SetActive(false);
    }

    private void StateChange(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.Draw:
                HandleDraw();
                break;
            case GameManager.GameState.Win:
            case GameManager.GameState.Lose:
                HandleEnd();
                break;
        }
    }

    private void HandleEnd()
    {
        NewGameUI.SetActive(true);
        PlayGameUI.SetActive(false);
    }

    private void HandleDraw()
    {
        NewGameUI.SetActive(false);
        PlayGameUI.SetActive(true);
    }

    public void OnNewGameClick()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Draw);
    }
    
    public void OnFoldClick()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Lose);
    }

    public void OnBetClick()
    {
        BankUIController.Instance.UpdateMoney(-20);
        GameManager.Instance.Pot += 20;
        if (GameManager.Instance.State == GameManager.GameState.Refill)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Compare);
        }
    }

    public void OnDrawClick()
    {
        switch (GameManager.Instance.State)
        {
            // case GameManager.GameState.Refill:
            //     GameManager.Instance.UpdateGameState(GameManager.GameState.Compare);
            //     break;
            case GameManager.GameState.Draw:
                GameManager.Instance.UpdateGameState(GameManager.GameState.Refill);
                break;
            default:
                GameManager.Instance.UpdateGameState(GameManager.GameState.Draw);
                break;
        }
    }

    public void OnMainMenuClick()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    
    
}
