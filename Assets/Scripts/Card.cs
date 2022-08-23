using UnityEngine;
using UnityEngine.U2D;

public class Card : MonoBehaviour
{
    [SerializeField]
    private SpriteAtlas atlas;

    private bool faceUp;

    private SpriteRenderer spriteRenderer;

    public CardValue Value { get; set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetDisplayingOrder(int order)
    {
        spriteRenderer.sortingOrder = order;
    }

    public bool FaceUp
    {
        get
        {
            return faceUp;
        }
        set
        {
            faceUp = value;
            spriteRenderer.sprite = faceUp
                ? atlas.GetSprite(gameObject.name)
                : atlas.GetSprite(Constants.CardBack);
        }
    }
}
