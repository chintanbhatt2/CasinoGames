using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

/// <summary>
/// Basic data class that contains an array of card data for a simulated card deck without replacement
/// </summary>


public class CardDeck 
{

    private List<CardData> Deck = new List<CardData>();
    
    
    public void FillDeck()
    {
        for (int i = 0; i < 52; i++)
        {
            CardData.Suit s = (CardData.Suit) (i / 13);
            int n = i % 13;
            if (n == 0) n = 13;
            CardData newCard = new CardData(s, n);
            Deck.Add(newCard);
        }
    }

    public CardData GetRandomCardData()
    {
        Assert.IsFalse(Deck.Count == 0);
        int n = Random.Range(1, Deck.Count - 1);
        CardData returnCard = Deck[n];
        Deck.RemoveAt(n);
        return returnCard;
    }

}
