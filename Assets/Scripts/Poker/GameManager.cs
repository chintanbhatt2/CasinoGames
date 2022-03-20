using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using PlasticGui.WorkspaceWindow;
using UnityEngine.Assertions;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Start,
        Play,
        Draw,
        Refill,
        Compare,
        Win,
        Lose,
        End,
    }

    public enum Hands
    {
        FiveOfKind,
        StraightFlush,
        FourOfKind,
        FullHouse,
        Flush,
        Straight,
        ThreeOfKind,
        TwoPair,
        OnePair,
        HighCard,
    }
    

    [SerializeField] public int HandTotal = 5;

    [SerializeField] public int HandMax = 5;
    public static event Action<GameState> OnGameStateChange;

    public static GameManager Instance;

    public GameState State;

    [SerializeField] private GameObject CardPrefab;

    public int ShoeSize;

    private List<CardDeck> m_Decks = new List<CardDeck>();
    
    public void Awake()
    {
        Instance = this;
        Assert.IsNotNull(CardPrefab);
        if (ShoeSize == 0)
        {
            ShoeSize = 4;
        }
        GameManager.OnGameStateChange += StateChange;
        SceneManager.LoadScene("Scenes/Poker/PokerUI", LoadSceneMode.Additive);

    }

    public void Start()
    {
        Instance.UpdateGameState(GameState.Start);
        for (int i = 0; i < ShoeSize; i++)
        {
            m_Decks.Add(new CardDeck());
            m_Decks[i].FillDeck();
        }
    }
    
    private void StateChange(GameState state)
    {
        switch (state)
        {
            case GameState.Draw:
                HandleDraw();
                break;
            case GameState.Compare:
                HandleCompare();
                break;
            case GameState.Win:
                HandleWin();
                break;
            case GameState.Lose:
                HandleLose();
                break;
        }
    }
    
    public void UpdateGameState(GameState state)
    {
        State = state;
        OnGameStateChange?.Invoke(state);
    }


    public GameObject GetCardFromDeck()
    {
        int randomDeck = Random.Range(0, ShoeSize - 1);
        GameObject card = Instantiate(CardPrefab);
        card.GetComponent<CardController>().SetCard(m_Decks[randomDeck].GetRandomCardData());
        return card;
    }
    public struct HandData
    {
        public List<CardData> cards;
        public Hands hand;

        public HandData(List<CardData> Cards, Hands Hand)
        {
            cards = Cards;
            hand = Hand;
        }
    }

    public void HandleWin()
    {
        Debug.Log("You Win!");
    }

    public void HandleLose()
    {
        Debug.Log("You Lose!");
    }

    public void ClearHands()
    {
        GameObject Player = GameObject.Find("Player");
        GameObject Dealer = GameObject.Find("Dealer");
        
        Player.GetComponent<PlayerController>().ClearHand();
        Dealer.GetComponent<DealerController>().ClearHand();
    }
    
    public void HandleCompare()
    {
        GameObject Player = GameObject.Find("Player");
        GameObject Dealer = GameObject.Find("Dealer");


        List<CardData> PlayerCards = Player.GetComponent<PlayerController>().GetCardData();
        List<CardData> DealerCards = Dealer.GetComponent<DealerController>().GetCardData();

        if (PlayerCards.Count > HandMax)
        {
            UpdateGameState(GameState.Draw);
            return;
        }
        
        
        HandData playerHands = new HandData();
        HandData dealerHands = new HandData();


        playerHands = CheckHands(PlayerCards);
        dealerHands = CheckHands(DealerCards);

        if (playerHands.hand < dealerHands.hand)
        {
            Instance.UpdateGameState(GameState.Win);
        }
        else if (playerHands.hand > dealerHands.hand)
        {
            Instance.UpdateGameState(GameState.Lose);
        }
        else if (playerHands.hand == dealerHands.hand)
        {
            if (playerHands.cards[0].CardNumber > dealerHands.cards[0].CardNumber)
            {
                Instance.UpdateGameState(GameState.Win);
            }
            else if (playerHands.cards[0].CardNumber == dealerHands.cards[0].CardNumber)
            {
                CardData player = HighCard(PlayerCards.Except(playerHands.cards).ToList())[0];
                CardData dealer = HighCard(DealerCards.Except(dealerHands.cards).ToList())[0];

            }
            else
            {
                Instance.UpdateGameState(GameState.Lose);
            }
        }
        
    }


    private void HandleDraw()
    {
    }

    private HandData CheckHands(List<CardData> Cards)
    {
        List<CardData> returnCards = new List<CardData>();
        
        returnCards = FiveOfKind(Cards);
        if(returnCards.Count != 0)
        {
            return new HandData(returnCards, Hands.FiveOfKind);
        }
        returnCards = StraightFlush(Cards);
        if(returnCards.Count != 0)
        {
            return new HandData(returnCards, Hands.StraightFlush);

        }
        returnCards = FourOfKind(Cards);
        if(returnCards.Count != 0)
        {
            return new HandData(returnCards, Hands.FourOfKind);

        }
        returnCards = FullHouse(Cards);
        if(returnCards.Count != 0)
        {
            return new HandData(returnCards, Hands.FullHouse);

        }
        returnCards = Flush(Cards);
        if(returnCards.Count != 0)
        {
            return new HandData(returnCards, Hands.Flush);

        }
        returnCards = Straight(Cards);
        if(returnCards.Count != 0)
        {
            return new HandData(returnCards, Hands.Straight);

        }
        returnCards = ThreeOfKind(Cards);
        if(returnCards.Count != 0)
        {
            return new HandData(returnCards, Hands.ThreeOfKind);

        }
        returnCards = TwoPair(Cards);
        if(returnCards.Count != 0)
        {
            return new HandData(returnCards, Hands.TwoPair);

        }
        returnCards = OnePair(Cards);
        if(returnCards.Count != 0)
        {
            return new HandData(returnCards, Hands.OnePair);

        }
        returnCards = HighCard(Cards);
        return new HandData(returnCards, Hands.HighCard);

    }
    
    
    
    private List<CardData> FiveOfKind(List<CardData> Cards)
    {
        CardData firstCard = Cards[0];
        List<CardData> returnCards = new List<CardData>();
        returnCards.Add(firstCard);
        for (int i = 1; i < Cards.Count; i++)
        {
            if (Cards[i].CardNumber == firstCard.CardNumber)
            {
                    returnCards.Add(Cards[i]);
            }
            else
            {
                return new List<CardData>();
            }
        }

        return returnCards;
    }
    private List<CardData> StraightFlush(List<CardData> Cards)
    {
        CardData firstCard = Cards[0];
        List<int> numberList = new List<int>();
        List<CardData> returnCards = new List<CardData>();
        
        for (int i = 0; i < Cards.Count; i++)
        {
            if (Cards[i].CardSuit != firstCard.CardSuit)
            {
                return new List<CardData>();
            }
            else
            {
                numberList.Add(Cards[i].CardNumber);
                returnCards.Add(Cards[i]);
            }
        }
        numberList.Sort();     
        for (int i = 1; i < numberList.Count; i++)
        {
            if (numberList[i] != numberList[i - 1] + 1)
            {
                return new List<CardData>();
            }
        }
        
        return returnCards;
    }
    private List<CardData> FourOfKind(List<CardData> Cards)
    {
        Cards = Cards.OrderByDescending(x => x.CardNumber).ThenBy(x => x.CardSuit).ToList();
        List<CardData> returnCards = new List<CardData>();

        for (int i = 0; i < Cards.Count-1; i++)
        {
            returnCards.Add(Cards[i]);
            for (int j = i + 1; j < Cards.Count; j++)
            {
                if (Cards[i].CardNumber == Cards[j].CardNumber)
                {
                    returnCards.Add(Cards[j]);
                }
            }

            if (returnCards.Count == 4)
            {
                return returnCards;
            }
            else
            {
                returnCards.Clear();
            }
        }

        return new List<CardData>();

    }
    private List<CardData> FullHouse(List<CardData> Cards)
    {
        List<CardData> threeOfKind = ThreeOfKind(Cards);
        List<CardData> twoOfKind = OnePair(Cards);
        List<CardData> returnCards = new List<CardData>();
        if (threeOfKind.Count != 0 && twoOfKind.Count != 0)
        {
            returnCards.Concat(threeOfKind);
            returnCards.Concat(twoOfKind);
            return returnCards;
        }

        return new List<CardData>();
    }
    private List<CardData> Flush(List<CardData> Cards)
    {
        List<CardData> returnCards = new List<CardData>();
        for (int i = 0; i < Cards.Count; i++)
        {
            if (Cards[0].CardSuit != Cards[i].CardSuit)
            {
                return new List<CardData>();
            }
            else
            {
             returnCards.Add(Cards[i]);   
            }
        }

        return returnCards;
    }
    private List<CardData> Straight(List<CardData> Cards)
    {
        List<CardData> returnCards = new List<CardData>();
        Cards = Cards.OrderByDescending(x => x.CardNumber).ToList();
        returnCards.Add(Cards[0]);
        for (int i = 1; i < Cards.Count; i++)
        {
            if (Cards[i].CardNumber != Cards[i - 1].CardNumber - 1)
            {
                return new List<CardData>();
            }
            else
            {
                returnCards.Add(Cards[i]);
            }
        }

        return returnCards;
    }
    private List<CardData> ThreeOfKind(List<CardData> Cards)
    {
        Cards = Cards.OrderBy(x => x.CardSuit).ThenByDescending(x => x.CardNumber).ToList();
        List<CardData> returnCards = new List<CardData>();

        for (int i = 0; i < Cards.Count-1; i++)
        {
            returnCards.Add(Cards[i]);
            for (int j = i +1; j < Cards.Count; j++)
            {
                if (Cards[i] == Cards[j])
                {
                    returnCards.Add(Cards[j]);
                }
            }

            if (returnCards.Count != 3)
            {
                returnCards.Clear();
            }
            else
            {
                return returnCards;
            }
        }

        return new List<CardData>();
    }
    private List<CardData> TwoPair(List<CardData> Cards)
    {
        List<CardData> singlePair = OnePair(Cards);
        List<CardData> returnCards = new List<CardData>();
        if (singlePair.Count == 0)
        {
            return new List<CardData>();
        }

        returnCards = OnePair(Cards.Except(singlePair).ToList());


        if (returnCards.Count == 0)
        {
            return new List<CardData>();
        }

        returnCards = returnCards.Concat(singlePair).ToList();
        return returnCards;
    }
    private List<CardData> OnePair(List<CardData> Cards)
    {
        Cards = Cards.OrderByDescending(x => x.CardNumber).ToList();
        List<CardData> returnCards = new List<CardData>();

        for (int i = 0; i < Cards.Count-1; i++)
        {
            for (int j = i + 1; j < Cards.Count; j++)
            {
                if (Cards[i].CardNumber == Cards[j].CardNumber)
                {
                    returnCards.Add(Cards[i]);
                    returnCards.Add(Cards[j]);
                    return returnCards;
                }
            }
        }

        return returnCards;

    }

    private List<CardData> HighCard(List<CardData> Cards)
    {
        Cards = Cards.OrderByDescending(x => x.CardNumber).ToList();
        List<CardData> returnCards = new List<CardData>();
        returnCards.Add(Cards[0]);
        return returnCards;

    }

    
    
    
}
