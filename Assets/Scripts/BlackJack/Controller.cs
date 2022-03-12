using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;


/// <summary>
/// Main controller which player and dealer controller extend from, implements basic functions
/// </summary>
public class Controller : MonoBehaviour
{
    protected List<GameObject> m_Cards = new List<GameObject>();
    protected int m_pointValue = 0;
    
    protected void Awake()
    {
        BJGameManager.OnGameStateChange += GameManagerStateChange;
    }

    protected void OnDestroy()
    {
        BJGameManager.OnGameStateChange -= GameManagerStateChange;
    }

    //Updates to GameState event system in BJGameManager
    protected void GameManagerStateChange(BJGameManager.GameState state)
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
            if (this.m_pointValue == 21)
            {
                BJGameManager.Instance.UpdateGameState(BJGameManager.GameState.Win);
            }
        }
        if (state == BJGameManager.GameState.Win || state == BJGameManager.GameState.Lose)
        {
        }
    }
    
    //Adds Card Gameobject to m_cards array and adds points to m_pointValue
    public void DrawCard()
    {
        m_Cards.Add(BJGameManager.Instance.GetCardObjectFromDeck());
        m_Cards[m_Cards.Count-1].transform.SetParent(this.transform);
        Vector3 newPosition = m_Cards[m_Cards.Count - 1].transform.position;
        newPosition.z -= 1;
        m_Cards[m_Cards.Count - 1].transform.position = newPosition;
        int value = m_Cards[m_Cards.Count - 1].gameObject.GetComponent<CardController>().CardValue();
        if (value > 10)
        {
            value = 10;
        }

        else if (value == 1)
        {
            m_Cards[m_Cards.Count - 1].gameObject.GetComponent<CardController>().UpdateCardValue(11);
            value = 11;
        }
        m_pointValue += value;
        Debug.Log("Current count: " + m_pointValue.ToString());
    }

    //Iterates through each card in hand and adds up the point value
    public void UpdatePointValues()
    {
        int finalValue = 0;
        foreach (GameObject card in m_Cards)
        {
            int value = card.GetComponent<CardController>().CardValue();
            if (value > 10)
            {
                value = 10;
            }

            finalValue += value;
        }

        m_pointValue = finalValue;
        Debug.Log("Current count: " + m_pointValue.ToString());


    }

    public int GetPointValue()
    {
        return m_pointValue;
    }
    
}
