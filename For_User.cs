using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    class For_User
    {
        public static void InviteToEnterCommand()
        {
            Console.WriteLine("Введите команду:\n");
        }
        public static void PrintInstruction()
        {
            Console.WriteLine("Для добавления заметки введите: <ADD>  <Название заметки>  <Описание заметки>.");
            Console.WriteLine("Для удаления заметки введите: <DEL>  <Название заметки>.");
            Console.WriteLine("Для редактирования заметки введите: <EDIT>  <Название заметки>  <Новое название>  <Новое описание>.");
            Console.WriteLine("Для изменения статуса заметки введите: <STA>  <Название заметки>  <+>, если задача выполнена и <->, если нет.");
            Console.WriteLine("Для выведения списка дел введите: <LIST>.");
            Console.WriteLine("Для закрытия программы введите: <exit>.\n");

            Console.WriteLine("Параметры команд, такие как \"название заметки\", \"описание заметки\" и др. разделяйте более чем одним пробелом.\n");
        }
        public static void PrintCommandError()
        {
            Console.WriteLine("Некорректная команда!\n");
        }
        public static void PrintErrorAndInstruction()
        {
            PrintCommandError();
            PrintInstruction();
        }
    }
}
