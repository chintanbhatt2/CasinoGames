using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
            case GameManager.GameState.Draw:
                HandleDraw();
                break;
            case GameManager.GameState.Refill:
                HandleRefill();
                break;
            default:
                break;
        }
    }
    
    private void HandleDraw()
    {
        ClearHand();
        for (int i = 0; i < GameManager.Instance.HandTotal; i++)
        {
            m_Cards.Add(GameManager.Instance.GetCardFromDeck());
            m_Cards[m_Cards.Count-1].transform.SetParent(this.transform);
            Vector3 newPosition = m_Cards[m_Cards.Count - 1].transform.position;
            newPosition.z -= 1;
            m_Cards[m_Cards.Count - 1].transform.position = newPosition;
        }
    }

    private void HandleRefill()
    {

        for (int i = 0; i < m_Cards.Count; i++)
        {
            if (m_Cards[i] == null)
            {
                m_Cards.RemoveAt(i);
                i--;
            }
        }

        while (m_Cards.Count < GameManager.Instance.HandMax)
        {
            GameObject newCard = GameManager.Instance.GetCardFromDeck();
            newCard.transform.SetParent(this.transform);
            Vector3 newPosition = newCard.transform.position;
            newPosition.z -= 1;
            newCard.transform.position = newPosition;
            m_Cards.Add(newCard);
        }
        
        // for (int i = 0; i < GameManager.Instance.HandMax; i++)
        // {
        //     if (m_Cards[i] != null)
        //     {
        //         m_Cards[i] = GameManager.Instance.GetCardFromDeck();
        //         m_Cards[i].transform.SetParent(this.transform);
        //         Vector3 newPosition = m_Cards[i].transform.position;
        //         newPosition.z -= 1;
        //         m_Cards[i].transform.position = newPosition;
        //     }
        // }
    }

    public List<CardData> GetCardData()
    {
        List<CardData> returnCards = new List<CardData>();

        for (int i = 0; i < m_Cards.Count; i++)
        {
            returnCards.Add(m_Cards[i].GetComponent<CardController>().GetCardData());
        }

        return returnCards;
    }


    public void ClearHand()
    {
        foreach (GameObject card in m_Cards)
        {
            Destroy(card);
        }
        m_Cards.Clear();
    }

}
