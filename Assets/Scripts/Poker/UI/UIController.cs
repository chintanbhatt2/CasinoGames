using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public void OnDrawClick()
    {
        if (GameManager.Instance.State == GameManager.GameState.Draw)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Refill);
        }
        else
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Draw);
        }
    }
}
