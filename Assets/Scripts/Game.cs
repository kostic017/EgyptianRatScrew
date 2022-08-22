using System;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
    [SerializeField]
    private GameObject cardPrefab;

    [SerializeField]
    private Transform deck;

    [SerializeField]
    private Transform player1Cards;

    [SerializeField]
    private Transform player2Cards;

    [SerializeField]
    private CardAnimator cardAnimator;

    private Player currentPlayer = Player.Player1;

    private void Start()
    {
        DealCards();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Card card = MouseOverCard();
            if (card != null)
            {
                if (!card.FaceUp)
                {
                    if (card.Player == currentPlayer)
                    {
                        MoveCardToDeck(card);
                        TooglePlayer();
                    }
                }
                else
                {
                    // TODO: handle Player2 slap
                    // TODO: check if slap was valid
                    TakeCardsFromDeck(Player.Player1);
                }
            }    
        }
    }

    private void DealCards()
    {
        List<CardValue> cards = new();
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                cards.Add(new CardValue(suit, rank));

        for (int n = cards.Count - 1; n > 0; --n)
        {
            int k = Random.Range(0, n + 1);
            (cards[k], cards[n]) = (cards[n], cards[k]);
        }

        float zDeck = 0f;
        float zDeal = 0f;
        bool flag = true;
        foreach (var cardValue in cards)
        {
            var go = Instantiate(cardPrefab, deck.position + new Vector3(0f, 0f, zDeck), Quaternion.identity, flag ? player1Cards : player2Cards);
            go.name = $"{cardValue.Rank.GetDescription()}_of_{cardValue.Suit.GetDescription()}";

            var card = go.GetComponent<Card>();
            card.Player = flag ? Player.Player1 : Player.Player2;

            cardAnimator.AddCardAnimation(card, card.transform.parent.position + new Vector3(0f, 0f, zDeal));

            flag = !flag;
            zDeck += Constants.ZOffset;
            zDeal -= Constants.ZOffset;
        }
    }

    private Card MouseOverCard()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit && hit.transform.TryGetComponent<Card>(out var card))
            return card;
        return null;
    }

    private void MoveCardToDeck(Card card)
    {
        float z = 0f;
        if (deck.childCount > 0)
            z = deck.GetChild(deck.childCount - 1).transform.position.z - Constants.ZOffset;

        card.FaceUp = true;
        card.Player = Player.None;
        card.transform.parent = deck;
        cardAnimator.AddCardAnimation(card, deck.position + new Vector3(0f, 0f, z), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
    }

    private void TakeCardsFromDeck(Player player)
    {
        var cardStack = player == Player.Player1 ? player1Cards : player2Cards;
        
        float z = 0f;
        if (cardStack.childCount > 0)
            z = cardStack.GetChild(0).transform.position.z;

        while (deck.childCount > 0)
        {
            var card = deck.GetChild(0).GetComponent<Card>();
            card.FaceUp = false;
            card.Player = player;
            card.transform.parent = cardStack;
            cardAnimator.AddCardAnimation(card, card.transform.parent.position + new Vector3(0f, 0f, z));
            z += Constants.ZOffset;
        }
    }

    private void TooglePlayer()
    {
        if (currentPlayer == Player.Player2)
            currentPlayer = Player.Player1;
        else
            currentPlayer = Player.Player2;
    }

    public void OnAllAnimationsFinished()
    {
    }
}
