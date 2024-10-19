using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float m_jumpCooldown = 1.0f; //time between jumps

    [SerializeField]
    private float m_jumpLength = 5.0f;

    [SerializeField]
    private float m_direction = -1.0f;

    [SerializeField]
    private Rect m_boundaries = new Rect();

    [SerializeField]
    private float m_health = 100.0f;

    [SerializeField]
    private float m_strength = 20.0f; //deal damage equal to strength

    private float m_timeToJumpNext = 0.0f;

    void Start()
    {
        m_timeToJumpNext = Time.time + m_jumpCooldown;
    }

    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (Time.time >= m_timeToJumpNext)
        {
            Vector3 newPos = transform.position + new Vector3(m_jumpLength * m_direction, 0);

            var outOfLeftBound = newPos.x < m_boundaries.x;
            var outOfRightBound = newPos.x > m_boundaries.x + m_boundaries.width;
            if (outOfLeftBound || outOfRightBound)
            {
                var extraLength = m_boundaries.x - newPos.x;
                if (outOfRightBound)
                    extraLength += m_boundaries.width;
                newPos.x += 2f * extraLength;
                newPos.y -= m_jumpLength;

                m_direction *= -1f;
            }

            if (newPos.y < m_boundaries.y - m_boundaries.height)
            {
                GameManager.Instance.PlayerTakeDamage(m_strength);
                Destroy(gameObject);
            }

            transform.position = newPos;

            m_timeToJumpNext = Time.time + m_jumpCooldown;
        }
    }

    public void TakeDamage(float amount)
    {
        m_health -= amount;
        UpdateHealthDisplay();
        if (m_health <= 0.0f)
            Destroy(gameObject);
    }

    private void UpdateHealthDisplay()
    {
        GetComponent<SpriteRenderer>().color = new Color(1.0f, m_health / 100.0f, m_health / 100.0f);
    }
}
