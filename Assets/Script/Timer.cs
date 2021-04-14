using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timer = 0.0f;
    public float maxtimer = 10.0f;
    public float actualtimer = 0.0f;
    public bool Timsup = false;


    // Start is called before the first frame update
    void Start()
    {
        timer = maxtimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(Timsup == false)
        {
            timer -= 1 * Time.deltaTime;
        }
        
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Livraison1"))
        {
            actualtimer = timer;
            timer = maxtimer + actualtimer;
        }
        if (other.CompareTag("Livraison2"))
        {
            actualtimer = timer;
            timer = maxtimer + actualtimer;
        }
        if (other.CompareTag("Livraison3"))
        {
            Timsup = true;
            if (timer <= 0)
            {
                Debug.Log("Lose");
            }
            else if(timer > 0)
            {
                Debug.Log("Win");
            }
        }
    }
}
