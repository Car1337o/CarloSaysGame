using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public int Difficulty;
    List<int> instructions = new List<int>();
    int choiceIndex = 0;
    enum GameState { setup, playing, gameOver };
    GameState currentState = GameState.setup;

    //button objects
    public Button redButton;
    public Button blueButton;
    public Button yellowButton;
    public Button greenButton;

    //audio variables
    AudioSource myAS;

    void Start()
    {
        EnableButtons(false);
        BuildPuzzle();
        StartCoroutine(SimonSaysWhat());


    }

    public void GameOver()
    {
        EnableButtons(false);
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
            //print("answer #: " + (n + 1) + " value: " + instructions[n]);

        }

    }

    public void ButtonPress(int colour)
    {

 
        if (currentState == GameState.playing && choiceIndex < instructions.Count)
        {
            if (instructions[choiceIndex] == colour)
            {
                PlaySound(colour);
                choiceIndex++;
            }
            else
            {
                print("Wrong");
                GameOver();               
            }
        }
    }

    IEnumerator SimonSaysWhat()
    {
        //Wait to start game
        print("Game Starting in THREE SECONDS");
        yield return new WaitForSeconds(1);
        print("2");
        yield return new WaitForSeconds(1);
        print("1");
        yield return new WaitForSeconds(1);

        //Beeping the buttons
        for (int c = 0; c< Difficulty; c++)
        {

            //RED
            if (instructions[c] == 0)
            {
                redButton.image.color = redButton.colors.pressedColor;
                PlaySound(0);

                //Cheat codes:
                print((c + 1) + ": red");


                yield return new WaitForSeconds(1);
                redButton.image.color = Color.red;


            }
            //BLUE
            else if(instructions[c] == 1)
            {
                blueButton.image.color = blueButton.colors.pressedColor;
                PlaySound(1);

                //Cheat codes:
                print((c + 1) + ": blue");

                yield return new WaitForSeconds(1);
                blueButton.image.color = Color.blue;


            }
            //YELLOW
            else if(instructions[c] == 2)
            {
                yellowButton.image.color = yellowButton.colors.pressedColor;
                PlaySound(2);

                //Cheat codes:
                print((c + 1) + ": yellow");

                yield return new WaitForSeconds(1);
                yellowButton.image.color = Color.yellow;


            }
            //GREEN
            else
            {
                greenButton.image.color = greenButton.colors.pressedColor;
                PlaySound(69420);

                //Cheat codes:
                print((c + 1) + ": green");

                yield return new WaitForSeconds(1);
                greenButton.image.color = Color.green;


            }


        }
        yield return new WaitForSeconds(1);
        print("Your turn to play!");
        currentState = GameState.playing;
        EnableButtons(true);

    }

    public void PlaySound(int colour)
    {
        //red
        if(colour == 0)
        {
            myAS = redButton.gameObject.GetComponent<AudioSource>();
            myAS.PlayOneShot(myAS.clip);

        }
        //blue
        else if(colour == 1)
        {
            myAS = blueButton.gameObject.GetComponent<AudioSource>();
            myAS.PlayOneShot(myAS.clip);

        }
        //yellow
        else if(colour == 2)
        {
            myAS = yellowButton.gameObject.GetComponent<AudioSource>();
            myAS.PlayOneShot(myAS.clip);
        }
        //green
        else
        {
            myAS = greenButton.gameObject.GetComponent<AudioSource>();
            myAS.PlayOneShot(myAS.clip);
        }
    }


    void EnableButtons(bool directions)
    {
        redButton.interactable = directions;
        yellowButton.interactable = directions;
        blueButton.interactable = directions;
        greenButton.interactable = directions;
    }
}
