using System.Collections;
using UnityEngine;
using TMPro;

public class EventManager : MonoBehaviour
{
    public DiceRoll dice; // �������ҧ�ԧ�֧ DiceRoll script
    public int targetDiceNumber; // ���������¢ͧ Event
    private bool isPlayerRolling = false; // �����͵�Ǩ�ͺ��Ҽ����蹷���١����������
    Playermovement1 playermovement1;
    TurnManager PlayTurn;
    [SerializeField] TextMeshProUGUI Event;      // แสดงชื่อผู้ชนะใน UI
    [SerializeField] TextMeshProUGUI aaaa;  


    private void Awake()
    {
        playermovement1 = FindObjectOfType<Playermovement1>();
        PlayTurn = FindObjectOfType<TurnManager>();
    }

    public IEnumerator TriggerEvent(Playermovement1 player)
    {

        targetDiceNumber = Random.Range(1, 7);
        Debug.Log($"Event Triggered! {player.name} must roll a {targetDiceNumber} to succeed.");
        // Event.text = "Roll the dice to get " + targetDiceNumber;
        // aaaa.text = targetDiceNumber.ToString();
        Debug.Log("11111111111111111111111111111111111111111");


        Debug.Log("Waiting for the player to roll the dice. Click to roll!");
        isPlayerRolling = false; 

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); 
        

    }
}
