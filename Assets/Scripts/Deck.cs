using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Deck")]
public class Deck : ScriptableObject
{
    [SerializeField]
    private List<CardData> m_cards;

    private List<CardData> m_drawPile;
    private List<CardData> m_discardPile;

    public void InitDeck()
    {
        m_drawPile = new List<CardData>();
        m_discardPile = new List<CardData>();
        foreach(CardData card in m_cards)
        {
            m_drawPile.Add(card);
        }
        reshuffle();
    }

    public CardData DrawCard()
    {
        CardData drawnCard = null;
        if (m_drawPile.Count > 0)
        {
            drawnCard = m_drawPile[0];
            m_drawPile.RemoveAt(0);
        }
        return drawnCard;
    }

    public void AddToDiscard(CardData card)
    {
        m_discardPile.Add(card);
    }

    public void reshuffle()
    {
        foreach (CardData card in m_discardPile)
        {
            m_drawPile.Add(card);
        }
        m_discardPile.Clear();

        List<CardData> newDrawPile = new List<CardData>();

        int deckSize = m_drawPile.Count;
        for (int i = 0; i < deckSize; i++)
        {
            int randomIndex = Random.Range(0, m_drawPile.Count);
            newDrawPile.Add(m_drawPile[randomIndex]);
            m_drawPile.RemoveAt(randomIndex);
        }
        m_drawPile = newDrawPile;
    }
}
