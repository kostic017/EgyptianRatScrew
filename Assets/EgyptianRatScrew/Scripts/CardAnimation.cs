using UnityEngine;

public class CardAnimation
{
    private Vector3 position;
    private readonly Card card;
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
            card.transform.position = position;
            return true;
        }

        card.transform.SetPositionAndRotation(
            Vector3.MoveTowards(card.transform.position, position, movementSpeed * Time.deltaTime),
            Quaternion.RotateTowards(card.transform.rotation, rotation, rotationSpeed * Time.deltaTime)
        );

        return false;
    }
}