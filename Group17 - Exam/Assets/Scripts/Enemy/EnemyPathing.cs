using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] private Rigidbody2D selfRb;
    [SerializeField] private EnemyAim enemyAim;
    [SerializeField] private float movementSpeed = 150f;
    // Start is called before the first frame update
    void Start()
    {
        selfRb = GetComponent<Rigidbody2D>();
        enemyAim = GetComponent<EnemyAim>();
        InvokeRepeating("Move", 0.1f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Move()
    {         
        selfRb.AddForce(enemyAim.GetRotation() * movementSpeed, ForceMode2D.Force);   
    }
}
