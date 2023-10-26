using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject patlamaEfekti;
    public GameObject dusmanMermisi;
    public Manager manager;
    public Image dusmanCanBar;

    private float currentHealthOfEnemy = 100.0f;
    private float totalHealthOfEnemy = 100.0f;
   
    float dusmanHareketHizi = 2.0f;
    float dusmanMermiHizi = 200f;

    public Transform oyuncuKonumu;  //gameobject de diyebiliriz ama sadece transform position'� laz�m bize

    float atesEtmeAraligi = 0.5f;
    float atesEtmeZamani = 0f;

   


    void Update()           
    {

        if(oyuncuKonumu)
        {
            if (transform.position.x > oyuncuKonumu.position.x)       //burda normalde tam tersi olmas� laz�m, bir anormallik var :D 
            {
                transform.Translate(dusmanHareketHizi * Time.deltaTime, 0, 0);
            }
            if (transform.position.x < oyuncuKonumu.position.x)
            {
                transform.Translate(-dusmanHareketHizi * Time.deltaTime, 0, 0);
            }

            float distanceBetweenUs = (transform.position.x - oyuncuKonumu.position.x);         //yu�an�nd dedi�i update i�ine koymayal�m da performans cart curt ossurdu�u ibnenin :D mevzuya g�zel bir �rnek, bunu update d���na al bakim noluyor g�r ebeninkini :D :P 
            if (distanceBetweenUs <= 0.2f)
            {
                if (Time.time >= atesEtmeZamani)
                {                                                //bu �ok g�zel bir blok...ates etme s�kl���n� ayarl�yor,
                    Fire();                                       //burda ilk ate�i edince ate� etme zaman� art�yor,
                    atesEtmeZamani = Time.time + atesEtmeAraligi;   // normal zaman�m�z o (ates etme zaman�)na gelene kadar ate� edemiyor
                }

            }
        }
       
        
    }


    private void Fire()
    {
        GameObject yeniKursun = Instantiate(dusmanMermisi, transform.position, Quaternion.identity);    //bu transform position ve quaternion olmazsa e�er, player hareket etti�inde mermiler hep orta noktadan ��kar player'�n oldu�u yerden de�il
        yeniKursun.GetComponent<Rigidbody2D>().AddForce(Vector2.down * dusmanMermiHizi);
        Destroy(yeniKursun, 5f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "oyuncu_mermi")
        {
            enemyTakesDamage(10f);
        }

    }
    private void enemyTakesDamage(float amount)
    {
        currentHealthOfEnemy -= amount;
        dusmanCanBar.fillAmount = currentHealthOfEnemy / totalHealthOfEnemy;
        if (currentHealthOfEnemy <= 0)
        {

            enemyGotDestroyed();
        }
    }


    void enemyGotDestroyed()
    {
        Destroy(gameObject, 1f);

        GameObject patlama = Instantiate(patlamaEfekti, transform.position, Quaternion.identity);
        Destroy(patlama, 1f);

        manager.showPanel("KAZANDIN");


    }
}
