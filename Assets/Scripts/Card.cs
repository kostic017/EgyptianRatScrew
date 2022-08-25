using UnityEngine;
using UnityEngine.U2D;

public class Card : MonoBehaviour
{
    [SerializeField]
    private SpriteAtlas atlas;

    private SpriteRenderer spriteRenderer;

    public CardValue Value { get; set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.sprite = atlas.GetSprite(gameObject.name);
    }

    public void SetDisplayingOrder(int order)
    {
        spriteRenderer.sortingOrder = order;
    }
}
