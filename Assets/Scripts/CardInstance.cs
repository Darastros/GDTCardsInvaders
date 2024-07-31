using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardInstance : MonoBehaviour, IPointerClickHandler
{
    private CardData m_cardData;
    private CardHolder m_holder;

    public CardData GetCardData() { return m_cardData; }

    public void Initialize(CardData data, CardHolder holder)
    {
        m_cardData = data;
        m_holder = holder;
        GetComponent<SpriteRenderer>().sprite = Sprite.Create(m_cardData.m_cardImage, new Rect(0.0f, 0.0f, m_cardData.m_cardImage.width, m_cardData.m_cardImage.height), new Vector2(0.5f, 0.5f));
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        m_holder.OnCardClicked(gameObject);
    }
}
