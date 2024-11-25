
using System.Collections; 
using System.Collections.Generic; 
using TMPro;
using UnityEngine;

public class FaceDetector : MonoBehaviour
{
    DiceRoll dice;
    Playermovement1 playermovement1;
    TurnManager PlayTurn;
    EventManager eventManager;
    private void Awake()
    {
        dice = FindObjectOfType<DiceRoll>();
        playermovement1 = FindObjectOfType<Playermovement1>();
        PlayTurn = FindObjectOfType<TurnManager>();
        eventManager = FindObjectOfType<EventManager>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (dice != null)
        {
            if (dice.GetComponent<Rigidbody>().velocity == Vector3.zero)
            {
                dice.diceFaceNum = int.Parse(other.name);
                if (playermovement1.y == 2)
                {
                    // ตรวจสอบผลการทอยลูกเต๋า
                    Debug.Log($"Player rolled: {dice.diceFaceNum}");
                    if (dice.diceFaceNum == eventManager.targetDiceNumber)
                    {
                        Debug.Log($"{playermovement1.name} succeeded! Moving forward 2 steps.");
                        StartCoroutine(playermovement1.MoveSteps(2)); // เดินหน้า 2 ช่อง

                    }
                    else
                    {
                        Debug.Log($"{playermovement1.name} failed! Moving backward 2 steps.");
                        StartCoroutine(playermovement1.MoveSteps(-2)); // ถอยหลัง 2 ช่อง

                    }
                    PlayTurn.currentPlayer = PlayTurn.currentPlayer == PlayTurn.player1 ? PlayTurn.player2 : PlayTurn.player1;
                    Debug.Log($"Now it's {PlayTurn.currentPlayer.name}'s turn.");
                    PlayTurn.isDiceRolling = false;
                    playermovement1.x = 1;
                    playermovement1.y = 0;

                }
                else if (playermovement1.x == 0 && playermovement1.y != 2)
                {
                    StartCoroutine(PlayTurn.PlayTurn());
                    playermovement1.x = 1;
                    
                }
                


            }
            
        }
    }
}