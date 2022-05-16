//ABDIEL WONG AVILA

/*****************************************************************************\
|*                                                                            *|
\*****************************************************************************/

using System;
using System.IO;
public class AnyGame
{
    /**************************************************************************\
    |* Game Constants                                                          *|
    \**************************************************************************/

    public static readonly string I_GIVE_UP = "I give up!";

    /**************************************************************************\
    |* Game State                                                              *|
    \**************************************************************************/

    string[] words;
    string selectedWord;
    char firtsLetter;
    bool playerGuessedWordCorrectly;
    bool playerGaveUp;
    int guessCount;

    /**************************************************************************\
    |*                                                                         *|
    \**************************************************************************/

    public static void Main(string[] arg)
    {
        AnyGame ag = new AnyGame();
        ag.Start();
    }

    /**************************************************************************\
    |*                                                                         *|
    \**************************************************************************/

    public AnyGame()
    {
    }

    /**************************************************************************\
    |*                                                                         *|
    \**************************************************************************/

    public void Start()
    {
        string input;
        Init(); // 1. Initialize Variables
        ShowGameStartScreen(); // 2. Show Game Start Screen
        do
        {
            ShowScene(); // 3. Show Board / Scene / Map
            do
            {
                ShowInputOptions(); // 4. Show Input Options
                input = GetInput(); // 5. Get Input
            }
            while (!IsValidInput(input)); // 6. Validate Input
            ProcessInput(input); // 7. Process Input
            UpdateGameState(); // 8. Update Game State
        }
        while (!IsGameOver()); // 9. Check for Termination Conditions
        ShowGameOverScreen(); // 10. Show Game Results
    }

    /**************************************************************************\
    |*                                                                         *|
    \**************************************************************************/

    public void Init()
    {
        words = LoadWordsFromFile();

        Random rnd = new Random();

        int rIndex = rnd.Next(words.Length); //0-99

        selectedWord = words[rIndex];
        firtsLetter = selectedWord[0];
        playerGuessedWordCorrectly = false;
        playerGaveUp = false;
        guessCount = 0;
    }

    /**************************************************************************\
    |*                                                                         *|
    \**************************************************************************/

    public string[] LoadWordsFromFile()
    {
        try
        {
            using (StreamReader fileReader = new StreamReader("words.txt"))
            {
                int wordCount = Convert.ToInt32(fileReader.ReadLine());

                string[] terms = new string[wordCount];

                for (int i = 0; i < wordCount; i++)
                {
                    terms[i] = fileReader.ReadLine();
                }

                return terms;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e); // Print error message
            Environment.Exit(-1); // Abort game execution
            return new string[0];
        }
    }

    /**************************************************************************\
    |*                                                                         *|
    \**************************************************************************/

    public void ShowGameStartScreen()
    {
        Console.WriteLine("Welcome to ISpy!");
        Console.WriteLine($"I spy with my little eye something beginning with... {firtsLetter}");
    }

    /**************************************************************************\
    |*                                                                         *|
    \**************************************************************************/

    public void ShowScene()
    {
        for (int i = 0; i < words.Length; i++)
        {
            Console.Write($"{words[i],-10}   ");

            if ((i + 1) % 4 == 0)
            {
                Console.WriteLine();
            }
        }
    }

    /**************************************************************************\
    |*                                                                          *|
    \**************************************************************************/

    public void ShowInputOptions()
    {
        Console.Write($"\nInput any word starting with\nthe letter {firtsLetter} or \"{I_GIVE_UP}\": ");
    }

    /**************************************************************************\
    |*                                                                          *|
    \**************************************************************************/

    public string GetInput()
    {
        string input = Console.ReadLine().Trim().ToLower();

        return input;
    }

    /**************************************************************************\
    |*                                                                           *|
    \**************************************************************************/

    public bool IsValidInput(string input)
    {
        if (input.Length == 0)
        {
            Console.WriteLine("Please enter a a word.");
            return false;
        }

        else if (input == I_GIVE_UP.ToLower())
        {
            return true;
        }

        else if (input[0] != firtsLetter)
        {
            Console.WriteLine($"The word should start with {firtsLetter}!");
            return false;
        }

        else
        {
            return true;
        }
    }

    /**************************************************************************\
    |*                                                                         *|
    \**************************************************************************/

    public void ProcessInput(string input)
    {
        if (input.Equals(selectedWord))
        {
            playerGuessedWordCorrectly = true;
        }
        else if (input == I_GIVE_UP.ToLower())
        {
            playerGaveUp = true;
        }
        else
        {
            Console.WriteLine("Nope! That was not it! Keep trying!");
        }
    }

    /**************************************************************************\
    |*                                                                        *|
    \**************************************************************************/

    public void UpdateGameState()
    {
        guessCount++;
    }

    /**************************************************************************\
    |*                                                                         *|
    \**************************************************************************/

    public bool IsGameOver()
    {
        return (CheckWin() || CheckLoss());
    }

    /**************************************************************************\
    |*                                                                         *|
    \**************************************************************************/

    public bool CheckWin()
    {
        return playerGuessedWordCorrectly;
    }

    /**************************************************************************\
    |*                                                                        *|
    \**************************************************************************/

    public bool CheckLoss()
    {
        return playerGaveUp;
    }


    /**************************************************************************\
    |*                                                                        *|
    \**************************************************************************/

    public void ShowGameOverScreen()
    {
        ShowScene();
        Console.WriteLine("Game Over!");
        if (CheckWin())
        {
            Console.WriteLine($"You Won! It took you only {guessCount} tries!");
        }
        else if (CheckLoss())
        {
            Console.WriteLine($"You Lost! The secret word was {selectedWord}!");
        }
        else
        {
            Console.WriteLine("Something went really wrong! This is never supposed to happen!");
        }
    }
}