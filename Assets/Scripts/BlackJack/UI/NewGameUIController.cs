using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameUIController : MonoBehaviour
{
    // Start is called before the first frame update

    public void NewGameButton()
    {
        BJGameManager.Instance.UpdateGameState(BJGameManager.GameState.Deal);
    }

}
