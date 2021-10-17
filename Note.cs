using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{  
    class Note
    {
        public Note()
        {
            name = "Unknown ";
            description = "None";
            status = "not_completed";
            date = ""; 
        }

        //     Может быть private не будет работать

        public string name;
        public string description;
        public string status;
        public string date;

        private string GetDateForNote()
        {
            Console.WriteLine("Введите дату события в формате \"ДД.ММ.ГГГГ\"");
            Console.WriteLine("или нажмите \"Enter\", если заметка на сегодня: ");
            string date = Console.ReadLine();
            if (date == "")
            {
                string today = DateTime.Now.ToString("yyyy-MM-dd");
                return today;
            }
            else
            {
                string entered_date = DateTime.ParseExact(date, "dd.MM.yyyy",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                return entered_date;
            }
        }

        public Note GetAddNoteParameters(List<string> list)
        {
            Note note = new Note
            {
                name = list[1],
                description = list[2],
                date = GetDateForNote()
            };

            return note;
        }

        public Note GetDelNoteParameters(List<string> list)
        {
            Note note = new() { name = list[1] };
            return note;
        }

        public Note GetEditNoteParametrs(List<string> list)
        {
            string old_note_name = list[1];

            /*ввиду особенностей выбранного способа передачи параметров
            заметки передадим параметр old_note_name в поле status
            созданного объекта note для передачи параметров в SQL запрос*/

            Note note = new()
            {
                name = list[2],
                description = list[3],
                status = old_note_name // используется поле статус для простоты транспортировки old_note_name
            };

            return note;
        }
        public Note GetChangeStatusNoteParametrs(List<string> list)
        {
            Note note = new();
            note.name = list[1];

            if (list[2] == "+")
            {
                note.status = "completed";
            }
            else if (list[2] == "-")
            {
                note.status = "not_completed";
            }

            return note;
        }
    }
}
