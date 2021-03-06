using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class GameManager : MonoBehaviour
    {
        public UnityEvent restart;
        public UnityEvent Over;
        public bool end=false;
        private float timer=0;
        public static GameManager instance;
        public List<GameObject> obj;
        public int delivered = 0;
        public TimerDos timerd;
        public List<float> timeToAdd;

        public float finalTime;
        public float finalMin;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
            restart = new UnityEvent();

            delivered--;
        }
        // Start is called before the first frame update
        void Start()
        {

          //  timerd.AddTime(timeToAdd[0]);
        }

        // Update is called once per frame
        void Update()
        {
            if (end)
            {                    
                timer += Time.deltaTime;
                if (timer >= 2)
                {
                    timerd.FinalSave();
                    Debug.Log("fondu");
                    Over.Invoke();

                    GameReset();
                    if (timerd.timer >= 0f)
                        {
                            
                            SceneManager.LoadScene(2);
                        }
                        else
                        {
                            SceneManager.LoadScene(3);
                        }
                    end = false;
                }
            }
        }
        public void ChangeObjectif()
        {
            
            delivered++;
            Debug.Log(delivered);
            if (delivered < obj.Count)
            {
                for (int i = 0; i < obj.Count ; i++)
                {
                    if (i == delivered)
                        obj[i].SetActive(true);
                    else
                        obj[i].SetActive(false);
                }
                timerd.AddTime(timeToAdd[delivered]);
            }
            else
            {
                Debug.Log("ggwp");
                end = true;

                obj[delivered - 1].SetActive(false);

            }

        }
        public void GameReset()
        {
            //timerd.AddTime(35);
            delivered = -1;
            obj.Clear();
            for (int i=0; i<3;i++)
                obj.Insert(i,null);
        }
    }
}
