﻿using UnityEngine;
using UnityEngine.Events;

public class CardAnimation
{
    private readonly Card card;
    private Vector3 position;
    private Quaternion rotation;

    public UnityEvent OnFinished { get; }

    public CardAnimation(Card c, Vector3 pos, Quaternion rot)
    {
        card = c;
        position = pos;
        rotation = rot;
        OnFinished = new UnityEvent();
    }

    public bool Play(float movementSpeed, float rotationSpeed)
    {
        if (Vector3.Distance(card.transform.position, position) < 0.01f)
        {
            OnFinished.Invoke();
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