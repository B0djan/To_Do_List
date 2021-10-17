using System;
using Npgsql;
using System.Collections.Generic;

namespace ToDoList
{
    class Program
    {
        static void Main(string[] args)
        {
            Data_Base.TestConnection();          

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "exit")
                {
                    break;
                } 
                else if (command == "")
                {
                    For_User.PrintInstruction();
                    For_User.InviteToEnterCommand();
                    continue;
                }

                List<string> parsed_command = Parse_Command.ParseCommandString(command);

                try 
                {
                    switch (parsed_command[0])
                    {
                        case "ADD":

                            Note NoteAdd = new();
                            Data_Base.AddNote(NoteAdd.GetAddNoteParameters(parsed_command));

                            break;

                        case "DEL":

                            Note NoteDel = new();
                            Data_Base.DeliteNote(NoteDel.GetDelNoteParameters(parsed_command));

                            break;

                        case "EDIT":

                            Note NoteEdit = new();
                            Data_Base.EditNote(NoteEdit.GetEditNoteParametrs(parsed_command));
                            break;

                        case "STA":

                            Note NoteSta = new();
                            Data_Base.ChangeStatusNote(NoteSta.GetChangeStatusNoteParametrs(parsed_command));

                            break;

                        case "LIST":

                            Data_Base.GetList();
                            break;

                        default:
                            For_User.PrintErrorAndInstruction();
                            break;
                    }
                }
                catch
                {
                    For_User.PrintErrorAndInstruction(); 
                }
            }

            
        }
        
    }
}
