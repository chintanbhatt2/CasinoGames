using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that contains basic data about a card (Suit, and card number). Face cards are represented numerically (i.e. 12, 13, 14)
/// </summary>

[System.Serializable]
public class CardData
{
    public enum Suit
    {
        Clubs,
        Hearts,
        Diamonds,
        Spades
    }

    public Suit CardSuit = 0;

    public int CardNumber = -1;


    public CardData(Suit s, int n)
    {
        CardSuit = s;
        CardNumber = n;
    }
    
    
}
