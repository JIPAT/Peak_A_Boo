using System.Collections;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public DiceRoll dice; // ตัวแปรอ้างอิงถึง DiceRoll script
    public int targetDiceNumber; // แต้มเป้าหมายของ Event
    private bool isPlayerRolling = false; // ใช้เพื่อตรวจสอบว่าผู้เล่นทอยลูกเต๋าหรือไม่
    Playermovement1 playermovement1;

    private void Awake()
    {
        playermovement1 = FindObjectOfType<Playermovement1>();
    }

    public IEnumerator TriggerEvent(Playermovement1 player)
    {
        // สุ่มแต้มเป้าหมาย (1 ถึง 6)
        targetDiceNumber = Random.Range(1, 7);
        Debug.Log($"Event Triggered! {player.name} must roll a {targetDiceNumber} to succeed.");

        // รอให้ผู้เล่นคลิกเพื่อทอยลูกเต๋า
        Debug.Log("Waiting for the player to roll the dice. Click to roll!");
        isPlayerRolling = false; // ตั้งค่าเริ่มต้น

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); // รอจนกว่าผู้เล่นจะคลิกเมาส์
        

    }
}
