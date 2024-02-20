using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float m_radius = 4f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            Monster monster = collision.collider.GetComponent<Monster>();
            if (monster != null) monster.GetHit();
        }
    }
}
