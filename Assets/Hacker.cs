using System.IO;
using System.Threading.Tasks;
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
    string[] Passwords;
    const string MenuHint = "Type menu at any time to return menu";
    // Start is called before the first frame update
    async void Start()
    {
        Initialize();
        ShowStart();
        await Task.Delay(3000);
        ShowMainMenu();
    }

    void Initialize()
    {
        
        try{
            Passwords = File.ReadAllLines("Assets/Passwords.txt");
        }catch (FileNotFoundException ex)
        {
            Debug.LogException(ex);
            Task.Delay(5000);
            Application.Quit();
        }
        
    }

    void ShowStart()
    {
        Terminal.WriteLine(@"
 _                _             
| |              | |            
| |__   __ _  ___| | _____ _ __ 
| '_ \ / _` |/ __| |/ / _ \ '__|
| | | | (_| | (__|   <  __/ |   
|_| |_|\__,_|\___|_|\_\___|_| 
");
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
    async void OnUserInput(string Input)
    {
        if (Input == "menu"){ShowMainMenu();}
        else if (Input=="quit"|| Input=="close"||Input=="exit") {Application.Quit();}
        else if (CurrentScreen == Screen.MainMenu) { RunMainMenu(Input); }
        else if (CurrentScreen == Screen.Password) { await CheckPasswordAsync(Input); }
    }

    


    void RunMainMenu(string Input)
    {
        bool isValidInput = (Input == "1" || Input == "2" || Input=="3");
        if (isValidInput) {
            Level = int.Parse(Input);
            AskForPassword();
        }
        else if (Input == "007"){
            Terminal.WriteLine("Please select a level Mr.Bond");
            Terminal.WriteLine(MenuHint);
        }
        else{
            Terminal.WriteLine("Please choose a valid input");
            Terminal.WriteLine(MenuHint);
        }
    }

    void AskForPassword() {
        CurrentScreen = Screen.Password;
        Terminal.ClearScreen();
        GenPassword();
        Terminal.WriteLine("Enter your password, "+"hint: "+Password.Anagram());
        Terminal.WriteLine(MenuHint);
    }

    async Task CheckPasswordAsync(string input)
    {
        if (input == Password)
        {
            DisplayWinScreen();
        }
        else
        {
            await Task.Delay(250);
            Terminal.ClearScreen();
            Terminal.WriteLine("Sorry,wrong password");
            await Task.Delay(1000);
            AskForPassword();
        }
    }
    void GenPassword(){
        switch (Level) {
            case 1:
                do
                {
                    Password = Passwords[Random.Range(0, Passwords.Length)];
                } while (Password.Length>4);//1,2,3,4
                break;
            case 2:
                do
                {
                    Password = Passwords[Random.Range(0, Passwords.Length)];
                } while (Password.Length >8 || Password.Length < 5);//8,7,6,5,
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
                Terminal.WriteLine("Play again for greater challenge");
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
