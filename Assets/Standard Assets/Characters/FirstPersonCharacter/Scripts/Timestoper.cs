using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
namespace UnityStandardAssets.Characters.FirstPerson
{

    public class Timestoper : MonoBehaviour
    {
        public static Timestoper instance;
        [SerializeField] FirstPersonController player;
        ControllerAddon addon;
        VideoPlayer play;
        int waiter = 0;
        public bool end = false;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else Destroy(this.gameObject);
        }
        // Start is called before the first frame update
        void Start()
        {
            addon = player.gameObject.GetComponent<ControllerAddon>();
            play = GetComponent<VideoPlayer>();
            Time.timeScale = 0f;
            Invoke("GoBack", 2f);
            player.enabled=false;
            addon.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (!end)
            {
                if (waiter >= 5)
                {
                    if (!play.isPlaying)
                    {
                        GoBack();
                        end = true;
                        addon.enabled = true;
                        player.enabled=true;
                    }
                }
                else
                    waiter++;
            }


        }
        public void GoBack()
        {
            Time.timeScale = 1f;
        }

    }
}