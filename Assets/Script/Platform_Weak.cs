using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Weak : MonoBehaviour
{
    public float stay = 2.0f;
    public GameObject Plateforme;
    public bool Go = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Go == true)
        {
            stay -= 1.0f * Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Go = true;
        if(stay <= 0)
        {
            if (other.CompareTag("Player"))
            {
                Invoke("Disappear", 1.5f);
            }
        }
        
    }

    public void Disappear()
    {
       Plateforme.SetActive(false);
    }
}

            
        