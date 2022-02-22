using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrackerGame : MonoBehaviour
{
    // Time
    public Text timerText;
    private float _endGameDeltaTime;
    private float _endGameTime = 20;
    // Pins
    public Text firstPinText;
    private int _firstPinNumber;
    private int _firstPinStartNumber =5;
    private int _firstPinEndNumber = 4;
    public Text secondPinText;
    private int _secondPinNumber;
    private int _secondPinStartNumber =8;
    private int _secondPinEndNumber = 9;
    public Text thirdPinText;
    private int _thirdPinNumber;
    private int _thirdPinStartNumber =6;
    private int _thirdPinEndNumber = 7;
    // Panels
    public GameObject gamePanel;
    public GameObject endGamePanel;
    // Buttons
    public Button drillButton;
    public Button hammerButton;
    public Button lockPickButton;
    // Game
    private bool _gameState;
    public Text endGameText;



    void Start()
    {
        ResetGame();
    }


    public void ResetGame()
    {
        // ��������� ������, ������, ��������� ����
        gamePanel.SetActive(true);
        endGamePanel.SetActive(false);

        drillButton.interactable = true;
        hammerButton.interactable = true;
        lockPickButton.interactable = true;

        _gameState = true;

        // ��������� ����
        PinUpdate(_firstPinStartNumber, _secondPinStartNumber, _thirdPinStartNumber);

        // ������������� �����
        TimeReset();

    }

    public void DrillButtonClick()
    {
        PinUpdate(_firstPinNumber + 1, _secondPinNumber - 1, _thirdPinNumber);
    }

    public void HammerButtonClick()
    {
        PinUpdate(_firstPinNumber - 1, _secondPinNumber + 2, _thirdPinNumber - 1);
    }

    public void LockpiickButtonClick()
    {
        PinUpdate(_firstPinNumber - 1, _secondPinNumber + 1, _thirdPinNumber + 1);
    }

    private void TimeReset()
    {
        _endGameDeltaTime = _endGameTime;
    }

    private void TimeUpdate()
    {
        _endGameDeltaTime = _endGameDeltaTime - (Time.deltaTime);
        timerText.text = Mathf.Round(_endGameDeltaTime).ToString();
    }

    private void PinUpdate(int first, int second, int third)
    {
        _firstPinNumber = PinUpdateChanger(first);
        _secondPinNumber = PinUpdateChanger(second);
        _thirdPinNumber = PinUpdateChanger(third);

        firstPinText.text = $"{_firstPinNumber}";
        secondPinText.text = $"{_secondPinNumber}";
        thirdPinText.text = $"{_thirdPinNumber}";
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

    private void EndGame(string endText)
    {
        endGamePanel.SetActive(true);
        endGameText.text = endText;

        drillButton.interactable = false;
        hammerButton.interactable = false;
        lockPickButton.interactable = false;

        _gameState = false;
    }


    private bool PlayerWinCheck()
    {
        return _firstPinNumber == _firstPinEndNumber && _secondPinNumber == _secondPinEndNumber && _thirdPinNumber == _thirdPinEndNumber;
    }

    void Update()
    {

        if (_gameState is true)
        {
            TimeUpdate();

            if (PlayerWinCheck())
            {
                EndGame("�� ��������!");
            }
            else if ((_endGameDeltaTime) <= 0)
            {
                EndGame("�� ���������!");
            }
        }
        
    }
}
