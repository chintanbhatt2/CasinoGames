using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Extends from Controller class and reimplements GameManagerStateChange to reflect the unique if branches of a dealer
/// as well as a basic strategy for the dealer
/// </summary>

public class  DealerController : Controller
{
    protected new void Awake()
    {
        BJGameManager.OnGameStateChange += GameManagerStateChange;
        m_Cards.Clear();
    }
    
    private new void GameManagerStateChange(BJGameManager.GameState state)
    {
        if (state == BJGameManager.GameState.Deal)
        {
            foreach (GameObject card in m_Cards)
            {
                Destroy(card);
            }
            
            m_Cards.Clear();
            m_pointValue = 0;
            DrawCard();
            DrawCard();
            m_Cards[0].gameObject.GetComponent<CardController>().MakeCardFaceDown();
        }
        
        else if (state == BJGameManager.GameState.DealerTurn)
        {
            while (this.m_pointValue < 17)
            {
                DrawCard();
            }

            if (this.m_pointValue == 21)
            {
                BJGameManager.Instance.UpdateGameState(BJGameManager.GameState.Lose);
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

                    if (this.m_pointValue <= 21)
                    {
                        break;
                    }
                    else
                    {
                        BJGameManager.Instance.UpdateGameState(BJGameManager.GameState.Win);
                    }
                }
            }
            else
            {
                GameObject player = GameObject.Find("Player");
                if (player.gameObject.GetComponent<PlayerController>().GetPointValue() < m_pointValue)
                {
                    BJGameManager.Instance.UpdateGameState(BJGameManager.GameState.Lose);
                }
                else if (player.gameObject.GetComponent<PlayerController>().GetPointValue() == m_pointValue)
                {
                    //TODO: Implement Game_Draw state, for now the player just wins
                    BJGameManager.Instance.UpdateGameState(BJGameManager.GameState.Win);
                }
                else if(player.gameObject.GetComponent<PlayerController>().GetPointValue() > m_pointValue)
                {
                    BJGameManager.Instance.UpdateGameState(BJGameManager.GameState.Win);
                }
            }
        }
        
        else if (state == BJGameManager.GameState.Win || state == BJGameManager.GameState.Lose)
        {
            m_Cards[0].gameObject.GetComponent<CardController>().MakeCardFaceUp();
        }

        
    }
    
    
    

}
