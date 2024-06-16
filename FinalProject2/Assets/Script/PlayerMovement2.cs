using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerMovement2 : MonoBehaviour
{
    public int health=100;
    public float movePower = 5f; // Move speed
    public float jumpPower = 10f; // Jump power

    private Rigidbody2D rigid;
    private bool isJumping = false;

    public void playerDamaged(int damage){
    Debug.Log("Player took"+damage+"damage!");

    health -= damage;
        if (health <= 0)
        {
            // 플레이어가 죽었을 때의 처리 로직
            Debug.Log("Player is dead!");
        }
}

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

	void OnCollisionEnter2D(Collision2D collision) {
    //Tag가 item일 때
	// if (collision.gameObject.tag == "Item") {
	// 	//Deactive Item
	// 	collision.gameObject.SetActive(false);
	// }
    // if(collision.gameObject.tag.CompareTo("Block")==0){
    //     GameManager.instance.GameOver();
    // }

    // Debug.Log("Finish");
   if(collision.gameObject.CompareTag("Spike")){
    Debug.Log("장애물에 걸림");
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
}
