using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float m_radius = 4f;

    private void Start()
    {
        CheckForMonsters();
        Destroy(gameObject, 1f);
    }

    private void CheckForMonsters()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_radius);

        if (colliders.Length < 1) return;
        foreach (Collider collider in colliders)
        {
            Monster monster = collider.GetComponent<Monster>();
            if (monster != null)
            {
                monster.GetHit();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, m_radius);
    }
}
