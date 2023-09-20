using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform spawnpoint;
    [SerializeField] float spawnValue;

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < -spawnValue)
        {
            RespawnPoint();
        }
    }

    void RespawnPoint()
    {
        transform.position = spawnpoint.position;
    }
}
