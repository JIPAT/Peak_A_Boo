using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private float moveSpeed = 2f; // ความเร็วในการเคลื่อนที่

    private void Start()
    {
        // ตั้งค่าตำแหน่งเริ่มต้นของ Player
        if (pathNodes.Length > 0)
        {
            transform.position = pathNodes[0].position;
        }
    }

    private void Update()
    {
        
    }


    public System.Collections.IEnumerator MovePlayer()
    {
        steps = dice.diceFaceNum;
        Debug.LogError(steps);
        for (int i = 0; i < steps; i++)
            {
            // เพิ่ม Index เพื่อย้ายไปยัง Node ถัดไป
            if (currentNodeIndex >= pathNodes.Length - 1)
            {
                currentNodeIndex = pathNodes.Length - 1; // ล็อกให้ตำแหน่งสุดท้ายอยู่ที่บล็อกสุดท้าย
                Debug.Log("Player has reached the finish line!");
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
        Debug.Log("หี");
    }

}
