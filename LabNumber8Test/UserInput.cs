using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace LabNumber8Test
{
    class UserInput
    {

        public Dictionary<int,string[]> RetrieveData()
        {
            //This function creates a dictionary by reading a file
            //that was created with a separate program and
            //serialized with a binary formatter
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                FileInfo fileInfo = new FileInfo(@"studentList.bin");

                Dictionary<int, string[]> studentList;
                using (var binaryFile = fileInfo.OpenRead())
                {
                    studentList = (Dictionary<int, string[]>)binaryFormatter.Deserialize(binaryFile);
                }

                return studentList;

            }
            catch (IOException E)
            {
                Console.WriteLine(E.Message);
                Dictionary<int, string[]> defaultDict = new Dictionary<int, string[]>();
                return defaultDict;
            }
        }

        public static void PrintInColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }

        public int GetValidInt(string message)
        {
            Console.Write(message);
            try
            {
                int input = int.Parse(Console.ReadLine());
                return input;
            }
            catch(FormatException E)
            {
                PrintInColor(E.Message + "\n", ConsoleColor.Red);
                return GetValidInt(message);
            } 
        }

        public bool CheckStudentID(int ID, int dictLength)
        {
            try
            {
                if(ID > dictLength || ID < 1)
                {
                    throw new IndexOutOfRangeException();
                }
                return true;
            }
            catch(IndexOutOfRangeException I)
            {
                PrintInColor(I.Message + " Invalid input detected.\n", ConsoleColor.Red);
                return false;
            }
        }

        public string GetStudentInfo(int key, string index, Dictionary<int, string[]> dict)
        {
            switch (index)
            {
                case "name": return dict[key][0];

                case "hometown": return dict[key][1];

                case "favorite food": return dict[key][2];

                default: return "";
                                    
            }            
        }

        public string GetString(string message = "Please enter a string: ", string error = "Error - oops, it looks like you forgot to enter something!\n")
        {
            Console.Write(message);
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                PrintInColor(error, ConsoleColor.Red);
                return GetString(message, error);
            }
            else if (input == "hometown")
            {
                return input;
            }
            else if (input == "favorite food")
            {
                return input;
            }
            else
            {
                PrintInColor("That data does not exist.\n", ConsoleColor.Red);
                return GetString(message,error);   
            }
        }
    }
}
