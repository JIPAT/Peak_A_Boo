using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    DiceRoll dice;
    [SerializeField] TextMeshProUGUI scoreText;

    private void Awake()
    {
        dice = FindObjectOfType<DiceRoll>();

        // Log a message if dice or scoreText is null
        if (dice == null)
            Debug.LogError("DiceRoll component not found in the scene.");
        if (scoreText == null)
            Debug.LogError("ScoreText UI Text is not assigned in the Inspector.");
    }

    private void Update()
    {
        if (dice != null && scoreText != null)
        {
            if (dice.diceFaceNum != 0)
            {
                scoreText.text = dice.diceFaceNum.ToString();
            }
        }
    }
}
