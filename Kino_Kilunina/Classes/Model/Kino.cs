using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kino_Kilunina.Classes.Model
{
    public class Kino
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountZal { get; set; }
        public int Count { get; set; }

        public Kino(int Id, string Name, int CountZal, int Count)
        {
            this.Id = Id;
            this.Name = Name;
            this.CountZal = CountZal;
            this.Count = Count;
        }
    }
}
