using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System;

/// <summary>
/// Handles the button presses and calls to the GameState in BJGameManager to update on certain button hits.
/// </summary>

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public Button[] buttons = new Button[5];

    private void Awake()
    {
        BJGameManager.OnGameStateChange += StateChange;
    }

    private void OnDestroy()
    {
        BJGameManager.OnGameStateChange -= StateChange;
    }

    void Start()
    {
        Assert.AreEqual(buttons.Length, 5);
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(false);
        }

        buttons[0].gameObject.SetActive(true);
    }
    private void StateChange(BJGameManager.GameState state)
    {
        if (state == BJGameManager.GameState.Start || state == BJGameManager.GameState.Win || state == BJGameManager.GameState.Lose)
        {
            foreach (var button in buttons)
            {
                button.gameObject.SetActive(false);
            }

            buttons[0].gameObject.SetActive(true);
        }
        else
        {
            foreach (Button button in buttons)
            {
                button.gameObject.SetActive(false);
            }

            for (int i = 1; i < buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(true);
            }
        }
    }

    public void OnClickHit()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerController>().OnClickHit();
    }

    public void OnClickStay()
    {
        BJGameManager.Instance.UpdateGameState(BJGameManager.GameState.DealerTurn);
    }

    public void OnClickFold()
    {
        BJGameManager.Instance.UpdateGameState(BJGameManager.GameState.Lose);
    }
    
    public void NewGameButton()
    {
        BJGameManager.Instance.UpdateGameState(BJGameManager.GameState.Deal);
    }
    
    

}
