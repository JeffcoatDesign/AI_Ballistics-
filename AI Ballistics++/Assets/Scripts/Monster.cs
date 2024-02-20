using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    private Animator animator;
    private Vector3 lastPosition;
    private float defaultSpeed;
    public float currentSpeed;
    public float attackRange = 2f;
    public int maxHP = 3;
    public int hp;
    public Collider collider;
    bool isDead = false;
    bool isAttacking = false;
    internal void GetHit()
    {
        if (isDead) return;
        hp--;
        isAttacking = false;
        if (hp > 0)
        {
            SetSpeed(0f);
            animator.SetTrigger("GetHit");
            Invoke("ResetSpeed", 1.5f);
        }
        else Die();
    }

    private void Die()
    {
        isDead = true;
        SetSpeed(0f);
        animator.SetTrigger("Die");
        collider.enabled = false;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        agent.SetDestination(target.position);
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        defaultSpeed = agent.speed;
        hp = maxHP;
    }

    private void Update()
    {
        currentSpeed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        animator.SetFloat("Speed", currentSpeed);
        lastPosition = transform.position;

        if (Vector3.Distance(transform.position, target.position) <= attackRange && !isAttacking)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (isDead) return;
        SetSpeed(0f);
        isAttacking = true;
        animator.SetTrigger("Attack");
        GameManager.Instance.HurtPlayer();
        Invoke("ResetAttack", 2f);
    }

    private void ResetAttack()
    {
        isAttacking = false;
    }

    private void SetSpeed(float speed)
    {
        agent.speed = speed;
    }

    private void ResetSpeed() => agent.speed = defaultSpeed;
}
