using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardAnimator : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 20f;

    [SerializeField]
    private float rotationSpeed = 10f;

    [SerializeField]
    private UnityEvent OnAllAnimationsFinished = new();

    private bool working;
    private CardAnimation currentCardAnimation;
    private readonly Queue<CardAnimation> cardAnimations = new();

    private void Update()
    {
        if (currentCardAnimation == null)
        {
            NextAnimation();
        }
        else
        {
            if (currentCardAnimation.Play(movementSpeed, rotationSpeed))
            {
                NextAnimation();
            }
        }
    }

    private void NextAnimation()
    {
        currentCardAnimation = null;

        if (cardAnimations.Count > 0)
        {
            currentCardAnimation = cardAnimations.Dequeue();
        }
        else
        {
            if (working)
            {
                working = false;
                OnAllAnimationsFinished.Invoke();
            }
        }
    }

    public UnityEvent AddAnimation(Card card, Vector3 position)
    {
        return AddAnimation(card, position, Quaternion.identity);
    }

    public UnityEvent AddAnimation(Card card, Vector3 position, Quaternion rotation)
    {
        working = true;
        var cardAnimation = new CardAnimation(card, position, rotation);
        cardAnimations.Enqueue(cardAnimation);
        return cardAnimation.OnFinished;
    }
}
