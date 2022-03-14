using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Loads the UI scene into the main game scene
/// </summary>
public class UILoader : MonoBehaviour
{
    private void Awake()
    {
        BJGameManager.OnGameStateChange += StateChange;
    }

    private void OnDestroy()
    {
        BJGameManager.OnGameStateChange -= StateChange;
    }

    private void StateChange(BJGameManager.GameState state)
    {
        if (state == BJGameManager.GameState.Start)
        {
            if(SceneManager.GetSceneByName("BlackJackUI").isLoaded == false)
            {
                SceneManager.LoadSceneAsync("BlackJackUI", LoadSceneMode.Additive);
            }
        }
    }
}
