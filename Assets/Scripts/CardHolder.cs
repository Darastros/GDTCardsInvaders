using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    [SerializeField]
    private float m_length = 5.0f;
    [SerializeField]
    private float m_maxInterval = 1.0f; //max space between 2 cards
    [SerializeField]
    private GameObject m_cardInstancePrefab;

    protected List<GameObject> m_cards;
    
    public virtual void OnCardClicked(GameObject clickedCard)
    { }

    protected void InitHolder()
    {
        m_cards = new List<GameObject>();
    }

    protected void AddCard(CardData card)
    {
        GameObject newCard = Instantiate(m_cardInstancePrefab);
        newCard.GetComponent<CardInstance>().Initialize(card, this);
        m_cards.Add(newCard);
        UpdateCardDisplay();
    }

    protected void RemoveCard(GameObject card)
    {
        m_cards.Remove(card);
        UpdateCardDisplay();
    }

    protected virtual void UpdateCardDisplay()
    {
        float length = Mathf.Min(m_length, (m_cards.Count - 1) * m_maxInterval);
        if(length >= 0.0f)
        {
            float interval = length > Mathf.Epsilon ? length / (m_cards.Count - 1) : 0.0f;
            for (int i = 0; i < m_cards.Count; i++)
            {
                m_cards[i].transform.position = transform.position + new Vector3(i * interval - 0.5f * length, 0.0f);
            }
        }
    }
}
