using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the button presses and calls to the GameState in BJGameManager to update on certain button hits.
/// </summary>

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject NewGameUI;
    public GameObject PlayGameUI;
    public TextMeshProUGUI endText, scoreText;
    private void Awake()
    {
        BJGameManager.OnGameStateChange += StateChange;
    }

    private void OnDestroy()
    {
        BJGameManager.OnGameStateChange -= StateChange;
    }


    private void Start()
    {
        NewGameUI.SetActive(true);
        PlayGameUI.SetActive(false);
        endText.text = "";
        
    }

    private void StateChange(BJGameManager.GameState state)
    {
        if (state == BJGameManager.GameState.Lose || state == BJGameManager.GameState.Win || state == BJGameManager.GameState.Start)
        {
            PlayGameUI.SetActive(false);
            NewGameUI.SetActive(true);
            if (state != BJGameManager.GameState.Start)
            {
                endText.text = "";
            }
            else
            {
                endText.text = "You " + state.ToString() + "!";
            }
        }

        else if (state == BJGameManager.GameState.Deal)
        {
            PlayGameUI.SetActive(true);
            NewGameUI.SetActive(false);
            endText.text = "";
            GameObject player = GameObject.Find("Player");
            player.GetComponent<PlayerController>().OnClickHit();
            scoreText.text =
                "Current score: " + player.GetComponent<PlayerController>().GetPointValue().ToString();
        }
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenu", LoadSceneMode.Single);
    }
}
