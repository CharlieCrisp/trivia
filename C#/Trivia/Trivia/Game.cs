using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UglyTrivia
{
    public class Game
    {


        List<string> players = new List<string>();

        int[] places = new int[6];
        int[] purses = new int[6];

        bool[] inPenaltyBox = new bool[6];

        LinkedList<string> popQuestions = new LinkedList<string>();
        LinkedList<string> scienceQuestions = new LinkedList<string>();
        LinkedList<string> sportsQuestions = new LinkedList<string>();
        LinkedList<string> rockQuestions = new LinkedList<string>();

        int currentPlayer = 0;
        bool isGettingOutOfPenaltyBox;
        private string Category1 = "Pop";
        private string Category2 = "Science";
        private string Category3 = "Sports";
        private string Category4 = "Rock";
        private string notEscapingPenaltyBox = "is not getting out of the penalty box";
        private string escapingPenaltyBox = "is getting out of the penalty box";
        private string correctAnswer = "Answer was correct!!!!";
        private string incorrectlyAnswered = "Question was incorrectly answered";


        public Game()
        {
            for (int i = 0; i < 50; i++)
            {
                popQuestions.AddLast(createQuestion(Category1, i));
                scienceQuestions.AddLast(createQuestion(Category2, i));
                sportsQuestions.AddLast(createQuestion(Category3, i));
                rockQuestions.AddLast(createQuestion(Category4, i));
            }
        }

        public String createQuestion(string category, int index)
        {
            return category + " Question " + index;
        }

        public bool isPlayable()
        {
            return (howManyPlayers() >= 2);
        }

        public bool add(String playerName)
        {
            players.Add(playerName);
            places[howManyPlayers()] = 0;
            purses[howManyPlayers()] = 0;
            inPenaltyBox[howManyPlayers()] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
            return true;
        }

        public int howManyPlayers()
        {
            return players.Count;
        }

        public bool isOdd(int roll)
        {
            if (roll % 2 != 0) return true;
            else return false;
        }

        public void printNewLocation(string player, int place)
        {
            Console.WriteLine(player + "'s new location is " + place);
        }

        public void roll(int roll)
        {
            Console.WriteLine(players[currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (inPenaltyBox[currentPlayer])
            {
                if (isOdd(roll))
                {
                    isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(players[currentPlayer] + escapingPenaltyBox);
                    movePlayer(roll);
         

                    printNewLocation(players[currentPlayer], places[currentPlayer]);
                    printCurrentCategory();
                    askQuestion();
                }
                else
                {
                    Console.WriteLine(players[currentPlayer] + notEscapingPenaltyBox);
                    isGettingOutOfPenaltyBox = false;
                }

            }
            else
            {

                movePlayer(roll);

                printNewLocation(players[currentPlayer], places[currentPlayer]);
                printCurrentCategory();
                askQuestion();
            }

        }

        private void movePlayer(int roll)
        {
            places[currentPlayer] = (places[currentPlayer] + roll) % 12;
        }

        private void printCurrentCategory()
        {
            Console.WriteLine("The category is " + currentCategory());
        }

        private void askQuestion()
        {
            if (currentCategory() == Category1)
            {
                Console.WriteLine(popQuestions.First());
                popQuestions.RemoveFirst();
            }
            if (currentCategory() == Category2)
            {
                Console.WriteLine(scienceQuestions.First());
                scienceQuestions.RemoveFirst();
            }
            if (currentCategory() == Category3)
            {
                Console.WriteLine(sportsQuestions.First());
                sportsQuestions.RemoveFirst();
            }
            if (currentCategory() == Category4)
            {
                Console.WriteLine(rockQuestions.First());
                rockQuestions.RemoveFirst();
            }
        }


        private String currentCategory()
        {
            if (places[currentPlayer] == 0) return Category1;
            if (places[currentPlayer] == 4) return Category1;
            if (places[currentPlayer] == 8) return Category1;
            if (places[currentPlayer] == 1) return Category2;
            if (places[currentPlayer] == 5) return Category2;
            if (places[currentPlayer] == 9) return Category2;
            if (places[currentPlayer] == 2) return Category3;
            if (places[currentPlayer] == 6) return Category3;
            if (places[currentPlayer] == 10) return Category3;
            return Category4;
        }

        public bool wasCorrectlyAnswered()
        {
            if (inPenaltyBox[currentPlayer])
            {
                if (isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine(correctAnswer);
                    purses[currentPlayer]++;
                    Console.WriteLine(players[currentPlayer]
                            + " now has "
                            + purses[currentPlayer]
                            + " Gold Coins.");

                    bool winner = didPlayerWin();
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;

                    return winner;
                }
                else
                {
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;
                    return true;
                }



            }
            else
            {

                Console.WriteLine(correctAnswer);
                purses[currentPlayer]++;
                Console.WriteLine(players[currentPlayer]
                        + " now has "
                        + purses[currentPlayer]
                        + " Gold Coins.");

                bool winner = didPlayerWin();
                currentPlayer++;
                if (currentPlayer == players.Count) currentPlayer = 0;

                return winner;
            }
        }

        public bool wrongAnswer()
        {
            Console.WriteLine(incorrectlyAnswered);
            Console.WriteLine(players[currentPlayer] + " was sent to the penalty box");
            inPenaltyBox[currentPlayer] = true;

            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
            return true;
        }


        private bool didPlayerWin()
        {
            return !(purses[currentPlayer] == 6);
        }
    }

}
