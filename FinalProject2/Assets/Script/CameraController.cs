using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 5.0f; // 카메라 이동 속도

    public GameObject player; // 플레이어 오브젝트

    private void Start()
    {
        // player 변수가 할당되지 않았다면 자동으로 "Player" 태그를 가진 오브젝트를 찾음
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Debug.LogError("Player object not found! Please assign the player object in the inspector or make sure the player has the 'Player' tag.");
            }
        }
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            // 카메라의 현재 위치와 플레이어의 위치를 사용하여 새로운 위치를 계산
            Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
        }
    }
}

