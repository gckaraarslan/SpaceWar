using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class oyuncu : MonoBehaviour
{
    public GameObject patlamaEfekti;
    public GameObject oyuncuMermisi;
    float mermiHizi = 500f;
    public Image oyuncuCanBar;

    private float currentHealthOfPlayer=100.0f;
    private float totalHealthOfPlayer= 100.0f;
    
    float oyuncuHareketHizi = 3f;
    public Manager manager;



    private void Fire()
    {           //bu yenikursun objesini yapmazsak eðer, destroy komutunu çalýþtýrýnca hata veriyor (Destroying assets is not permitted to avoid data loss.) diyor
        GameObject yeniKursun = Instantiate(oyuncuMermisi,transform.position,Quaternion.identity);    //bu transform position ve quaternion olmazsa eðer, player hareket ettiðinde mermiler hep orta noktadan çýkar player'ýn olduðu yerden deðil
        yeniKursun.GetComponent<Rigidbody2D>().AddForce(Vector2.up*mermiHizi);
       Destroy(yeniKursun, 1f);
    }

    void Update()
    {
        float hareket = Input.GetAxis("Horizontal");
        transform.Translate(hareket * Time.deltaTime * oyuncuHareketHizi, 0, 0);
        float xPos = Mathf.Clamp(transform.position.x, -2.3f, 2.3f);
        transform.position = new Vector2(xPos, transform.position.y);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            playerTakesDamage(10f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="dusman_mermi")
        {
            playerTakesDamage(10f);
        }
       
    }
    private void playerTakesDamage(float amount)
    {
        currentHealthOfPlayer -= amount;
        oyuncuCanBar.fillAmount = currentHealthOfPlayer / totalHealthOfPlayer;
        if (currentHealthOfPlayer <= 0)
        {

            playerGotDestroyed();
        }
    }
    void playerGotDestroyed()
    {
        Destroy(gameObject, 1f);
        
        GameObject patlama=Instantiate(patlamaEfekti,transform.position,Quaternion.identity);
        Destroy(patlama,1f);

       manager.showPanel("Kaybettinn");
        

    }


  


}
