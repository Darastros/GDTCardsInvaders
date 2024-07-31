using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField]
    private float m_speed = 5.0f;

    [SerializeField]
    private float m_strength = 50.0f; //deal damage equal to strength

    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 newPos = transform.position + new Vector3(0.0f, m_speed * Time.fixedDeltaTime);
        if (newPos.y > 20.0f)
            Destroy(gameObject);
        transform.position = newPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemyComponent = collision.gameObject.GetComponent<Enemy>();
        if (enemyComponent != null)
        {
            enemyComponent.TakeDamage(m_strength);
            Destroy(gameObject);
        }
    }
}
