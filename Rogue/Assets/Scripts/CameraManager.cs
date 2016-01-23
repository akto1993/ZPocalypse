using UnityEngine;
using System.Collections;


namespace Completed
{
    public class CameraManager : MonoBehaviour
    {
        private Transform player;
        // Use this for initialization
        void Start()
        {
            player = GameObject.Find("Player").transform;
            Debug.Log(transform.position.y);
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 playerpos = player.position;
            playerpos.z = transform.position.z;
            if (playerpos.y < 3.5f) playerpos.y = 3.5f;
            if (playerpos.x < 3.5f) playerpos.x = 3.5f;
            if (playerpos.y > GameManager.instance.rows - 4.5f) playerpos.y = GameManager.instance.rows - 4.5f;
            if (playerpos.x > GameManager.instance.columns - 4.5f) playerpos.x = GameManager.instance.columns - 4.5f;
            transform.position = playerpos;
        }
    }
}
