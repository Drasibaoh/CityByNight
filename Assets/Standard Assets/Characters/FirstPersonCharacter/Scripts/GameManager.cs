using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class GameManager : MonoBehaviour
    {
        public UnityEvent restart;

        public static GameManager instance;
        public List<GameObject> obj;
        public int delivered = 0;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
            restart = new UnityEvent();
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void ChangeObjectif()
        {
            delivered++;
            if (delivered < obj.Count)
            {
                for (int i = 0; i < obj.Count ; i++)
                {
                    if (i == delivered)
                        obj[i].SetActive(true);
                    else
                        obj[i].SetActive(false);
                }
            }
            else
            {
                Debug.Log("ggwp");
                obj[delivered - 1].SetActive(false);
            }

        }
    }
}
