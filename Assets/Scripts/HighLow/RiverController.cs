using UnityEngine;
using System.Collections.Generic;

public class RiverController : MonoBehaviour
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
            case GameManager.GameState.Start:
                HandleStart();
                break;
            case GameManager.GameState.Play:
                HandlePlay();
                break;
        }
    }

    private void HandleStart()
    {
        foreach (GameObject card in m_Cards)
        {
            Destroy(card);
        }
        m_Cards.Clear();
    }

    private void HandlePlay()
    {
        if (GameManager.Instance.RoundCount == 0)
        {
            foreach (GameObject card in m_Cards)
            {
                Destroy(card);
            }
            m_Cards.Clear();
            DrawCard();
        }
    }

    private void HandleLose()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Play);
    }

    private void HandleWin()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Play);
    }

    public void DrawCard()
    {
        GameObject card = GameManager.Instance.DrawRandomCardObject(); 
        card.transform.SetParent(this.transform);
        m_Cards.Add(card);
        GameManager.Instance.RoundCount++;
    }

    //Returns the last two cards from left to right order
    public CardData[] GetLastCardData()
    {
        CardData[] collection = new[]
        {
            m_Cards[m_Cards.Count - 2].GetComponent<CardController>().GetCardData(),
            m_Cards[m_Cards.Count - 1].GetComponent<CardController>().GetCardData(),
        };
        return collection;
    }
}
