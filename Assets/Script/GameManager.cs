using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using static UnityEditor.Progress;

[Serializable]
public class TopAlaniTeknikIslemler
{
    public Animator topAlaniAsansor;
    public TextMeshProUGUI sayiText;
    public int atilmasiGerekenTop;
    public GameObject[] toplar;
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject toplayiciObje;
    [SerializeField] private GameObject topKontrolObjesi;
    public bool toplayiciHaretketDurumu;

    int atilanTopSayisi;
    int toplamCheckPointSayisi;
    int mevcutCheckPoint;
    float parmakPozX;
    [SerializeField] private List<TopAlaniTeknikIslemler> topAlaniTeknikIslemler = new List<TopAlaniTeknikIslemler>();

    void Start()
    {
        toplayiciHaretketDurumu = true;

        for (int i = 0; i < topAlaniTeknikIslemler.Count; i++)
        {
            topAlaniTeknikIslemler[i].sayiText.text = atilanTopSayisi + "/" + topAlaniTeknikIslemler[i].atilmasiGerekenTop;
        }
        toplamCheckPointSayisi = topAlaniTeknikIslemler.Count - 1;
    }

    void Update()
    {
        if (toplayiciHaretketDurumu)
        {
            toplayiciObje.transform.position += 5f * Time.deltaTime * toplayiciObje.transform.forward;

            if (Time.timeScale != 0)
            {
                if(Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    Vector3 TouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));

                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            parmakPozX = TouchPosition.x - toplayiciObje.transform.position.x;
                            break;
                        case TouchPhase.Moved:
                            if(TouchPosition.x-parmakPozX > -1.15 && TouchPosition.x -parmakPozX < 1.15)
                            {
                                toplayiciObje.transform.position = Vector3.Lerp(toplayiciObje.transform.position,
                                    new Vector3(TouchPosition.x - parmakPozX, toplayiciObje.transform.position.y,
                                    toplayiciObje.transform.position.z), 3f);
                            }
                            break;
                    }
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    toplayiciObje.transform.position = Vector3.Lerp(toplayiciObje.transform.position,
                        new Vector3(toplayiciObje.transform.position.x - 0.1f, toplayiciObje.transform.position.y,
                        toplayiciObje.transform.position.z), 0.05f);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    toplayiciObje.transform.position = Vector3.Lerp(toplayiciObje.transform.position,
                        new Vector3(toplayiciObje.transform.position.x + 0.1f, toplayiciObje.transform.position.y,
                        toplayiciObje.transform.position.z), 0.05f);
                }
            }
        }
    }

    public void SiniraGeldi()
    {
        toplayiciHaretketDurumu = false;
        Invoke("AsamaKontrol", 2f);
        Collider[] hitColl = Physics.OverlapBox(topKontrolObjesi.transform.position, topKontrolObjesi.transform.localScale / 2, Quaternion.identity);

        int i = 0;
        while (i < hitColl.Length)
        {
            hitColl[i].GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 0.8f), ForceMode.Impulse);
            i++;
        }
    }

    public void ToplariSay()
    {
        atilanTopSayisi++;
        topAlaniTeknikIslemler[mevcutCheckPoint].sayiText.text = atilanTopSayisi + "/" + topAlaniTeknikIslemler[mevcutCheckPoint].atilmasiGerekenTop;
    }

    void AsamaKontrol()
    {
        if (atilanTopSayisi >= topAlaniTeknikIslemler[mevcutCheckPoint].atilmasiGerekenTop)
        {
            topAlaniTeknikIslemler[mevcutCheckPoint].topAlaniAsansor.Play("asansor");
            foreach (var item in topAlaniTeknikIslemler[mevcutCheckPoint].toplar)
            {
                item.SetActive(false);
            }

            if (mevcutCheckPoint == toplamCheckPointSayisi)
            {
                Debug.Log("OYUN BITTI");
                Time.timeScale = 0;
            }
            else
            {
                mevcutCheckPoint++;
                atilanTopSayisi = 0;
            }
        }
        else
        {
            Debug.Log("Kaybettin");
        }
    }
}
