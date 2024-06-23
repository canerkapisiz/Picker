using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsansarDurum : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator bariyerAlani;

    public void BariyerKaldir()
    {
        bariyerAlani.Play("bariyerKaldir");
    }

    public void Bitti()
    {
        gameManager.toplayiciHaretketDurumu = true;
    }
}
