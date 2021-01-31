using UnityEngine;

public class Hacker : MonoBehaviour
{
    int Level;
    enum Screen {
        MainMenu,
        Password,
        Win
    }
    Screen CurrentScreen;
    string Password;
    string[] Level1Passwords = {"books","aisle","self","password","font","borrow"};
    string[] Level2Passwords = {"prisoner","handcuffs","holster","uniform","arrest"};
    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
    }
    void ShowMainMenu() {
        CurrentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("What Would you like to hack into?" +
            " \nPress 1 for local library " +
            "\nPress 2 for the police station" +
            "\nPress 3 for NASA" +
            "\n\nEnter your selection");
    }
    void OnUserInput(string Input)
    {
        if (Input == "menu")
        {
            ShowMainMenu();
        }
        else if (CurrentScreen == Screen.MainMenu) { RunMainMenu(Input); }
        else if (CurrentScreen == Screen.Password) { CheckPassword(Input); }
    }

    


    private void RunMainMenu(string Input)
    {
        bool isValidInput = (Input == "1" || Input == "2");
        if (isValidInput) {
            Level = int.Parse(Input);
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
        Terminal.ClearScreen();
        GenPassword();
        Terminal.WriteLine("Please enter your password");
    }

    void CheckPassword(string input)
    {
        if (input == Password)
        {
            DisplayWinScreen();
        }
        else
        {
            Terminal.WriteLine("Wrong password");
        }
    }
    void GenPassword(){
        switch (Level) {
            case 1:
                Password = Level1Passwords[Random.Range(0, Level1Passwords.Length)];
                break;
            case 2:
                Password = Level2Passwords[Random.Range(0, Level2Passwords.Length)];
                break;
            default:
                Debug.LogError("Invalid level number");
                break;
        }
    }
    void DisplayWinScreen()
    {
        CurrentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
    }

    void ShowLevelReward()
    {
        switch (Level) {
            case 1:
                Terminal.WriteLine("Have a book");
                Terminal.WriteLine(@"
    _______
   /      /,
  /      //
 /______//
(______(/
");
                break;
            case 2:
                Terminal.WriteLine("You got the prison key");
                Terminal.WriteLine(@"
/<__>\
\    /
 '. |
  < |
  < |
  <_/

");
                break;
            default:
                Debug.LogError("Invalid level");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
