using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class Delivery : MonoBehaviour
    {
        bool hasDelivered;
        bool isInteractable;
        public Text feedback;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isInteractable)
            {

                if (Input.GetKeyDown(KeyCode.E) && !hasDelivered)
                {
                    GameManager.instance.ChangeObjectif();
                    hasDelivered = true;
                    feedback.gameObject.SetActive(false);
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                feedback.gameObject.SetActive(true);
                isInteractable = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                feedback.gameObject.SetActive(false);
                isInteractable = false;
            }
        }
    }
}