using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    int Level;
    enum Screen {
        MainMenu,
        Password,
        Win
    }
    Screen CurrentScreen=Screen.MainMenu;
    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
    }
    void ShowMainMenu() {
        Terminal.ClearScreen();
        Terminal.WriteLine("What Would you like to hack into?" +
            " \nPress 1 for local library " +
            "\nPress 2 for the police station" +
            "\nPress 3 for NASA" +
            "\n\nEnter your selection");
    }
    void OnUserInput(string Input)
    {
        if (Input == "menu"){ShowMainMenu();}
        else if (CurrentScreen == Screen.MainMenu) { RunMainMenu(Input); }
    }

    private void RunMainMenu(string Input)
    {
        if (Input == "1"){
            Level = 1;
            StartGame();
        }
        else if (Input == "2"){
            Level = 2;
            StartGame();
        }
        else if (Input == "3"){
            Level = 3;
            StartGame();
        }
        else if (Input == "007"){
            Terminal.WriteLine("Please select a level Mr.Bond");
        }
        else{
            Terminal.WriteLine("Please choose a valid input");
        }
    }

    void StartGame() {
        CurrentScreen = Screen.Password;
        Terminal.WriteLine("You have chosen level "+Level);
        Terminal.WriteLine("Please enter your password");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
