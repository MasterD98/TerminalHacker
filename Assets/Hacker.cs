using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
    }
    void ShowMainMenu() {
        Terminal.ClearScreen();
        string Greeting = "Hello Ben";
        Terminal.WriteLine(Greeting);
        Terminal.WriteLine("What Would you like to hack into?" +
            " \nPress 1 for local library " +
            "\nPress 2 for the police station" +
            "\nPress 3 for NASA" +
            "\n\nEnter your selection");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
