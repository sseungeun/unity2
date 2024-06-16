using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float movePower = 1f;
    public int score;
    public int enemyType;

    Animator animator;
    Vector3 movement;
    int movementFlag = 0;
    bool isDie = false;

    void FixedUpdate()
    {
        if (!isDie)
        {
            Move();
        }
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (movementFlag == 1)
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movementFlag == 2)
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();

        StartCoroutine(ChangeMovement());
    }

    IEnumerator ChangeMovement()
    {
        movementFlag = Random.Range(0, 3);

        if (movementFlag == 0)
            animator.SetBool("isMoving", false);
        else
            animator.SetBool("isMoving", true);

        yield return new WaitForSeconds(3f);

        StartCoroutine(ChangeMovement());
    }

    public void Die()
    {
        StopCoroutine("ChangeMovement");
        isDie = true;

        BoxCollider2D coll = gameObject.GetComponent<BoxCollider2D>();
        coll.enabled = false;

        Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();
        Vector2 dieVelocity = new Vector2(0, 3f);
        rigid.AddForce(dieVelocity, ForceMode2D.Impulse);

       // Destroy(gameObject, 5f);
    }
}
