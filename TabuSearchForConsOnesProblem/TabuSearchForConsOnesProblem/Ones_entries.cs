using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabuSearchForConsOnesProblem
{
    class Ones_entries
    {
       public int start;
       public int length;
        int row_id;


        public Ones_entries(int in_start, int in_length, int in_row_id)
        {
            start = in_start;
            length = in_length;
            row_id = in_row_id;
        }
    }
}
