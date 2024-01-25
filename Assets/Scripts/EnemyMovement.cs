using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    Rigidbody2D rigidbody;
    [SerializeField] float moveSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = new UnityEngine.Vector2 (moveSpeed, 0f);
    }

    void FlipEnemyFacing(){
        transform.localScale = new UnityEngine.Vector2 (-(Mathf.Sign(rigidbody.velocity.x)), 1f);
    }

    private void OnTriggerExit2D(Collider2D other) {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }
}
