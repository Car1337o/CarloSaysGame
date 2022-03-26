using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public int Difficulty;
    List<int> instructions = new List<int>();
    int choiceIndex = 0;
    enum GameState { setup, playing, gameOver };
    GameState currentState = GameState.setup;

    //button objects
    public Button RedButton;
    public Button BlueButton;
    public Button YellowButton;
    public Button GreenButton;
    List<Button> AllButtons;

    //audio variables
    AudioSource myAS;
    public AudioClip victory;

    //text variables
    public GameObject youWintext;
    public GameObject youLosetext;
    public GameObject instructionText;
    public GameObject yourTurnText;
    public TMP_Text numberText;

    void populateButtonList()
    { 
        AllButtons = new List<Button>()
        {
            RedButton, BlueButton, YellowButton, GreenButton
        };
    }

    void Start()
    {
        instructionText.SetActive(true);
        EnableButtons(false);
        BuildPuzzle();
        populateButtonList();
        StartCoroutine(SimonSaysWhat());
    }

    public void GameOver()
    {
        currentState = GameState.gameOver;
        myAS = gameObject.GetComponent<AudioSource>();
        myAS.PlayOneShot(myAS.clip);

    }

    void BuildPuzzle()
    {
        print("Difficulty: " + Difficulty);
        for (int n = 0; n < Difficulty; n++)
        {
            instructions.Add(Random.Range(0, 4));
        }

    }

    public void ButtonPress(int colour)
    {

        //Player already wons
        if (choiceIndex >= instructions.Count)
        {
            print("You already won!");
            myAS.PlayOneShot(victory);
        }
        //player already lost
        else if (currentState == GameState.gameOver)
        {
            myAS.PlayOneShot(myAS.clip);
        }
        //Player guesses correctly
        else if (instructions[choiceIndex] == colour)
        {
            //next guess
            PlaySound(colour);
            choiceIndex++;
            //player wins
            if(choiceIndex >= instructions.Count)
            {
                yourTurnText.SetActive(false);
                youWintext.SetActive(true);
                myAS.clip = victory;
                myAS.PlayOneShot(victory);
            }

        }
        //incorrect guess
        else
        {
            yourTurnText.SetActive(false);
            youLosetext.SetActive(true);
            GameOver();
        }

    }

    IEnumerator BeepButton(int button)
    {
        AllButtons[button].image.color = AllButtons[button].colors.pressedColor;
        PlaySound(button);
        yield return new WaitForSeconds(1);
        AllButtons[button].image.color = AllButtons[button].colors.disabledColor;
    }

    IEnumerator SimonSaysWhat()
    {
        //Wait to start game
        print("Game Starting in THREE SECONDS");
        numberText.text = "3";
        yield return new WaitForSeconds(1);
        numberText.text = "2";
        yield return new WaitForSeconds(1);
        numberText.text = "1";
        yield return new WaitForSeconds(1);
        numberText.text = "";

        //Beeping the buttons to show the user the pattern
        for (int c = 0; c < Difficulty; c++)
        {
            yield return StartCoroutine(BeepButton(instructions[c]));
        }

        yield return new WaitForSeconds(1);
        instructionText.SetActive(false);
        yourTurnText.SetActive(true);
        currentState = GameState.playing;
        EnableButtons(true);
    }
    public void PlaySound(int colour)
    {
        //red
        if (colour == 0)
        {
            myAS = RedButton.gameObject.GetComponent<AudioSource>();
            myAS.PlayOneShot(myAS.clip);

        }
        //blue
        else if (colour == 1)
        {
            myAS = BlueButton.gameObject.GetComponent<AudioSource>();
            myAS.PlayOneShot(myAS.clip);

        }
        //yellow
        else if (colour == 2)
        {
            myAS = YellowButton.gameObject.GetComponent<AudioSource>();
            myAS.PlayOneShot(myAS.clip);
        }
        //green
        else
        {
            myAS = GreenButton.gameObject.GetComponent<AudioSource>();
            myAS.PlayOneShot(myAS.clip);
        }
    }

    void EnableButtons(bool directions)
    {
        RedButton.interactable = directions;
        YellowButton.interactable = directions;
        BlueButton.interactable = directions;
        GreenButton.interactable = directions;
    }
}
