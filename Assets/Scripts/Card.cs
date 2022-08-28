using UnityEngine;
using UnityEngine.U2D;

public class Card : MonoBehaviour
{
    [SerializeField]
    private SpriteAtlas atlas;

    private SpriteRenderer spriteRenderer;

    private CardValue cardValue;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetDisplayingOrder(int order)
    {
        spriteRenderer.sortingOrder = order;
    }

    public CardValue Value
    {
        get
        {
            return cardValue;
        }
        set
        {
            cardValue = value;
            gameObject.name = $"{value.Rank.GetDescription()}_of_{value.Suit.GetDescription()}";
            spriteRenderer.sprite = atlas.GetSprite(gameObject.name);
        }
    }
}
