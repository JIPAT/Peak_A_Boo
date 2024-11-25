using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public Playermovement1 player1; // ตัวแปรอ้างอิงถึง Player 1
    public Playermovement1 player2; // ตัวแปรอ้างอิงถึง Player 2
    public DiceRoll dice; // ตัวแปรอ้างอิงถึง DiceRoll script
    public EventManager eventManager;
    public Playermovement1 playermovement1; // ตัวแปรอ้างอิงถึง Player 2
    public Playermovement1 currentPlayer; // ผู้เล่นคนปัจจุบัน
    public bool isDiceRolling = false; // เช็คว่ากำลังทอยลูกเต๋าหรือไม่

    private void Start()
    {
        playermovement1 = FindObjectOfType<Playermovement1>();
        eventManager = FindObjectOfType<EventManager>();
        // ตรวจสอบว่าได้กำหนดค่าให้กับ player1, player2, dice แล้ว
        if (player1 == null || player2 == null || dice == null)
        {
            Debug.LogError("Please assign all references in the inspector!");
            return;
        }

        currentPlayer = player1;
        player1.isMoving = false;
        player2.isMoving = false;
    }

    private void Update()
    {
        
    }

    public IEnumerator PlayTurn()
    {
        if (dice == null || currentPlayer == null)
        {
            Debug.LogError("Dice or CurrentPlayer is not assigned!");
            yield break; // หยุดการทำงานหากพบว่าไม่สามารถใช้งานได้
        }

        isDiceRolling = true;

        // ทอยลูกเต๋า
        yield return new WaitUntil(() => dice.GetComponent<Rigidbody>().velocity == Vector3.zero);

        // เริ่มให้ผู้เล่นคนปัจจุบันเดิน
        Debug.Log($"{currentPlayer.name} rolled: {dice.diceFaceNum}");
        currentPlayer.steps = dice.diceFaceNum;
        yield return currentPlayer.StartCoroutine(currentPlayer.MovePlayer());

        // ตรวจสอบว่าผู้เล่นตกลงที่ช่อง Event หรือไม่
        if (IsEventTile(currentPlayer.currentNodeIndex))
        {
            Debug.Log($"Player landed on an Event Tile at Node {currentPlayer.currentNodeIndex}");
            // เรียกใช้ TriggerEvent จาก EventManager
            yield return eventManager.StartCoroutine(eventManager.TriggerEvent(currentPlayer));
            currentPlayer.y = 2;
        }

        else if (!IsEventTile(currentPlayer.currentNodeIndex))
        {
            // สลับผู้เล่น
            currentPlayer = currentPlayer == player1 ? player2 : player1;
            Debug.Log($"สลับNow it's {currentPlayer.name}'s turn.");

        }
    }

    private List<int> eventTiles = new List<int> { 7, 20, 38 }; // เพิ่มช่อง Event ที่ต้องการ

    private bool IsEventTile(int nodeIndex)
    {
        // ตรวจสอบว่า Node ปัจจุบันอยู่ใน List ของ Event Tiles หรือไม่
        return eventTiles.Contains(nodeIndex);
    }

}