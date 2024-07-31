using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    /////////////SINGLETON IMPLEMENTATION/////////////////
    private static GameManager instance = null;
    public static GameManager Instance => instance;
    private void Awake()
    {
        instance = this;
        InitGame();
    }
    ///////////////////////////////////////////////////////
    ///

    [SerializeField]
    private float m_playerHealth = 100.0f;

    [SerializeField]
    private Deck m_playerDeck;
    [SerializeField]
    private Hand m_playerHand;

    [SerializeField]
    private TMP_Text m_healthText;
    [SerializeField]
    private GameObject m_gameOverScreen;


    void InitGame()
    {
        m_playerDeck.InitDeck();
        m_playerHand.InitHand(m_playerDeck);
        UpdateHealthDisplay();
    }

    public void PlayerTakeDamage(float amount)
    {
        m_playerHealth -= amount;
        UpdateHealthDisplay();
        if (m_playerHealth <= 0.0f)
            OnLoose();
    }

    private void UpdateHealthDisplay()
    {
        m_healthText.text = "Health : " + ((int)m_playerHealth).ToString();
    }

    private void OnLoose()
    {
        Time.timeScale = 0;
        m_gameOverScreen.SetActive(true);
    }
}
