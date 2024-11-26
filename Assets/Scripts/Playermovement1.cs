using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Playermovement1 : MonoBehaviour
{
    public DiceRoll dice; // อ้างอิงถึง DiceRoll script
    public Transform[] pathNodes; // Node ที่เป็นจุดในเส้นทางของด่าน
    public int currentNodeIndex = 0; // ตำแหน่งปัจจุบันของ Player
    public bool isMoving = false;
    public int steps;
    public int x;
    public int y;
    public DiceRoll diceRollScript; // ตัวแปรอ้างอิงถึงสคริปต์ DiceRoll
    public PlayerTurn playerTurn;
    public TurnManager currentPlayer; // ผู้เล่นคนปัจจุบัน
    

    [SerializeField] private float moveSpeed = 2f; // ความเร็วในการเคลื่อนที่

    [SerializeField] TextMeshProUGUI player1; // ตำแหน่ง Player 1

    private void Start()
    {
        // ตั้งค่าตำแหน่งเริ่มต้นของ Player
        if (pathNodes.Length > 0)
        {
            transform.position = pathNodes[0].position;
        }

        playerTurn = FindObjectOfType<PlayerTurn>();
    }

    private void Update()
    {
        player1.text = currentNodeIndex.ToString();

    }


    public System.Collections.IEnumerator MovePlayer()
    {
        Debug.Log("Start" + currentNodeIndex);
        steps = dice.diceFaceNum;
        Debug.LogError(steps);
        for (int i = 0; i < steps; i++)
            {
            // เพิ่ม Index เพื่อย้ายไปยัง Node ถัดไป
            if (currentNodeIndex >= 39)  //pathNodes.Length - 1
            {
                currentNodeIndex = 39; // ล็อกให้ตำแหน่งสุดท้ายอยู่ที่บล็อกสุดท้าย
                Debug.Log("Player has reached the finish line!");

                playerTurn.ShowWinMessage();
                break; // หยุดการเดิน
            }

            currentNodeIndex = (currentNodeIndex + 1) % pathNodes.Length;

                Vector3 targetPosition = pathNodes[currentNodeIndex].position;

                // เคลื่อนที่ไปยังตำแหน่งของ Node ปัจจุบัน
                while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                    yield return null;
                }
            }
            isMoving = false; // หยุดเคลื่อนที่เมื่อถึงเป้าหมาย
            // Debug.Log("end" + currentNodeIndex);
    }

    public IEnumerator MoveSteps(int stepsToMove)
    {
        int direction = stepsToMove > 0 ? 1 : -1; // กำหนดทิศทาง (เดินหน้า = 1, ถอยหลัง = -1)
        stepsToMove = Mathf.Abs(stepsToMove); // ทำให้จำนวนก้าวเป็นบวก

        for (int i = 0; i < stepsToMove; i++)
        {
            // เพิ่ม/ลด Index ตามทิศทาง
            if (direction > 0 && currentNodeIndex < pathNodes.Length - 1)
            {
                currentNodeIndex++;
            }
            else if (direction < 0 && currentNodeIndex > 0)
            {
                currentNodeIndex--;
            }
            else if (currentNodeIndex > pathNodes.Length - 1)
            {
                Debug.Log("Cannot move further in this direction.");
                break; // หยุดหากไม่สามารถเดินต่อได้
            }

            Vector3 targetPosition = pathNodes[currentNodeIndex].position;

            // เคลื่อนที่ไปยังโหนดใหม่
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            Debug.Log($"Player moved to Node {currentNodeIndex}");
        }

        yield return null; // รอให้ Coroutine จบ
        isMoving = false; // หยุดเคลื่อนที่เมื่อถึงเป้าหมาย
        stepsToMove = 0;
    }

}