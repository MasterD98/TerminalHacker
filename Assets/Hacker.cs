using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Hacker : MonoBehaviour
{
    int Level;
    enum Screen
    {
        MainMenu,
        Password,
        Win
    }
    Screen CurrentScreen;
    string Password;
    string[] Passwords;
    const string MenuHint = "Type menu/quit to return menu or quit";
    [SerializeField] float startScreenWaitTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Initialize();
        ShowStart();
        Invoke("ShowMainMenu", startScreenWaitTime);
    }

    void Initialize()
    {

        try
        {
            //convert text componet to string array
            Text text = GetComponent<Text>();
            Passwords = text.text.Split('\n');
        }
        catch (FileNotFoundException ex)
        {
            Debug.LogException(ex);
            Application.Quit();
        }

    }

    void ShowStart()
    {
        Terminal.WriteLine(@"

  /\  /\__ _  ___| | _____ _ __  
 / /_/ / _` |/ __| |/ / _ \ '__| 
/ __  / (_| | (__|   <  __/ |    
\/ /_/ \__,_|\___|_|\_\___|_|
                /\/\   __ _ _ __  
               /    \ / _` | '_ \ 
              / /\/\ \ (_| | | | |
              \/    \/\__,_|_| |_|");
    }

    void ShowMainMenu()
    {
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
        if (Input == "menu") { Invoke("ShowMainMenu", 1f); }
        else if (Input == "quit" || Input == "close" || Input == "exit") { Application.Quit(); }
        else if (CurrentScreen == Screen.MainMenu) {RunMainMenu(Input);}
        else if (CurrentScreen == Screen.Password) { CheckPassword(Input); }
    }

    void RunMainMenu(string Input)
    {
        bool isValidInput = (Input == "1" || Input == "2" || Input == "3");
        if (isValidInput)
        {
            Level = int.Parse(Input);
            Invoke("AskForPassword", 1f);
        }
        else if (Input == "007")
        {
            Terminal.WriteLine("Please select a level Mr.Bond");
            Terminal.WriteLine(MenuHint);
        }
        else
        {
            Terminal.WriteLine("Please choose a valid input");
            Terminal.WriteLine(MenuHint);
        }
    }

    void AskForPassword()
    {
        CurrentScreen = Screen.Password;
        Terminal.ClearScreen();
        GenPassword();
        Terminal.WriteLine("Enter your password, " + "hint: " + Password.Anagram());
        Terminal.WriteLine(MenuHint);
    }

    void CheckPassword(string input)
    {
        if (input == Password)
        {
            Invoke("ShowPasswordCorrectMessage", 1f);
            Invoke("DisplayWinScreen", 2f);
        }
        else
        {
            Invoke("ShowWrongPasswordMessage", 1f);
            Invoke("AskForPassword", 2f);
        }
    }

    void ShowWrongPasswordMessage()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine("Password Incorrect!!");
    }

    void GenPassword()
    {
        switch (Level)
        {
            case 1:
                do
                {
                    Password = Passwords[Random.Range(0, Passwords.Length)];
                } while (Password.Length > 4);//1,2,3,4
                break;
            case 2:
                do
                {
                    Password = Passwords[Random.Range(0, Passwords.Length)];
                } while (Password.Length > 8 || Password.Length < 5);//8,7,6,5,
                break;
            case 3:
                do
                {
                    Password = Passwords[Random.Range(0, Passwords.Length)];
                } while (Password.Length < 9); //9,10,11,...
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
        switch (Level)
        {
            case 1:
                Terminal.WriteLine("You Got into Library..! \nNow you can have any book you want");
                Terminal.WriteLine(@"
   ________
  /      //
 /______//
(______(/
");
                Terminal.WriteLine("Play again for greater challenge");
                break;
            case 2:
                Terminal.WriteLine("You got into Police Station\nand found the prison key");
                Terminal.WriteLine(@" 
 __
/o \__________
\__/-=`=`=`=`/

");
                Terminal.WriteLine("Play again for greater challenge");
                break;
            case 3:
                Terminal.WriteLine("You got into NASA server!!");
                Terminal.WriteLine(@"
 _ __   __ _ ___  __ _ 
| '_ \ / _` / __|/ _` |
| | | | (_| \__ \ (_| |
|_| |_|\__,_|___/\__,_|
");
                Terminal.WriteLine("Wellcome to NASA server");
                break;
            default:
                Debug.LogError("Invalid level");
                break;
        }
        Terminal.WriteLine(MenuHint);
    }

    void ShowPasswordCorrectMessage()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine("Password correct!!");
    }
}
