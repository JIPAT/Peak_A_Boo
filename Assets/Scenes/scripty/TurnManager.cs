using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public Playermovement1 player1; // �������ҧ�ԧ�֧ Player 1
    public Playermovement1 player2; // �������ҧ�ԧ�֧ Player 2
    public DiceRoll dice; // �������ҧ�ԧ�֧ DiceRoll script
    public EventManager eventManager;
    public Playermovement1 playermovement1; // �������ҧ�ԧ�֧ Player 2
    public Playermovement1 currentPlayer; // �����蹤��Ѩ�غѹ
    public bool isDiceRolling = false; // ����ҡ��ѧ����١����������

    private void Start()
    {
        playermovement1 = FindObjectOfType<Playermovement1>();
        eventManager = FindObjectOfType<EventManager>();
        // ��Ǩ�ͺ������˹�������Ѻ player1, player2, dice ����
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
            yield break; // ��ش��÷ӧҹ�ҡ������������ö��ҹ��
        }

        isDiceRolling = true;

        // ����١���
        yield return new WaitUntil(() => dice.GetComponent<Rigidbody>().velocity == Vector3.zero);

        // ������������蹤��Ѩ�غѹ�Թ
        Debug.Log($"{currentPlayer.name} rolled: {dice.diceFaceNum}");
        currentPlayer.steps = dice.diceFaceNum;
        yield return currentPlayer.StartCoroutine(currentPlayer.MovePlayer());

        // ��Ǩ�ͺ��Ҽ����蹵�ŧ����ͧ Event �������
        if (IsEventTile(currentPlayer.currentNodeIndex))
        {
            Debug.Log($"Player landed on an Event Tile at Node {currentPlayer.currentNodeIndex}");
            // ���¡�� TriggerEvent �ҡ EventManager
            yield return eventManager.StartCoroutine(eventManager.TriggerEvent(currentPlayer));
            currentPlayer.y = 2;
        }

        else if (!IsEventTile(currentPlayer.currentNodeIndex))
        {
            // ��Ѻ������
            currentPlayer = currentPlayer == player1 ? player2 : player1;
            Debug.Log($"Now it's {currentPlayer.name}'s turn.");

            isDiceRolling = false;
        }
    }

    private List<int> eventTiles = new List<int> { 7, 9, 17, 20, 21, 26, 29, 30, 35, 38 }; // ������ͧ Event ����ͧ���

    private bool IsEventTile(int nodeIndex)
    {
        // ��Ǩ�ͺ��� Node �Ѩ�غѹ����� List �ͧ Event Tiles �������
        return eventTiles.Contains(nodeIndex);
    }

}
