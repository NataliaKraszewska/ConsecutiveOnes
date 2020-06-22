using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabuSearchForConsOnesProblem
{
    class Entries_to_delete
    {
        public int start;
        public int length;
        public int row_id;
        public List<int> columns = new List<int>();


        public Entries_to_delete(int in_start, int in_length, int in_row_id)
        {
            start = in_start;
            length = in_length;
            row_id = in_row_id;
            Get_columns_to_delete();
        }

        public void Get_columns_to_delete()
        {
            for(int i = start; i< start+length; i++)
            {
                columns.Add(i);
            }
        }
    }
}
