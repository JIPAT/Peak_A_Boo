// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

// public class PlayerTurn : MonoBehaviour
// {
//     [SerializeField] TextMeshProUGUI turnText;      // แสดงข้อความบอกเทิร์นของผู้เล่น
//     [SerializeField] TextMeshProUGUI diceScoreText; // แสดงคะแนนลูกเต๋าในเทิร์น
//     [SerializeField] TextMeshProUGUI player1PositionText; // ตำแหน่ง Player 1
//     [SerializeField] TextMeshProUGUI player2PositionText; // ตำแหน่ง Player 2

//     private int[] playerPositions = { 1, 1 }; // ตำแหน่งปัจจุบันของผู้เล่น (เริ่มที่ช่อง 1)
//     private int currentPlayer = 0;            // ผู้เล่นคนปัจจุบัน (0 หรือ 1)
//     private bool isRolling = false;           // ตรวจสอบว่ากำลังทอยลูกเต๋าอยู่หรือไม่
//     private int lastDiceScore = 0;            // คะแนนลูกเต๋าล่าสุด

//     [SerializeField] int boardSize = 100;     // จำนวนช่องบนกระดาน

//     private DiceRoll dice; // อ้างอิงถึงสคริปต์ DiceRoll

//     private void Start()
//     {
//         dice = FindObjectOfType<DiceRoll>(); // ค้นหา DiceRoll ใน Scene
//         if (dice == null)
//         {
//             Debug.LogError("DiceRoll component not found in the scene.");
//         }

//         UpdateUI();
//     }


//     public void TurnValue()
//     {
//         if (isRolling || dice == null) return; // ห้ามทอยซ้ำถ้ากำลังทอยหรือไม่มี DiceRoll

//         isRolling = true;

//         int diceScore = dice.diceFaceNum; // รับค่าลูกเต๋าจาก DiceRoll
//         if (diceScore == 0)
//         {
//             Debug.Log("Dice not rolled yet.");
//             isRolling = false;
//             return;
//         }

//         lastDiceScore = diceScore; // เก็บคะแนนลูกเต๋าล่าสุด
//         // diceScoreText.text = "Dice: " + lastDiceScore;
//         diceScoreText.text = "Dice: " + dice.diceFaceNum;

//         // คำนวณตำแหน่งใหม่
//         int newPosition = playerPositions[currentPlayer] + lastDiceScore;

//         // ตรวจสอบถ้าตำแหน่งเกินขนาดกระดาน
//         if (newPosition > boardSize)
//         {
//             newPosition = playerPositions[currentPlayer]; // อยู่ที่เดิมถ้าเดินเกินกระดาน
//         }

//         playerPositions[currentPlayer] = newPosition; // อัปเดตตำแหน่งของผู้เล่น

//         // อัปเดต UI
//         UpdateUI();

//         // สลับเทิร์น
//         currentPlayer = (currentPlayer + 1) % 2;
//         isRolling = false;

//         // รีเซ็ตค่าลูกเต๋า (ป้องกันใช้ค่าซ้ำในเทิร์นถัดไป)
//         dice.diceFaceNum = 0;
//     }

//     private void UpdateUI()
//     {
//         // แสดงข้อมูลผู้เล่นที่กำลังเล่น
//         turnText.text = "Player " + (currentPlayer + 1) + "'s Turn";
        

//         // แสดงคะแนนลูกเต๋าล่าสุด
//         diceScoreText.text = "Dice: " + lastDiceScore;
//         Debug.Log("lastDiceScore: " + lastDiceScore);

//         // แสดงตำแหน่งของผู้เล่นแต่ละคน
//         player1PositionText.text = "Player 1 : " + playerPositions[0];
//         player2PositionText.text = "Player 2 : " + playerPositions[1];
//     }

// }


using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // เพิ่มเพื่อใช้ฟังก์ชันจัดการฉาก

public class PlayerTurn : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI turnText;      // แสดงข้อความบอกเทิร์นของผู้เล่น
    [SerializeField] TextMeshProUGUI diceScoreText; // แสดงคะแนนลูกเต๋าในเทิร์น
    [SerializeField] TextMeshProUGUI player1PositionText; // ตำแหน่ง Player 1
    // [SerializeField] TextMeshProUGUI player2PositionText; // ตำแหน่ง Player 2
    [SerializeField] GameObject winPanel;          // UI แสดงข้อความ "Congrats, you win!"
    [SerializeField] TextMeshProUGUI winText;      // แสดงชื่อผู้ชนะใน UI




    private int[] playerPositions = { 1, 1 }; // ตำแหน่งปัจจุบันของผู้เล่น (เริ่มที่ช่อง 1)
    // private int currentPlayer = 0;            // ผู้เล่นคนปัจจุบัน (0 หรือ 1)
    private bool isRolling = false;           // ตรวจสอบว่ากำลังทอยลูกเต๋าอยู่หรือไม่
    private int lastDiceScore = 0;            // คะแนนลูกเต๋าล่าสุด

    [SerializeField] int boardSize = 100;     // จำนวนช่องบนกระดาน

    private DiceRoll dice; // อ้างอิงถึงสคริปต์ DiceRoll

    public bool isTurnActive = false;  // ตัวแปรใหม่ใช้ควบคุมให้เรียก TurnValue() ทีละครั้ง
    public Playermovement1 player;
    public TurnManager turnManager;
    
    private void Awake()
    {
        player = FindObjectOfType<Playermovement1>();
        turnManager = FindObjectOfType<TurnManager>();
    }
    
    private void Start()
    {
        dice = FindObjectOfType<DiceRoll>(); // ค้นหา DiceRoll ใน Scene
        if (dice == null)
        {
            Debug.LogError("DiceRoll component not found in the scene.");
        }

        winPanel.SetActive(false); // ซ่อน UI "Congrats, you win!" ตอนเริ่มเกม
        UpdateUI();
    }

    public void TurnValue()
    {

        // ตั้งค่าให้เริ่มต้นการทอย
        isRolling = true;

        int diceScore = dice.diceFaceNum; // รับค่าลูกเต๋าจาก DiceRoll
        if (diceScore == 0)
        {
            Debug.Log("Dice not rolled yet.");
            isRolling = false;
            return;
        }

        lastDiceScore = diceScore; // เก็บคะแนนลูกเต๋าล่าสุด
        diceScoreText.text = "Dice: " + dice.diceFaceNum;

        // คำนวณตำแหน่งใหม่ของผู้เล่น
        // int newPosition = playerPositions[currentPlayer] + lastDiceScore;

        // ตรวจสอบถ้าตำแหน่งเกินขนาดกระดาน
        // if (newPosition >= boardSize)
        // {
        //     newPosition = boardSize; // ถ้าเกินขนาดกระดาน ให้หยุดที่ขอบสุด
        //     playerPositions[currentPlayer] = newPosition; 
        //     ShowWinMessage(currentPlayer); // เรียกฟังก์ชันแสดงข้อความผู้ชนะ
        //     return; // จบเทิร์นทันทีเมื่อมีผู้ชนะ
        // }
        

        // playerPositions[currentPlayer] = newPosition; // อัปเดตตำแหน่งของผู้เล่น


        // สลับเทิร์น
        // currentPlayer = (currentPlayer + 1) % 2;
        isRolling = false;  // รีเซ็ตการทอยหลังจากเทิร์นเสร็จ

        // อัปเดต UI
        UpdateUI();

        // รีเซ็ตค่าลูกเต๋า
        dice.diceFaceNum = 0;

        // รีเซ็ต isTurnActive หลังจากจบเทิร์น
        isTurnActive = false; // รีเซ็ตเพื่อให้สามารถเรียก TurnValue ในเทิร์นถัดไป
        // Debug.Log("Player's turn  " + isTurnActive);
    }

    private void UpdateUI()
    {
        // แสดงข้อมูลผู้เล่นที่กำลังเล่น
        // turnText.text = "Player " + (currentPlayer + 1) + "'s Turn";


        // แสดงคะแนนลูกเต๋าล่าสุด
        diceScoreText.text = "Dice: " + lastDiceScore;
        // Debug.Log("lastDiceScore: " + lastDiceScore);

        // แสดงตำแหน่งของผู้เล่นแต่ละคน
        // player1PositionText.text = "Player 1 : " + playerPositions[0];
        // player2PositionText.text = "Player 2 : " + playerPositions[1];

        player1PositionText.text = player.currentNodeIndex.ToString();
        turnText.text = turnManager.currentPlayer.ToString();
    }

    private void ShowWinMessage(int winner)
    {
        winPanel.SetActive(true); // เปิด UI "Congrats, you win!"
        winText.text = "Congrats, Player " + (winner + 1) + " wins!";

        Invoke("RestartGame", 5f); // รอ 3 วินาที แล้วรีสตาร์ตเกม
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // โหลดฉากใหม่
    }

    private void Update()
    {
        player1PositionText.text = player.currentNodeIndex.ToString();
        turnText.text = turnManager.currentPlayer.ToString();
    }

}
