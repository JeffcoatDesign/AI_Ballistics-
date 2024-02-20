using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Frog : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    [SerializeField] private float m_force = 5f;
    [SerializeField] private bool useMin = true;
    private Rigidbody m_rb;
    private Vector3 spawn;
    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();

        spawn = transform.position;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Vector3? force = FiringSolution.Calculate(transform.position, m_target.position, m_force, useMin);
            if (force.HasValue) m_rb.AddForce(force.Value.normalized * m_force, ForceMode.VelocityChange);
        }
        else if (Input.GetKeyDown(KeyCode.R)) {
            Respawn();
        }
    }
    public void SetUseMin(bool useMin)
    {
        this.useMin = useMin;
    }
    public void Respawn()
    {
        transform.position = spawn;
        m_rb.velocity = Vector3.zero;
        m_rb.angularVelocity = Vector3.zero;
    }
}
