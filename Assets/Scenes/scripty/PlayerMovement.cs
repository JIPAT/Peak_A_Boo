using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public DiceRoll dice; // อ้างอิงถึง DiceRoll script
    public Transform[] pathNodes; // Node ที่เป็นจุดในเส้นทางของด่าน
    private int currentNodeIndex = 0; // ตำแหน่งปัจจุบันของ Player
    private bool isMoving = false;
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
 
        // เริ่มเคลื่อนที่เมื่อผลลูกเต๋าออก
        if (Input.GetMouseButtonDown(0))
        {
            diceRollScript.RollDice();
            // รอให้ DiceRoll คำนวณหน้าเต๋า
            if (dice.GetComponent<Rigidbody>().velocity == Vector3.zero)
            {
                isMoving = true;
                StartCoroutine(MovePlayer(dice.diceFaceNum));
            }
        }
    }

    private System.Collections.IEnumerator MovePlayer(int steps)
    {
        if (isMoving = true)
        {
            for (int i = 0; i < steps; i++)
            {
                // เพิ่ม Index เพื่อย้ายไปยัง Node ถัดไป
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

            
    }
}
