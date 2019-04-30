using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace LabNumber8Test
{
    class Program
    {
        static void Main(string[] args)
        {   
            UserInput U = new UserInput();
            //int holds student ID starting with 1
            //string[] holds different info at different indicies:
            //0 = name, 1 = hometown, 2 = favorite food
            //the data can be acessed with GetStudentInfo(studentID, "name"/"hometown"/"favorite food", dictionary)
            Dictionary<int, string[]> studentList = U.RetrieveData();

            string welcomeMessage = "Welcome to our C# class.";
            string studentPrompt = $"Which student would you like to learn more about? (enter a number 1-{studentList.Count}): ";
            bool IDisGood = false, learnAboutStudent = true, moreInfo = true;

            UserInput.PrintInColor(welcomeMessage + "\n", ConsoleColor.Green);            
            
            while (learnAboutStudent)
            {                
                moreInfo = true;
                int studentID = U.GetValidInt(studentPrompt);
                IDisGood = U.CheckStudentID(studentID, studentList.Count);
                if (IDisGood)
                {
                    UserInput.PrintInColor($"Student {studentID} is {U.GetStudentInfo(studentID, "name", studentList)}.\n", ConsoleColor.Yellow);
                    while (moreInfo)
                    {
                        Console.WriteLine($"What would you like to know about {U.GetStudentInfo(studentID, "name", studentList).Split()[0]}?");
                        string userInput = U.GetString("Please enter \"hometown\" or \"favorite food\": ");
                        UserInput.PrintInColor($"{U.GetStudentInfo(studentID, "name", studentList).Split()[0]}'s " +
                            $"{userInput} is {U.GetStudentInfo(studentID, userInput, studentList)}.\n", ConsoleColor.Yellow);
                        moreInfo = EndProgram("Would you like to know more about " +
                            $"{U.GetStudentInfo(studentID, "name", studentList).Split()[0]}? (y/n)", "y", "n");
                    }

                }
                else
                {
                    Console.WriteLine($"Student {studentID} does not exist.");
                }

                learnAboutStudent = EndProgram("Would you like to learn about a different student? (y/n) :", "y", "n");
            }            
            
        }

        public static bool EndProgram(string message, string option1, string option2)
        {
            Console.Write(message);
            string input = Console.ReadLine();

            if (input == option1)
            {
                return true;
            }
            else if (input == option2)
            {
                return false;
            }
            else
            {
                UserInput.PrintInColor("Please enter 'y' for yes and 'n' for no.\n", ConsoleColor.Red);
                return EndProgram(message, option1, option2);
            }
        }
    }
}
