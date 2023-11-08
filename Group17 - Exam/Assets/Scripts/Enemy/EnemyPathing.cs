using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] private Rigidbody2D selfRb;
    [SerializeField] private EnemyAim enemyAim;
    [SerializeField] private float movementSpeed = 150f;
    [SerializeField] private AudioClip walkSound;

    private AudioSource enemyAudio;
    // Start is called before the first frame update
    void Start()
    {
        selfRb = GetComponent<Rigidbody2D>();
        enemyAim = GetComponent<EnemyAim>();
        enemyAudio = GetComponent<AudioSource>();
        //InvokeRepeating("Move", 0.1f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Move();    
    }

    private void Move()
    {         
        selfRb.AddForce(enemyAim.GetRotation() * movementSpeed, ForceMode2D.Force); 
        //if (!enemyAudio.isPlaying)
        //    enemyAudio.PlayOneShot(walkSound);
    }
}
