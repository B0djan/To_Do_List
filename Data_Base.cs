using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace ToDoList
{
    class Data_Base
    {
        private static NpgsqlConnection Connection = new ("Server=ella.db.elephantsql.com;" +
            " Port=5432; Username=gpsnyfqm; Password=DYdmEsuLlhiw_GHsWLUn8u_rvFv3CJDk; Database=gpsnyfqm");
        private void EnterSqlRequestValues (NpgsqlCommand command, Note note)
        {
            command.Parameters.Add("@name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = note.name;
            command.Parameters.Add("@description", NpgsqlTypes.NpgsqlDbType.Varchar).Value = note.description;
            command.Parameters.Add("@status", NpgsqlTypes.NpgsqlDbType.Varchar).Value = note.status;
            command.Parameters.Add("@date", NpgsqlTypes.NpgsqlDbType.Varchar).Value = note.date;
        }


        public NpgsqlConnection GetConnection()
        {
            return Connection;
        }
        public void OpenConnection()
        {
            if(Connection.State == System.Data.ConnectionState.Closed)
            {
                Connection.Open();
            }
        }
        public void CloseConnection()
        {
            if (Connection.State == System.Data.ConnectionState.Open)
            {
                Connection.Close();
            }
        }
        public static void TestConnection()
        {
            Data_Base db = new();
            try
            {
                db.OpenConnection();

                if (db.GetConnection().State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Connected!\n");
                }

                db.CloseConnection();

                For_User.PrintInstruction();
                For_User.InviteToEnterCommand();
            }
            catch (NpgsqlException)
            {
                Console.WriteLine("No coonection avalible!\n" + ContactMessage());

            }
            catch (System.TypeInitializationException)
            {
                Console.WriteLine("Incorrect connection request format!\n" + ContactMessage());

            }
            catch (System.Net.Sockets.SocketException)
            {
                Console.WriteLine("No Interent coonection! Please connect to the Internet.\n");

            }
        }

        public static void AddNote (Note note) 
        {
            Data_Base db = new Data_Base();

            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Notes (name, description, status, date) " +
                "VALUES (@name, @description, @status, @date);", db.GetConnection());

            db.EnterSqlRequestValues(command, note);

            db.OpenConnection();
            {
                if(command.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine("Заметка добавлена!\n");
                } else
                {
                    For_User.PrintErrorAndInstruction();
                }
            }
            db.CloseConnection();

        }

        public static void DeliteNote(Note note)
        {
            Data_Base db = new Data_Base();
                     
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM Notes WHERE name = @name;", db.GetConnection());

            db.EnterSqlRequestValues(command, note);

            db.OpenConnection();
            {
                if (command.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine("Заметка удалена!\n");
                }
                else
                {
                    For_User.PrintErrorAndInstruction();
                }
            }
            db.CloseConnection();

        }

        public static void ChangeStatusNote(Note note)
        {
            Data_Base db = new Data_Base();

            NpgsqlCommand command = new NpgsqlCommand("UPDATE Notes SET status = @status WHERE name = @name;", db.GetConnection());

            db.EnterSqlRequestValues(command, note);

            db.OpenConnection();
            {
                if (command.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine("Статус изменен!\n");
                }
                else
                {
                    For_User.PrintErrorAndInstruction();
                }
            }
            db.CloseConnection();
        }

        public static void EditNote(Note note)
        {
            Data_Base db = new Data_Base();

            NpgsqlCommand command = new NpgsqlCommand("UPDATE Notes SET name = @name, description = @description WHERE name = @status;", db.GetConnection());
            
            /*Смотри пояснение в Note.GetEditNoteParametrs*/
            
            db.EnterSqlRequestValues(command, note);

            db.OpenConnection();
            {
                if (command.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine("Заметка изменена!\n");
                }
                else
                {
                    For_User.PrintErrorAndInstruction();
                }
            }
            db.CloseConnection();
        }

        public static void GetList()
        {
            Data_Base db = new Data_Base();

            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM Notes;", db.GetConnection());

            db.OpenConnection();
            {
                NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("{0,-2} {1,-22} {2,-63} {3,-13} {4,-10}\n", reader.GetName(0), reader.GetName(1), reader.GetName(2), reader.GetName(3), reader.GetName(4));

                    while (reader.Read())
                    {
                        object id = reader.GetValue(0);
                        object name = reader.GetValue(1);
                        object description = reader.GetValue(2);
                        object status = reader.GetValue(3);
                        object date = reader.GetValue(4);

                        Console.WriteLine("{0,-2} {1,-22} {2,-63} {3,-13} {4,-10}", id, name, description, status, date);
                    }

                    Console.WriteLine();                    
                }
                reader.Close();
            }
            db.CloseConnection();
        }


        private static string ContactMessage()
        {
            string message = "If you are reading this, please let us know by email \"bs@mt11.su\"\n";
            return message;
        }
    }

}
