
using System.Collections; 
using System.Collections.Generic; 
using TMPro;
using UnityEngine;

public class FaceDetector : MonoBehaviour
{
    DiceRoll dice;
    Playermovement1 playermovement1;
    private void Awake()
    {
        dice = FindObjectOfType<DiceRoll>();
        playermovement1 = FindObjectOfType<Playermovement1>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (dice != null)
        {
            if (dice.GetComponent<Rigidbody>().velocity == Vector3.zero)
            {
                dice.diceFaceNum = int.Parse(other.name);
                if (playermovement1.x == 0)
                {
                    Debug.LogError("hello");
                    StartCoroutine(playermovement1.MovePlayer());
                    playermovement1.x = 1;

                }

            }
            
        }
    }
}