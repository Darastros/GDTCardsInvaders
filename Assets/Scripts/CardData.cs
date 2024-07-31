using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/default")]
public class CardData : ScriptableObject
{
    public string m_cardName;
    public Texture2D m_cardImage;
    public string m_cardText;

    public virtual void Resolve(Vector3 cursorPosition) {}
}
