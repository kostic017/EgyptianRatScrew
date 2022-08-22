using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardAnimation
{
    private readonly Card card;
    private Vector3 position;
    private Quaternion rotation;

    public CardAnimation(Card c, Vector3 pos, Quaternion rot)
    {
        card = c;
        position = pos;
        rotation = rot;
    }

    public bool Play(float movementSpeed, float rotationSpeed)
    {
        if (Vector3.Distance(card.transform.position, position) < 0.01f)
        {
            card.transform.SetPositionAndRotation(position, rotation);
            return true;
        }

        card.transform.SetPositionAndRotation(
            Vector3.MoveTowards(card.transform.position, position, movementSpeed * Time.deltaTime),
            Quaternion.Lerp(card.transform.rotation, rotation, rotationSpeed * Time.deltaTime)
        );

        return false;
    }
}

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

    public void AddCardAnimation(Card card, Vector3 position)
    {
        AddCardAnimation(card, position, Quaternion.identity);
    }

    public void AddCardAnimation(Card card, Vector3 position, Quaternion rotation)
    {
        cardAnimations.Enqueue(new CardAnimation(card, position, rotation));
        working = true;
    }
}
