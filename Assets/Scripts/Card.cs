using UnityEngine;
using UnityEngine.U2D;

public class Card : MonoBehaviour
{
    [SerializeField]
    private SpriteAtlas atlas;

    private bool faceUp;
    
    public Player Player { get; set; }

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
