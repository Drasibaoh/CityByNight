using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class PlatformWeak : MonoBehaviour
    {
        public float stay = 2.0f;
        public GameObject Plateforme;
        public bool Go = false;
        // Start is called before the first frame update
        void Start()
        {
            GameManager.instance.restart.AddListener(Return);
        }

        // Update is called once per frame
        void Update()
        {
            if (Go == true)
            {
                stay -= 1.0f * Time.deltaTime;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            Go = true;
            if (stay <= 0)
            {
                if (other.CompareTag("Player"))
                {
                    Invoke("Disappear", 1.5f);
                    stay = 2;
                }
            }

        }
        public void Return()
        {
            Plateforme.SetActive(true);
        }

        public void Disappear()
        {
            Plateforme.SetActive(false);
        }
    }

}
        