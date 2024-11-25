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

    private void Awake()
    {
        dice = FindObjectOfType<DiceRoll>();
        playermovement1 = FindObjectOfType<Playermovement1>(); // ����Ѻ Player 1
        playermovement2 = FindObjectOfType<Playermovement1>(); // ����Ѻ Player 2
        PlayTurn = FindObjectOfType<TurnManager>();
        eventManager = FindObjectOfType<EventManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (dice != null)
        {
            if (dice.GetComponent<Rigidbody>().velocity == Vector3.zero)
            {
                dice.diceFaceNum = int.Parse(other.name); // ��ҹ��Ҽ��Ѿ��ҡ�١���

                if (PlayTurn.currentPlayer.y == 2) // ��Ǩ�ͺ��� Player �Ѩ�غѹ������Թ�������
                {
                    // ��Ǩ�ͺ�š�÷���١���
                    Debug.Log($"Player rolled: {dice.diceFaceNum}");

                    if (dice.diceFaceNum == eventManager.targetDiceNumber)
                    {
                        Debug.Log($"{PlayTurn.currentPlayer.name} succeeded! Moving forward 2 steps.");
                        StartCoroutine(PlayTurn.currentPlayer.MoveSteps(2)); // �Թ˹�� 2 ��ͧ
                    }
                    else
                    {
                        Debug.Log($"{PlayTurn.currentPlayer.name} failed! Moving backward 2 steps.");
                        StartCoroutine(PlayTurn.currentPlayer.MoveSteps(-2)); // �����ѧ 2 ��ͧ
                    }

                    // ��Ѻ������
                    PlayTurn.currentPlayer = PlayTurn.currentPlayer == PlayTurn.player1 ? PlayTurn.player2 : PlayTurn.player1;
                    Debug.Log($"Now it's {PlayTurn.currentPlayer.name}'s turn.");
                    PlayTurn.currentPlayer.x = 1;
                    PlayTurn.currentPlayer.y = 0;
                    Debug.Log(PlayTurn.currentPlayer.y);
                    PlayTurn.isDiceRolling = false;
                }
                if (PlayTurn.currentPlayer.x == 0 && PlayTurn.currentPlayer.y != 2)
                {
                    Debug.Log("���������������������������");
                    StartCoroutine(PlayTurn.PlayTurn()); // ���������������
                    PlayTurn.currentPlayer.x = 1;
                }
            }
        }
    }
}
