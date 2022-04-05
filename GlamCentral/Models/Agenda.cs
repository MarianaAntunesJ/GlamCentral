using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GlamCentral.Models
{
    public class Agenda
    {
        public int Id { get; set; }
        /*public int Cliente { get; set; }
        public int Funcionario { get; set; }
        public int Procedimento { get; set; }
        public int Procedimento { get; set; }*/
        public string Subject { get; set; }
        public string Description { get; set; }

        [Required(AllowEmptyStrings = true)]
        public DateTime Start { get; set; }

        [Required(AllowEmptyStrings = true)]
        public DateTime End { get; set; }
        public string ThemeColor { get; set; }
        public bool IsFullDay { get; set; }

        public Agenda()
        {
        }

        public Agenda(int id, string subject, string description, DateTime start, DateTime end)
        {
            Id = id;
            Subject = subject;
            Description = description;
            Start = start;
            End = end;
        }
    }
}
