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

    public Transform oyuncuKonumu;  //gameobject de diyebiliriz ama sadece transform position'ý lazým bize

    float atesEtmeAraligi = 0.5f;
    float atesEtmeZamani = 0f;

   


    void Update()           
    {

        if(oyuncuKonumu)
        {
            if (transform.position.x > oyuncuKonumu.position.x)       //burda normalde tam tersi olmasý lazým, bir anormallik var :D 
            {
                transform.Translate(dusmanHareketHizi * Time.deltaTime, 0, 0);
            }
            if (transform.position.x < oyuncuKonumu.position.x)
            {
                transform.Translate(-dusmanHareketHizi * Time.deltaTime, 0, 0);
            }

            float distanceBetweenUs = (transform.position.x - oyuncuKonumu.position.x);         //yuþanýnd dediði update içine koymayalým da performans cart curt ossurduðu ibnenin :D mevzuya güzel bir örnek, bunu update dýþýna al bakim noluyor gör ebeninkini :D :P 
            if (distanceBetweenUs <= 0.2f)
            {
                if (Time.time >= atesEtmeZamani)
                {                                                //bu çok güzel bir blok...ates etme sýklýðýný ayarlýyor,
                    Fire();                                       //burda ilk ateþi edince ateþ etme zamaný artýyor,
                    atesEtmeZamani = Time.time + atesEtmeAraligi;   // normal zamanýmýz o (ates etme zamaný)na gelene kadar ateþ edemiyor
                }

            }
        }
       
        
    }


    private void Fire()
    {
        GameObject yeniKursun = Instantiate(dusmanMermisi, transform.position, Quaternion.identity);    //bu transform position ve quaternion olmazsa eðer, player hareket ettiðinde mermiler hep orta noktadan çýkar player'ýn olduðu yerden deðil
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
