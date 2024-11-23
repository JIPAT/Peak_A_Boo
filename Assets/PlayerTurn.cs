using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTurn : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI turnText;      // แสดงข้อความบอกเทิร์นของผู้เล่น
    [SerializeField] TextMeshProUGUI diceScoreText; // แสดงคะแนนลูกเต๋าในเทิร์น
    [SerializeField] TextMeshProUGUI player1PositionText; // ตำแหน่ง Player 1
    [SerializeField] TextMeshProUGUI player2PositionText; // ตำแหน่ง Player 2

    private int[] playerPositions = { 1, 1 }; // ตำแหน่งปัจจุบันของผู้เล่น (เริ่มที่ช่อง 1)
    private int currentPlayer = 0;            // ผู้เล่นคนปัจจุบัน (0 หรือ 1)
    private bool isRolling = false;           // ตรวจสอบว่ากำลังทอยลูกเต๋าอยู่หรือไม่

    [SerializeField] int boardSize = 100;     // จำนวนช่องบนกระดาน

    private DiceRoll dice; // อ้างอิงถึงสคริปต์ DiceRoll

    private void Start()
    {
        dice = FindObjectOfType<DiceRoll>(); // ค้นหา DiceRoll ใน Scene
        if (dice == null)
        {
            Debug.LogError("DiceRoll component not found in the scene.");
        }

        UpdateUI();
    }

    public void RollDice()
    {
        if (isRolling || dice == null) return; // ห้ามทอยซ้ำถ้ากำลังทอยหรือไม่มี DiceRoll

        isRolling = true;

        int diceScore = dice.diceFaceNum; // รับค่าลูกเต๋าจาก DiceRoll
        if (diceScore == 0)
        {
            Debug.Log("Dice not rolled yet.");
            isRolling = false;
            return;
        }

        diceScoreText.text = "Dice: " + diceScore;

        // คำนวณตำแหน่งใหม่
        int newPosition = playerPositions[currentPlayer] + diceScore;

        // ตรวจสอบถ้าตำแหน่งเกินขนาดกระดาน
        if (newPosition > boardSize)
        {
            newPosition = playerPositions[currentPlayer]; // อยู่ที่เดิมถ้าเดินเกินกระดาน
        }

        playerPositions[currentPlayer] = newPosition; // อัปเดตตำแหน่งของผู้เล่น

        // อัปเดต UI
        UpdateUI();

        // สลับเทิร์น
        currentPlayer = (currentPlayer + 1) % 2;
        isRolling = false;

        // รีเซ็ตค่าลูกเต๋า (ป้องกันใช้ค่าซ้ำในเทิร์นถัดไป)
        dice.diceFaceNum = 0;
    }

    private void UpdateUI()
    {
        diceScoreText.text = dice.diceFaceNum.ToString();


        // แสดงข้อมูลผู้เล่นที่กำลังเล่น
        turnText.text = "Player " + (currentPlayer + 1) + "'s Turn";

        // แสดงตำแหน่งของผู้เล่นแต่ละคน
        player1PositionText.text = "Player 1 Position: " + playerPositions[0];
        player2PositionText.text = "Player 2 Position: " + playerPositions[1];
    }
}
