using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UnityLinker;
using UnityEngine;


/// <summary>
/// Extends from the controller class and implements player specific functions
/// </summary>
public class PlayerController : Controller
{
    public void OnClickHit()
    {
        DrawCard();
        if (this.m_pointValue == 21)
        {
            BJGameManager.Instance.UpdateGameState(BJGameManager.GameState.Win);
        }
        else if (m_pointValue < 21)
        {
            return;
        }
        else if (this.m_pointValue > 21)
        {
            foreach (GameObject card in m_Cards)
            {
                if (card.gameObject.GetComponent<CardController>().CardValue() == 11)
                {
                    this.m_pointValue -= 10;
                    card.gameObject.GetComponent<CardController>().UpdateCardValue(1);
                }

                if (this.m_pointValue < 21)
                {
                    return;
                }
                else if (this.m_pointValue == 21)
                {
                    BJGameManager.Instance.UpdateGameState(BJGameManager.GameState.Win);
                    return;
                }

            }
            BJGameManager.Instance.UpdateGameState(BJGameManager.GameState.Lose);

        }
    }
    
}
