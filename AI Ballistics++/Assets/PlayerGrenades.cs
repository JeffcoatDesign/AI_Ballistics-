using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrenades : MonoBehaviour
{
    [SerializeField] private GameObject grenadeGameObject;
    [SerializeField] private Transform target;
    private Grenade currentGrenade;
    [SerializeField] int numGrenades = 4;
    private void Start()
    {
        if (currentGrenade == null)
        {
            Reload();
        }
    }

    private void OnThrow() {
        if (numGrenades > 0) { Reload(); }
    }

    private void Reload()
    {
        numGrenades--;
        currentGrenade = Instantiate(grenadeGameObject, transform).GetComponent<Grenade>();
        currentGrenade.target = target;
        currentGrenade.OnExplode.AddListener(OnThrow);
    }
}
