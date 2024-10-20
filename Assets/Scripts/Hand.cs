using UnityEngine;

public class Hand : CardHolder
{
    [SerializeField]
    private int m_handsize = 3;

    [SerializeField, Tooltip("Indicates from where you play card (start position of shots)")]
    private Cursor m_cursor;

    private int m_selectedCard; //index in m_cards, -1 if no card selected
    private Deck m_playerDeck;

    public void InitHand(Deck playerDeck)
    {
        InitHolder();
        m_playerDeck = playerDeck;
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

    public override void OnCardClicked(GameObject clickedCard) //doesn't work anymore, clash with onCardHold => fix : add a slight delay before considering a card "hold"
    {
        m_selectedCard = m_cards.FindIndex(card => card == clickedCard);
        UpdateCardDisplay();
    }

    public override void OnCardHold(GameObject HoldCard)
    {
        m_selectedCard = m_cards.FindIndex(card => card == HoldCard);
        UpdateCardDisplay();
    }

    private void Update()
    {
        if(m_selectedCard >= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_cursor.Hold();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                GameObject selectedCard = m_cards[m_selectedCard];
                selectedCard.GetComponent<CardInstance>().GetCardData().Resolve(m_cursor.transform.position);
                m_cursor.Release();

                m_selectedCard = -1;
                m_playerDeck.AddToDiscard(selectedCard.GetComponent<CardInstance>().GetCardData());
                RemoveCard(selectedCard);
                Destroy(selectedCard);
                RefillHand();
            }
        }
    }
}
