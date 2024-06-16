using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float movePower = 5f; // Move speed
    public float jumpPower = 10f; // Jump power

    private Rigidbody2D rigid;
    private bool isJumping = false;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Jump input
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rigid.velocity.y) < 0.001f)
        {
            isJumping = true;
        }
    }

    void FixedUpdate()
    {
        Move();
        if (isJumping)
        {
            Jump();
        }
    }

    void Move()
    {
        float move = Input.GetAxis("Horizontal");

        // Move the player
        Vector2 moveVelocity = new Vector2(move * movePower, rigid.velocity.y);
        rigid.velocity = moveVelocity;

        // Flip the player's sprite based on movement direction
        if (move < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (move > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void Jump()
    {
        // Prevent Velocity amplification
        rigid.velocity = new Vector2(rigid.velocity.x, 0);

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }

	void OnTriggerEnter2D(Collider2D collision) {
    //Tag가 item일 때
	// if (collision.gameObject.tag == "Item") {
	// 	//Deactive Item
	// 	collision.gameObject.SetActive(false);
	// }
    // if(collision.gameObject.tag.CompareTo("Block")==0){
    //     GameManager.instance.GameOver();
    // }

    // Debug.Log("Finish");
    if(collision.gameObject.CompareTag("Item")){
        HandleItemPickup(collision.gameObject);
    }
    else if(collision.gameObject.CompareTag("Finish")){
        HandleGameFinish();
    }


}

void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Enemy" && rigid.velocity.y < -10f){

        Debug.Log("dddd");
        EnemyMove enemy=collision.gameObject.GetComponent<EnemyMove>();
        enemy.Die();

        Vector2 killVelocity=new Vector2(0,3f);
        rigid.AddForce(killVelocity,ForceMode2D.Impulse);

        ScoreManagers.setScore(enemy.score);
    }
    }
void HandleItemPickup(GameObject item){
    Debug.Log("Item picked up: "+item.name);
    Destroy(item);
}

void HandleGameFinish(){
    Debug.Log("Game Clear!");
    SceneManager.LoadScene("GameOver");
}
public void Dead(){
    Destroy(gameObject);
}

}


