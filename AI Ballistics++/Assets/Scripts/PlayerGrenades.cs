using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrenades : MonoBehaviour
{
    [SerializeField] private GameObject grenadeGameObject;
    [SerializeField] private Transform target;
    private Grenade currentGrenade;
    private void Start()
    {
        if (currentGrenade == null)
        {
            Invoke("Reload", 3f);
        }
    }

    private void OnThrow() {
        Invoke("Reload", 2f);
    }

    private void Reload()
    {
        currentGrenade = Instantiate(grenadeGameObject, transform).GetComponent<Grenade>();
        currentGrenade.target = target;
        currentGrenade.OnThrow.AddListener(OnThrow);
    }
}
