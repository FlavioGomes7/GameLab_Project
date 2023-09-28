using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivarCollider : MonoBehaviour
{
    public Collider colliderParaAtivar;

    public void Ativar()
    {
        colliderParaAtivar.enabled = true;
    }
    public void Desativar()
    {
        colliderParaAtivar.enabled = false;
    }
}
