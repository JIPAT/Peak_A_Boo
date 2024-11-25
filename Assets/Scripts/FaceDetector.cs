using UnityEngine;

public class FaceDetector : MonoBehaviour
{
    DiceRoll dice;
    PlayerTurn playerTurn;

    private void Awake()
    {
        dice = FindObjectOfType<DiceRoll>();
        playerTurn = FindObjectOfType<PlayerTurn>(); // ค้นหา PlayerTurn ใน Scene
    }

    private void OnTriggerStay(Collider other)
    {
        // if (dice != null && playerTurn != null)
        if (dice != null)
        {
            // ตรวจสอบว่า velocity ของ Rigidbody เป็น 0 (ลูกเต๋าหยุดหมุน)
            if (dice.GetComponent<Rigidbody>().velocity == Vector3.zero)
            {
                // เมื่อหยุดหมุนแล้ว กำหนดค่าหน้าลูกเต๋า
                dice.diceFaceNum = int.Parse(other.name);
                // Debug.Log("Save diceFaceNum");

                // เรียกฟังก์ชัน TurnValue() จาก PlayerTurn เมื่อ isTurnActive เป็น false
                if (!playerTurn.isTurnActive)
                {
                    Debug.Log("Start TurnValue()");
                    playerTurn.TurnValue();
                    playerTurn.isTurnActive = true; // ตั้งค่าเป็น active หลังจากเรียกครั้งแรกแล้ว
                    
                }
            }
        }
    }
}


// using System.Collections; 
// using System.Collections.Generic; 
// using TMPro;
// using UnityEngine;
// public class FaceDetector : MonoBehaviour
// {
//     DiceRoll dice;
//     private void Awake()
//     {
//         dice = FindObjectOfType<DiceRoll>();
//     }
//     private void OnTriggerStay(Collider other)
//     {
//         if (dice != null)
//         {
//             if (dice.GetComponent<Rigidbody>().velocity == Vector3.zero)
//             {
//                 dice.diceFaceNum = int.Parse(other.name);
                
//             }
//         }
//     }
// }