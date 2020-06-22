using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabuSearchForConsOnesProblem
{
    class Result_permutation_for_row
    {
        public int row_id;
        public List<Entries_to_delete> entries = new List<Entries_to_delete>();


        public Result_permutation_for_row(Entries_to_delete entry, int id)
        {
            entries.Add(entry);
            row_id = id;
        }

        public int Get_end_of_last_element_in_list()
        {
            if(entries.Count==0)
            {
                return 0;
            }

            int entry_end = 0;
            entry_end = entries[entries.Count - 1].start + entries[entries.Count - 1].length;
            
            return entry_end;
        }
    }
}
