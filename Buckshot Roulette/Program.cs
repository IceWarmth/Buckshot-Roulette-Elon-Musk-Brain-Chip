using System;
using System.Globalization;

namespace Buckshot_Roulette
{
    internal class Program
    {
        //Create empty lines system (easiest thing in the world)
        static void DontShow()
        {
            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine();
            }
        }
        static int Input(string s)
        {
            int shells;
            bool error = false;

            //Obtain input and translate it
            for (int i = 0; i < 1;)
            {
                //User input
                if (!error)
                {
                    error = true;
                }
                else
                {
                    Console.WriteLine("If this message was executed, a logical error occurred");
                    Console.WriteLine();
                }
                Console.WriteLine("Please enter the number of " + s + " shells");
                string response = Console.ReadLine();

                //Response length check
                if (response.Length > 1 || response.Length <= 0)
                {
                    continue;
                }
                else
                {
                    //Response translation
                    char responseChar1 = response[0];
                    shells = responseChar1 - 48;

                    //Response number check
                    if (shells <= 0 || shells > 4)
                    {
                        continue;
                    }
                }
                Console.WriteLine();
                return shells;
            }
            shells = 0;
            return shells;
        }
        static void Main(string[] args)
        {
            bool initialLoop = true;
            bool error = false;

            while (initialLoop)
            {
                //Erase lines
                DontShow();

                //Error alert
                if (!error)
                {
                    error = true;
                }
                else
                {
                    Console.WriteLine("If this message was executed, a logical error occurred");
                    Console.WriteLine();
                }

                //Numbers of shells to be obtained
                int liveInt = Input("live");
                int blankInt = Input("blank");
                double live = liveInt;
                double blank = blankInt;

                //Probability and total calculations
                int total = liveInt + blankInt;
                int totalP = total;
                double liveP = live;

                //Check if the shell arrangement is correct
                if (blank - live != 1 && blank - live != 0)
                {
                    continue;
                }

                //Initial visualisation of the shell arrangement
                string[] arrangement = { "", "", "", "", "", "", "", "" };
                string[] looksCool = { "", "", "", "", "", "", "", "" };
                for (int i = 0; i != total; i++)
                {
                    int s = 7 - i;
                    arrangement[s] = "(?) ";
                }
                for (int i = 0; i != total; i++)
                {
                    int j = i + 1;
                    looksCool[i] = " " + j + "  ";
                }

                error = false;

                //Main loop
                bool mainLoop = true;
                while (mainLoop && total != 0)
                {
                    //Erase lines
                    DontShow();

                    //Error alert
                    if (!error)
                    {
                        error = true;
                    }
                    else
                    {
                        Console.WriteLine("If this message was executed, a logical error occurred");
                        Console.WriteLine();
                    }

                    //Update first shell
                    switch (arrangement[8 - total])
                    {
                        case "(?) ":
                            arrangement[8 - total] = "[?] ";
                            break;

                        case "(0) ":
                            arrangement[8 - total] = "[0] ";
                            break;

                        case "(X) ":
                            arrangement[8 - total] = "[X] ";
                            break;
                    }

                    //Check if probability is flat, if so change all unknowns to blank or live
                    double probability = liveP * 100 / totalP;
                    int j = 0;
                    switch (probability)
                    {
                        case 0:
                            foreach (string i in arrangement)
                            {
                                if (i == "(?) ")
                                {
                                    arrangement[j] = "(X) ";
                                }
                                else if (i == "[?] ")
                                {
                                    arrangement[j] = "[X] ";
                                }
                                j++;
                            }
                            break;

                        case 100:
                            foreach (string i in arrangement)
                            {
                                if (i == "(?) ")
                                {
                                    arrangement[j] = "(0) ";
                                }
                                else if (i == "[?] ")
                                {
                                    arrangement[j] = "[0] ";
                                }
                                j++;
                            }
                            break;
                    }

                    //Update arrangement and round probability
                    string chamber = "-> " + arrangement[0] + arrangement[1] + arrangement[2] + arrangement[3] + arrangement[4] + arrangement[5] + arrangement[6] + arrangement[7];
                    string chamber2 = "   " + looksCool[0] + looksCool[1] + looksCool[2] + looksCool[3] + looksCool[4] + looksCool[5] + looksCool[6] + looksCool[7];
                    probability = Math.Round(probability);

                    //Updated text
                    Console.WriteLine();
                    Console.WriteLine(chamber);
                    Console.WriteLine(chamber2);
                    Console.WriteLine();
                    Console.WriteLine(probability + "% probability that next (?) is live");
                    Console.WriteLine();
                    Console.WriteLine(live + "L  " + blank + "B  " + total + "TOT  ");
                    Console.WriteLine();
                    Console.WriteLine("Select an action by entering a number");
                    Console.WriteLine();
                    Console.WriteLine("1 - Register a shot/beer usage");
                    Console.WriteLine("2 - Register m. glass/phone info");
                    Console.WriteLine("0 - Reset");
                    Console.WriteLine();
                    string action = Console.ReadLine();

                    //Reset
                    if (action == "0")
                    {
                        error = false;
                        break;
                    }

                    //Register a shot
                    else if (action == "1")
                    {
                        string type;
                        bool known = false;

                        //Check if the shell is already known
                        if (arrangement[8 - total] == "[X] " || probability == 0)
                        {
                            type = "0";
                            known = true;
                        }
                        else if (arrangement[8 - total] == "[0] " || probability == 100)
                        {
                            type = "1";
                            known = true;
                        }
                        else
                        {
                            //Type input
                            Console.WriteLine();
                            Console.WriteLine("Select a type by entering a number");
                            Console.WriteLine();
                            Console.WriteLine("0 - Blank");
                            Console.WriteLine("1 - Live");
                            Console.WriteLine();
                            type = Console.ReadLine();
                        }

                        //Input incorrect
                        if (type != "0" && type != "1")
                        {
                            continue;
                        }

                        //Blank or live
                        switch (type)
                        {
                            case "0":
                                blank -= 1;
                                total -= 1;
                                if (!known)
                                {
                                    totalP -= 1;
                                }
                                arrangement[7 - total] = "";
                                looksCool[total] = "";
                                error = false;
                                break;

                            case "1":
                                live -= 1;
                                total -= 1;
                                if (!known)
                                {
                                    liveP -= 1;
                                    totalP -= 1;
                                }
                                arrangement[7 - total] = "";
                                looksCool[total] = "";
                                error = false;
                                break;
                        }
                    }
                    
                    //Register phone info
                    else if (action == "2")
                    {
                        //Position input
                        Console.WriteLine();
                        Console.WriteLine("Select the position by entering a number from 2 to 8 (corresponds to what the phone said)");
                        Console.WriteLine("Alternatively, enter '1' for magnifying glass info");
                        Console.WriteLine();
                        string pos = Console.ReadLine();
                        int position = pos[0] - 48;

                        //Input incorrect
                        if (pos.Length <= 0 || pos.Length > 1)
                        {
                            continue;
                        }
                        else if (position < 1 || position > total || arrangement[7 - total + position] == "(X) " || arrangement[7 - total + position] == "(0) ")
                        {
                            continue;
                        }

                        string type;
                        if (probability == 0)
                        {
                            type = "0";
                        }
                        else if (probability == 100)
                        {
                            type = "1";
                        }
                        else
                        {
                            //Type input
                            Console.WriteLine();
                            Console.WriteLine("Select a type by entering a number");
                            Console.WriteLine();
                            Console.WriteLine("0 - Blank");
                            Console.WriteLine("1 - Live");
                            Console.WriteLine();
                            type = Console.ReadLine();
                        }

                        //Input incorrect
                        if (type != "0" && type != "1" || type == "0" && probability == 100 || type == "1" && probability == 0)
                        {
                            continue;
                        }

                        //Blank or live
                        switch (type)
                        {
                            case "0":
                                totalP -= 1;
                                if (position == 1)
                                {
                                    arrangement[7 - total + position] = "[X] ";
                                }
                                else
                                {
                                    arrangement[7 - total + position] = "(X) ";
                                }
                                error = false;
                                break;

                            case "1":
                                liveP -= 1;
                                totalP -= 1;
                                if (position == 1)
                                {
                                    arrangement[7 - total + position] = "[0] ";
                                }
                                else
                                {
                                    arrangement[7 - total + position] = "(0) ";
                                }
                                error = false;
                                break;
                        }
                    }
                }
            }
        }
    }
}