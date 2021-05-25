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
        public int id=0;
        public Text feedback;
        public Text boostedFeedback;

        // Start is called before the first frame update
        void Awake()
        {
            if (id >= 0)
            {
                GameManager.instance.obj[id - 1] = gameObject;
            }
            else
            {
                Debug.LogError("ID not assigned");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isInteractable)
            {

                if (Input.GetKeyDown(KeyCode.E) && !hasDelivered)
                {
                    hasDelivered = true;
                    GameManager.instance.ChangeObjectif();
                    isInteractable = false;
                    feedback.gameObject.SetActive(false);
                    boostedFeedback.gameObject.SetActive(false);
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                feedback.gameObject.SetActive(true);
                boostedFeedback.gameObject.SetActive(true);
                isInteractable = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                feedback.gameObject.SetActive(false);
                boostedFeedback.gameObject.SetActive(false);

                isInteractable = false;
            }
        }
    }
}