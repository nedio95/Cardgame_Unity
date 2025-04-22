using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInstance : MonoBehaviour
{
    public CardData cardData;

    public Image artworkImage;
    public TMP_Text nameText;
    public TMP_Text costText;

    public void Initialize(CardData data)
    {
        cardData = data;
        nameText.text = data.cardName;
        costText.text = data.cost.ToString();
        artworkImage.sprite = data.artwork;
    }

    public void PlayCard(Vector2Int targetPosition)
    {
        cardData.effect?.ApplyEffect(this, targetPosition);
        //Destroy(gameObject); // For now
    }
}
