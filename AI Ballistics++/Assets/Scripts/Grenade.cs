using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grenade : MonoBehaviour
{
    public UnityEvent OnThrow = new();
    public UnityEvent OnExplode = new();
    public float force = 10f;
    public Transform pin;
    public Transform target;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float explosionTime;
    Animator animator;
    Rigidbody rb;
    bool isThrown = false;
    bool isPinOut = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("PullPin");
        }
        else if (isPinOut && !isThrown && !Input.GetButton("Fire1"))
            ThrowGrenade();
    }

    private void ThrowGrenade()
    {
        if (isThrown) { return; }
        OnThrow.Invoke();
        isThrown = true;
        float distance = Vector3.Distance(transform.position, target.position);
        float modifiedForce = force;
        bool useMin = distance > 10f;
        if (useMin) { modifiedForce = distance; }
        Vector3? solution = FiringSolution.Calculate(transform.position, target.position, modifiedForce, useMin);


        rb.isKinematic = false;
        if (solution.HasValue)
            rb.AddForce(solution.Value.normalized * modifiedForce, ForceMode.VelocityChange);
        else
            Debug.Log("Not working");

        Invoke("Explode", explosionTime);
    }
    
    private void Explode()
    {
        OnExplode.Invoke();
        Instantiate(explosionPrefab,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }

    public void DropPin()
    {
        isPinOut = true;
        pin.parent = null;
    }
}
