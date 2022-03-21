using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDriven_ArchitectureCNN_1.Task1
{
    public  class Table
    {
        public State State { get;    set; }

        public int StatesCount { get; set; }

        public int TableIndex { get; set; }


        public Table(int id)
        {
            TableIndex = id;
         State = State.Free; // Новый стол всегда свободен

         Random random = new Random();   

         StatesCount = random.Next(1, 5); //Количество мест за каждым столом случайно
        
        
        
        }

        public bool SetState(State state)
        {
            if (state == State)
            { return false; }
             State = state;
            return true;
        
        
        }




    }
}
