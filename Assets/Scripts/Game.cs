using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
    [SerializeField]
    private Card cardPrefab;

    [SerializeField]
    private Transform discardPilePoint;

    [SerializeField]
    private Transform localCardSpawnPoint;

    [SerializeField]
    private Transform remoteCardSpawnPoint;

    [SerializeField]
    private Button player1PlayCardButton;

    [SerializeField]
    private Button player2PlayCardButton;

    [SerializeField]
    private GameObject popup;

    [SerializeField]
    private CardAnimator cardAnimator;

    private Player currentPlayer;
    private readonly Player localPlayer = new("Player 1");
    private readonly Player remotePlayer = new("Player 2");

    private readonly DiscardPile discardPile = new();

    private void Awake()
    {
        DealCards();
        currentPlayer = localPlayer;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUI())
            if (discardPile.CardCount > 0)
                Slap();
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

        bool flag = true;
        foreach (var card in cards)
        {
            if (flag)
                localPlayer.AddCard(card);
            else
                remotePlayer.AddCard(card);
            flag = !flag;
        }
    }

    public void PlayCard()
    {
        var cardValue = currentPlayer.GetCard();
        var cardSpawnPoint = currentPlayer == localPlayer ? localCardSpawnPoint : remoteCardSpawnPoint;

        var card = Instantiate(cardPrefab, cardSpawnPoint.position, Quaternion.identity, discardPilePoint);
        card.gameObject.name = $"{cardValue.Rank.GetDescription()}_of_{cardValue.Suit.GetDescription()}";
        card.SetDisplayingOrder(discardPile.CardCount);

        discardPile.AddCard(cardValue);
        cardAnimator.AddAnimation(card, discardPilePoint.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        
        TogglePlayer();
        UpdateButtons();
    }

    private void Slap()
    {
        var slapCombination = discardPile.GetSlapCombination();
        
        if (Input.mousePosition.y < Screen.height * 0.5f)
        {
            if (slapCombination != SlapCombination.None)
                TakeCardsFromDiscardPile(localPlayer);
            else
                TakeCardsFromDiscardPile(remotePlayer);
        }
        else
        {
            if (slapCombination != SlapCombination.None)
                TakeCardsFromDiscardPile(remotePlayer);
            else
                TakeCardsFromDiscardPile(localPlayer);
        }

        if (localPlayer.CardCount == 0)
        {
            ShowPopup($"{remotePlayer.Name} Wins!");
            Invoke(nameof(Restart), 2f);
        }
        else if (remotePlayer.CardCount == 0)
        {
            ShowPopup($"{localPlayer.Name} Wins!");
            Invoke(nameof(Restart), 2f);
        }
        else
        {
            ShowPopup(slapCombination.GetDescription());
        }
    } 

    private void TakeCardsFromDiscardPile(Player player)
    {
        var cardStack = player == localPlayer ? localCardSpawnPoint : remoteCardSpawnPoint;

        for (int i = discardPilePoint.childCount - 1; i >= 0; --i)
        {
            var card = discardPilePoint.GetChild(i).GetComponent<Card>();
            card.transform.parent = cardStack;
            player.AddCard(discardPile.GetTopCard());
            cardAnimator.AddAnimation(card, cardStack.position);
        }

        currentPlayer = player;
        UpdateButtons();
    }

    private void TogglePlayer()
    {
        if (localPlayer.CardCount == 0)
            currentPlayer = remotePlayer;
        else if (remotePlayer.CardCount == 0)
            currentPlayer = localPlayer;
        else if (currentPlayer == localPlayer)
            currentPlayer = remotePlayer;
        else if (currentPlayer == remotePlayer)
            currentPlayer = localPlayer;
    }

    private void UpdateButtons()
    {
        int player1Count = localPlayer.CardCount;
        player1PlayCardButton.interactable = player1Count != 0 && currentPlayer == localPlayer;
        player1PlayCardButton.GetComponentInChildren<TMP_Text>().text = $"Play card ({player1Count})";
        
        if (player2PlayCardButton != null)
        {
            int player2Count = remotePlayer.CardCount;
            player2PlayCardButton.GetComponentInChildren<TMP_Text>().text = $"Play card ({player2Count})";
            player2PlayCardButton.interactable = player2Count != 0 && currentPlayer == remotePlayer;
        }
    }

    private void ShowPopup(string text)
    {
        popup.SetActive(true);
        foreach (var t in popup.GetComponentsInChildren<TMP_Text>())
            t.text = text;
        Invoke(nameof(HidePopup), 2f);
    }

    private void HidePopup()
    {
        popup.SetActive(false);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private bool IsPointerOverUI()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return true;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return true;

        return false;
    }

    public void OnAllAnimationsFinished()
    {
        foreach (Transform card in localCardSpawnPoint)
            Destroy(card.gameObject);
        foreach (Transform card in remoteCardSpawnPoint)
            Destroy(card.gameObject);
    }
}
