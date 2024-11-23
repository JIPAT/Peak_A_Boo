using System.Collections;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public Playermovement1 player1; // ตัวแปรอ้างอิงถึง Player 1
    public Playermovement1 player2; // ตัวแปรอ้างอิงถึง Player 2
    public DiceRoll dice; // ตัวแปรอ้างอิงถึง DiceRoll script

    private Playermovement1 currentPlayer; // ผู้เล่นคนปัจจุบัน
    private bool isDiceRolling = false; // เช็คว่ากำลังทอยลูกเต๋าหรือไม่

    private void Start()
    {
        // เริ่มเกมที่ Player 1
        currentPlayer = player1;
        player1.isMoving = false;
        player2.isMoving = false;
    }

    private void Update()
    {
        // เมื่อกดปุ่มซ้ายเมาส์เพื่อเริ่มทอยลูกเต๋า
        if (Input.GetMouseButtonDown(0) && !isDiceRolling && !currentPlayer.isMoving)
        {
            StartCoroutine(PlayTurn());
        }
    }

    private IEnumerator PlayTurn()
    {
        isDiceRolling = true;

        // ทอยลูกเต๋า
        dice.RollDice();
        yield return new WaitUntil(() => dice.GetComponent<Rigidbody>().velocity == Vector3.zero);

        // เริ่มให้ผู้เล่นคนปัจจุบันเดิน
        Debug.Log($"{currentPlayer.name} rolled: {dice.diceFaceNum}");
        currentPlayer.steps = dice.diceFaceNum;
        yield return currentPlayer.StartCoroutine(currentPlayer.MovePlayer());

        // สลับผู้เล่น
        currentPlayer = currentPlayer == player1 ? player2 : player1;
        Debug.Log($"Now it's {currentPlayer.name}'s turn.");

        isDiceRolling = false;
    }
}
