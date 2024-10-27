using UnityEngine;
using TMPro;
public enum EGameStep
{
    Intro,
    Game,
    GameOver
}

public class GameManager : FiniteStateMachine<EGameStep>
{
    #region Singleton
    private static GameManager instance = null;
    public static GameManager Instance => instance;
    private void Awake()
    {
        instance = this;
        RequestStep(EGameStep.Intro);
    }
    #endregion

    [SerializeField]
    private float m_playerHealth = 100.0f;

    [SerializeField]
    private Deck m_playerDeck;
    [SerializeField]
    private Hand m_playerHand;

    [SerializeField]
    private GameObject m_enemiesRoot;

    [SerializeField]
    private TMP_Text m_healthText;
    [SerializeField]
    private GameObject m_gameOverScreen;

    #region FSM
    public void Intro_Enter()
    {
        GetComponent<CinematicManager>().RunCinematic(() => RequestStep(EGameStep.Game));
    }

    public void Game_Enter()
    {
        InitGame();
    }
    #endregion


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
