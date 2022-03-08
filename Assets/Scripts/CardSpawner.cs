using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject CardPreFab;
    public GameObject _card;
    public GameObject[] _cards;
    public int _cardCount = 0;

    void Start()
    {
        SpawnCard();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SpawnCard()
    {
        _card = Instantiate(CardPreFab);
        _card.transform.SetParent(transform);
        Card script = _card.GetComponent<Card>();
        script.SetCard((Card.Suit)Random.Range(0, 3), Random.Range(1, 13));
        _cardCount++;
    }
}
