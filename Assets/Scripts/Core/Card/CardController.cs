using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to the card prefab and instantiates the artwork and basic card functionality
/// </summary>

public class CardController : MonoBehaviour
{
    private CardData data;
    
    public Sprite Artwork;
    private SpriteRenderer _spriteRenderer;
    public bool isFaceDown;

    public void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        data = new CardData(0, 0);
    }

    public void SetCard(CardData cd)
    {
        data = cd;
        string fileName = "";

        switch (data.CardSuit)
        {
            case CardData.Suit.Clubs:
                fileName += "C";
                break;
            case CardData.Suit.Hearts:
                fileName += "H";
                break;
            case CardData.Suit.Diamonds:
                fileName += "D";
                break;
            case CardData.Suit.Spades:
                fileName += "S";
                break;
            default:
                Debug.LogError("Cad did not contain a valid enum");
                break;

        }

        switch (data.CardNumber)
        {
            case 11:
                fileName += "J";
                break;
            case 12:
                fileName += "Q";
                break;
            case 13:
                fileName += "K";
                break;
            case 1:
                fileName += "A";
                break;
            default:
                fileName += data.CardNumber.ToString();
                break;

        }

        fileName = "Textures/Cards/" + fileName;
        Artwork = Resources.Load<Sprite>(fileName);
        _spriteRenderer.sprite = Artwork;
        isFaceDown = false;
    }

    public void MakeCardFaceDown()
    {
        isFaceDown = true;
        _spriteRenderer.sprite = Resources.Load<Sprite>("Textures/Cards/Back");
    }

    public void MakeCardFaceUp()
    {
        isFaceDown = false;
        _spriteRenderer.sprite = Artwork;
    }

    public void FlipCard()
    {
        if (isFaceDown)
        {
            MakeCardFaceUp();
        }
        else
        {
            MakeCardFaceDown();
        }
    }

    public int CardValue()
    {
        return data.CardNumber;
    }

    public void UpdateCardValue(int newValue)
    {
        data.CardNumber = newValue;
    }

}
