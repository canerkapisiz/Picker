using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haberci : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("toplayiciSinirObjesi"))
        {
            gameManager.SiniraGeldi();
        }
    }
}
