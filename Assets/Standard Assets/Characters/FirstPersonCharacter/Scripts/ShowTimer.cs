using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class ShowTimer : MonoBehaviour
    {
        public float min;
        public float sec;
        public Text time;
        // Start is called before the first frame update
        void Start()
        {
            sec = GameManager.instance.finalTime;
            min=GameManager.instance.finalMin;
            if (SceneManager.GetActiveScene().buildIndex==2)
                time.text = "Tu as délivré la marchandise avec " + min + " minutes et " + (sec - sec % 0.1f) + " secondes d'avances !";
            else
                time.text = "Vous avez échoué as délivré la marchandise dans les temps avec " + min + " minutes et " + (sec-sec%0.1f) + " secondes de retard !";
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}