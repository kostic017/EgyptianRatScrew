using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
    [SerializeField]
    private GameObject cardPrefab;

    [SerializeField]
    private Transform discardPile;

    [SerializeField]
    private Transform player1Cards;

    [SerializeField]
    private Transform player2Cards;

    [SerializeField]
    private Button player1Button;

    [SerializeField]
    private Button player2Button;

    [SerializeField]
    private GameObject popup;

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
        if (Input.GetMouseButtonDown(0) && !IsPointerOverGameObject())
        {
            if (!gameDataManager.IsDiscardPileEmpty())
            {
                Slap();
            }
        }
    }

    public void PlayCard()
    {
        var cardValue = gameDataManager.DrawPlayerCard(currentPlayer);
        var cardSpawnPoint = currentPlayer == Player.Player1 ? player1Cards : player2Cards;

        var go = Instantiate(cardPrefab, cardSpawnPoint.position, Quaternion.identity, discardPile);
        go.name = $"{cardValue.Rank.GetDescription()}_of_{cardValue.Suit.GetDescription()}";

        var card = go.GetComponent<Card>();
        card.Value = cardValue;
        card.SetDisplayingOrder(discardPile.childCount);
        gameDataManager.PutCardInDiscardPile(cardValue);

        cardAnimator.AddAnimation(card, discardPile.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        TogglePlayer();
        UpdateButtons();
    }

    private void Slap()
    {
        var slapCombination = gameDataManager.GetSlapCombination();
        
        if (Input.mousePosition.y < Screen.height * 0.5f)
        {
            if (slapCombination != SlapCombination.None)
                TakeCards(Player.Player1);
            else
                TakeCards(Player.Player2);
        }
        else
        {
            if (slapCombination != SlapCombination.None)
                TakeCards(Player.Player2);
            else
                TakeCards(Player.Player1);
        }

        if (gameDataManager.GetPlayerCardCount(Player.Player1) == 0)
        {
            ShowPopup("Player2 Wins!");
            Invoke(nameof(Restart), 2f);
        }
        else if (gameDataManager.GetPlayerCardCount(Player.Player2) == 0)
        {
            ShowPopup("Player1 Wins!");
            Invoke(nameof(Restart), 2f);
        }
        else
        {
            ShowPopup(slapCombination.GetDescription());
        }
    }

    private void TakeCards(Player player)
    {
        var cardStack = player == Player.Player1 ? player1Cards : player2Cards;

        for (int i = discardPile.childCount - 1; i >= 0; --i)
        {
            var card = discardPile.GetChild(i).GetComponent<Card>();
            card.transform.parent = cardStack;
            gameDataManager.PopCardFromDiscardPile();
            gameDataManager.GiveCardToPlayer(player, card.Value);
            cardAnimator.AddAnimation(card, cardStack.position);
        }

        currentPlayer = player;
        UpdateButtons();
    }

    private void TogglePlayer()
    {
        if (gameDataManager.GetPlayerCardCount(Player.Player1) == 0)
            currentPlayer = Player.Player2;
        else if (gameDataManager.GetPlayerCardCount(Player.Player2) == 0)
            currentPlayer = Player.Player1;
        else if (currentPlayer == Player.Player1)
            currentPlayer = Player.Player2;
        else if (currentPlayer == Player.Player2)
            currentPlayer = Player.Player1;
    }

    private void UpdateButtons()
    {
        var player1Count = gameDataManager.GetPlayerCardCount(Player.Player1);
        var player2Count = gameDataManager.GetPlayerCardCount(Player.Player2);
        
        player1Button.interactable = player1Count != 0 && currentPlayer == Player.Player1;
        player2Button.interactable = player2Count != 0 && currentPlayer == Player.Player2;
        
        player1Button.GetComponentInChildren<TMP_Text>().text = $"Play card ({player1Count})";
        player2Button.GetComponentInChildren<TMP_Text>().text = $"Play card ({player2Count})";
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

    private bool IsPointerOverGameObject()
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
        foreach (Transform card in player1Cards)
            Destroy(card.gameObject);
        foreach (Transform card in player2Cards)
            Destroy(card.gameObject);
    }
}
