using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class HandManager : MonoBehaviour
{
    public Transform handArea;
    public GameObject cardPrefab;
    public List<CardData> startingDeck;

    public void Start()
    {
        foreach (var card in startingDeck)
        {
            DrawCard(card);
        }
    }

    public void DrawCard(CardData data)
    {
        var cardObj = Instantiate(cardPrefab, handArea);
        var instance = cardObj.GetComponent<CardInstance>();
        instance.Initialize(data);
    }
}
