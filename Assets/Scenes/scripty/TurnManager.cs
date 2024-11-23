using System.Collections;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public Playermovement1 player1; // �������ҧ�ԧ�֧ Player 1
    public Playermovement1 player2; // �������ҧ�ԧ�֧ Player 2
    public DiceRoll dice; // �������ҧ�ԧ�֧ DiceRoll script

    private Playermovement1 currentPlayer; // �����蹤��Ѩ�غѹ
    private bool isDiceRolling = false; // ����ҡ��ѧ����١����������

    private void Start()
    {
        // ���������� Player 1
        currentPlayer = player1;
        player1.isMoving = false;
        player2.isMoving = false;
    }

    private void Update()
    {
        // ����͡���������������������������١���
        if (Input.GetMouseButtonDown(0) && !isDiceRolling && !currentPlayer.isMoving)
        {
            StartCoroutine(PlayTurn());
        }
    }

    private IEnumerator PlayTurn()
    {
        isDiceRolling = true;

        // ����١���
        dice.RollDice();
        yield return new WaitUntil(() => dice.GetComponent<Rigidbody>().velocity == Vector3.zero);

        // ������������蹤��Ѩ�غѹ�Թ
        Debug.Log($"{currentPlayer.name} rolled: {dice.diceFaceNum}");
        currentPlayer.steps = dice.diceFaceNum;
        yield return currentPlayer.StartCoroutine(currentPlayer.MovePlayer());

        // ��Ѻ������
        currentPlayer = currentPlayer == player1 ? player2 : player1;
        Debug.Log($"Now it's {currentPlayer.name}'s turn.");

        isDiceRolling = false;
    }
}
