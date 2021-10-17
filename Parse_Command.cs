using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    class Parse_Command
    {
        public static List<string> ParseCommandString(string command)
        {
            var result = new List<string>();

            command = command.Trim();
            command += "  ";

            string command_word = "";

            for (int i = 0; i < command.Length - 1; i++)
            {
                if (command[i] == ' ' && command[i + 1] == ' ')
                {
                    if (command_word != "")
                    {
                        command_word = command_word.Trim();
                        result.Add(command_word);
                        command_word = "";
                    }
                }
                else
                {
                    command_word += command[i];
                }

            }
            return result;
        }

        public static void PrintList(List<string> list)
        {
            foreach (var commands in list)
            {
                Console.WriteLine(commands);
            }

        }
        
    }
}
 
