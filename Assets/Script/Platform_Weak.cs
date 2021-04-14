using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Weak : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Collision");
        if (other.CompareTag("Player"))
        {
            Invoke("Disappear", 1.5f);
        }
    }

    public void Disappear()
    {
       this.gameObject.SetActive(false);
    }
}

            
        