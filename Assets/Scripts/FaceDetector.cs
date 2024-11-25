using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FaceDetector : MonoBehaviour
{
    DiceRoll dice;
    Playermovement1 playermovement1;
    Playermovement1 playermovement2;
    TurnManager PlayTurn;
    EventManager eventManager;
    PlayerTurn playerTurn;

    private void Awake()
    {
        dice = FindObjectOfType<DiceRoll>();
        playerTurn = FindObjectOfType<PlayerTurn>(); // ค้นหา PlayerTurn ใน Scene
        playermovement1 = FindObjectOfType<Playermovement1>(); // สำหรับ Player 1
        playermovement2 = FindObjectOfType<Playermovement1>(); // สำหรับ Player 2
        PlayTurn = FindObjectOfType<TurnManager>();
        eventManager = FindObjectOfType<EventManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (dice != null)
        {
            if (dice.GetComponent<Rigidbody>().velocity == Vector3.zero)
            {
                dice.diceFaceNum = int.Parse(other.name); // อ่านค่าผลลัพธ์จากลูกเต๋า
                                                          // ตรวจสอบว่าเป็นเทิร์นของผู้เล่นที่ถูกต้อง
                if (!playerTurn.isTurnActive)
                {
                    Debug.Log("Start TurnValue()");
                    playerTurn.TurnValue();
                    playerTurn.isTurnActive = true; // ตั้งค่าเป็น active หลังจากเรียกครั้งแรกแล้ว
                }

                if (PlayTurn.currentPlayer != null && !PlayTurn.isDiceRolling)
                {
                    if (PlayTurn.currentPlayer.y == 2) // ตรวจสอบว่า Player ปัจจุบันพร้อมเดินหรือไม่
                    {
                        // ตรวจสอบผลการทอยลูกเต๋า
                        Debug.Log($"Player rolled: {dice.diceFaceNum}");

                        if (dice.diceFaceNum == eventManager.targetDiceNumber)
                        {
                            Debug.Log($"{PlayTurn.currentPlayer.name} succeeded! Moving forward 2 steps.");
                            StartCoroutine(PlayTurn.currentPlayer.MoveSteps(2)); // เดินหน้า 2 ช่อง
                        }
                        else
                        {
                            Debug.Log($"{PlayTurn.currentPlayer.name} failed! Moving backward 2 steps.");
                            StartCoroutine(PlayTurn.currentPlayer.MoveSteps(-2)); // ถอยหลัง 2 ช่อง
                        }
                        
                        // สลับผู้เล่น
                        PlayTurn.currentPlayer = PlayTurn.currentPlayer == PlayTurn.player1 ? PlayTurn.player2 : PlayTurn.player1;
                        Debug.Log($"ไรอะNow it's {PlayTurn.currentPlayer.name}'s turn.");
                        PlayTurn.currentPlayer.x = 1;

                    }
                    if (PlayTurn.currentPlayer.x == 0 && PlayTurn.currentPlayer.y != 2)
                    {
                        StartCoroutine(PlayTurn.PlayTurn()); // เริ่มต้นเทิร์นใหม่
                        PlayTurn.currentPlayer.x = 1;
                    }
                }
            }
        }
    }
}