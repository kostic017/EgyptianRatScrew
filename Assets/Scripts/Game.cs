using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
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
    private Button player1Button;

    [SerializeField]
    private Button player2Button;

    [SerializeField]
    private CardAnimator cardAnimator;

    private readonly GameDataManager gameDataManager = new();

    private Player currentPlayer = Player.Player1;

    private void Start()
    {
        gameDataManager.DealCards();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.mousePosition.y < Screen.height * 0.5f)
            {
                TakeCards(Player.Player1);
            }
            else
            {
                TakeCards(Player.Player2);
            }
        }
    }

    public void PlayCard()
    {
        var cardValue = gameDataManager.DrawCard(currentPlayer);
        var cardStack = currentPlayer == Player.Player1 ? player1Cards : player2Cards;

        var go = Instantiate(cardPrefab, cardStack.position, Quaternion.identity, deck);
        go.name = $"{cardValue.Rank.GetDescription()}_of_{cardValue.Suit.GetDescription()}";

        var card = go.GetComponent<Card>();
        card.FaceUp = true;
        card.Value = cardValue;
        card.SetDisplayingOrder(deck.childCount);

        cardAnimator.AddAnimation(card, deck.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        TogglePlayer();
        UpdateButtons();
    }

    private void TakeCards(Player player)
    {
        var cardStack = player == Player.Player1 ? player1Cards : player2Cards;

        for (int i = deck.childCount - 1; i >= 0; --i)
        {
            var card = deck.GetChild(i).GetComponent<Card>();
            card.transform.parent = cardStack;
            gameDataManager.AddCard(player, card.Value);
            cardAnimator.AddAnimation(card, cardStack.position).AddListener(() => Destroy(card.gameObject));
        }

        currentPlayer = player;
        UpdateButtons();
    }

    private void TogglePlayer()
    {
        if (gameDataManager.GetCardCount(Player.Player1) == 0)
            currentPlayer = Player.Player2;
        else if (gameDataManager.GetCardCount(Player.Player2) == 0)
            currentPlayer = Player.Player1;
        else if (currentPlayer == Player.Player1)
            currentPlayer = Player.Player2;
        else if (currentPlayer == Player.Player2)
            currentPlayer = Player.Player1;
    }

    private void UpdateButtons()
    {
        var player1Count = gameDataManager.GetCardCount(Player.Player1);
        var player2Count = gameDataManager.GetCardCount(Player.Player2);
        
        player1Button.interactable = player1Count != 0 && currentPlayer == Player.Player1;
        player2Button.interactable = player2Count != 0 && currentPlayer == Player.Player2;
        
        player1Button.GetComponentInChildren<TMP_Text>().text = $"Play card ({player1Count})";
        player2Button.GetComponentInChildren<TMP_Text>().text = $"Play card ({player2Count})";
    }

    public void OnAllAnimationsFinished()
    {
    }
}
