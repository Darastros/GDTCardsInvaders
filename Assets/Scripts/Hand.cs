using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : CardHolder
{
    [SerializeField]
    private int m_handsize = 3;

    [SerializeField]
    private GameObject m_cursor; //indicate from where you play card (start position of shots)

    private int m_selectedCard; //index in m_cards, -1 if no card selected
    private Deck m_playerDeck;
    public void InitHand(Deck playerDeck)
    {
        InitHolder();
        m_playerDeck = playerDeck;
        m_cursor.transform.position = new Vector3(100.0f, 100.0f);
        m_selectedCard = -1;
        RefillHand();
    }

    private void RefillHand()
    {
        bool reshuffledDeck = false; //for safety
        while (m_cards.Count < m_handsize)
        {
            CardData drawnCard = m_playerDeck.DrawCard();
            if (drawnCard == null)
            {
                if (m_cards.Count == 0 && !reshuffledDeck)
                {
                    m_playerDeck.reshuffle();
                    reshuffledDeck = true;
                }
                else
                    return;
            }
            else
            {
                AddCard(drawnCard);
            }
        }
    }

    protected override void UpdateCardDisplay()
    {
        base.UpdateCardDisplay();
        if (m_selectedCard >= 0)
            m_cards[m_selectedCard].transform.position += new Vector3(0.0f, 0.5f);
    }

    public override void OnCardClicked(GameObject clickedCard)
    {
        m_selectedCard = m_cards.FindIndex(card => card == clickedCard);
        UpdateCardDisplay();
    }

    private void Update()
    {
        if(m_selectedCard >= 0)
        {
            Vector3 cursorPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, -2.25f);
            m_cursor.transform.position = cursorPos;
            if (Input.GetMouseButtonDown(0))
            {
                GameObject selectedCard = m_cards[m_selectedCard];
                selectedCard.GetComponent<CardInstance>().GetCardData().Resolve(cursorPos);
                m_selectedCard = -1;
                m_cursor.transform.position = new Vector3(100.0f, 100.0f);
                m_playerDeck.AddToDiscard(selectedCard.GetComponent<CardInstance>().GetCardData());
                RemoveCard(selectedCard);
                Destroy(selectedCard);
                RefillHand();
            }
        }
    }
}
