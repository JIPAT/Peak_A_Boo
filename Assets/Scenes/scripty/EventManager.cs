using System.Collections;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public DiceRoll dice; // �������ҧ�ԧ�֧ DiceRoll script
    public int targetDiceNumber; // ���������¢ͧ Event
    private bool isPlayerRolling = false; // �����͵�Ǩ�ͺ��Ҽ����蹷���١����������
    Playermovement1 playermovement1;

    private void Awake()
    {
        playermovement1 = FindObjectOfType<Playermovement1>();
    }

    public IEnumerator TriggerEvent(Playermovement1 player)
    {
        // �������������� (1 �֧ 6)
        targetDiceNumber = Random.Range(1, 7);
        Debug.Log($"Event Triggered! {player.name} must roll a {targetDiceNumber} to succeed.");

        // ���������蹤�ԡ���ͷ���١���
        Debug.Log("Waiting for the player to roll the dice. Click to roll!");
        isPlayerRolling = false; // ��駤���������

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); // �ͨ����Ҽ����蹨Ф�ԡ�����
        

    }
}
