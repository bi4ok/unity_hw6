using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrackerGame : MonoBehaviour
{
    // Time
    public Text timerText;
    private float endGameDeltaTime;
    private float endGameTime = 20;
    // Pins
    public Text firstPinText;
    private int firstPinNumber;
    private int firstPinStartNumber =5;
    private int firstPinEndNumber = 4;
    public Text secondPinText;
    private int secondPinNumber;
    private int secondPinStartNumber =8;
    private int secondPinEndNumber = 9;
    public Text thirdPinText;
    private int thirdPinNumber;
    private int thirdPinStartNumber =6;
    private int thirdPinEndNumber = 7;
    // Panels
    public GameObject gamePanel;
    public GameObject endGamePanel;
    // Buttons
    public Button drillButton;
    public Button hammerButton;
    public Button lockPickButton;
    // Game
    private bool gameState;
    public Text endGameText;



    void Start()
    {
        ResetGame();
    }

    private void TimeReset()
    {
        endGameDeltaTime = endGameTime;
    }

    private void TimeUpdate()
    {
        endGameDeltaTime = endGameDeltaTime - (Time.deltaTime);
        timerText.text = Mathf.Round(endGameDeltaTime).ToString();
    }

    public void ResetGame()
    {
        // Обновляем панели, кнопку, состояние игры
        gamePanel.SetActive(true);
        endGamePanel.SetActive(false);

        drillButton.interactable = true;
        hammerButton.interactable = true;
        lockPickButton.interactable = true;

        gameState = true;

        // Обновляем пины
        PinUpdate(firstPinStartNumber, secondPinStartNumber, thirdPinStartNumber);

        // Перезапускаем время
        TimeReset();

    }

    private void PinUpdate(int first, int second, int third)
    {
        firstPinNumber = PinUpdateChanger(first);
        secondPinNumber = PinUpdateChanger(second);
        thirdPinNumber = PinUpdateChanger(third);

        firstPinText.text = $"{firstPinNumber}";
        secondPinText.text = $"{secondPinNumber}";
        thirdPinText.text = $"{thirdPinNumber}";
    }

    private int PinUpdateChanger(int newPin)
    {
        if (newPin > 10)
        {
            return 10;
        }
        else if (newPin < 0)
        {
            return 0;
        }
        else
        {
            return newPin;
        }
    }

    public void DrillButtonClick()
    {
        PinUpdate(firstPinNumber + 1, secondPinNumber - 1, thirdPinNumber);
    }

    public void HammerButtonClick()
    {
        PinUpdate(firstPinNumber - 1, secondPinNumber + 2, thirdPinNumber - 1);
    }

    public void LockpiickButtonClick()
    {
        PinUpdate(firstPinNumber - 1, secondPinNumber + 1, thirdPinNumber + 1);
    }


    private void EndGame(string endText)
    {
        endGamePanel.SetActive(true);
        endGameText.text = endText;

        drillButton.interactable = false;
        hammerButton.interactable = false;
        lockPickButton.interactable = false;

        gameState = false;
    }


    private bool PlayerWinCheck()
    {
        return firstPinNumber == firstPinEndNumber && secondPinNumber == secondPinEndNumber && thirdPinNumber == thirdPinEndNumber;
    }

    void Update()
    {

        if (gameState is true)
        {
            TimeUpdate();

            if (PlayerWinCheck())
            {
                EndGame("Вы выиграли!");
            }
            else if ((endGameDeltaTime) <= 0)
            {
                EndGame("Вы проиграли!");
            }
        }
        
    }
}
