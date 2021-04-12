using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class Respawn : MonoBehaviour
    {
        
        public GameObject player;
        // Start is called before the first frame update
        void Start()
        {
            transform.position = player.transform.position;
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Death()
        {
            player.SetActive(false);
            player.transform.position = transform.position;
            player.transform.rotation = transform.rotation;
            player.SetActive(true);
        }
    }
}