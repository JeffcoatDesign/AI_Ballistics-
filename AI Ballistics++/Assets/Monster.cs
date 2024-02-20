using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [SerializeField] private Transform target;
    private NavMeshAgent agent;
    private Animator animator;
    private Vector3 lastPosition;
    public float currentSpeed;
    internal void GetHit()
    {
        Debug.Log("GetHit");
        animator.SetTrigger("GetHit");
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
    }

    private void Update()
    {
        currentSpeed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        animator.SetFloat("Speed", currentSpeed);
        lastPosition = transform.position;
    }
}
