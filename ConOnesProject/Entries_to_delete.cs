using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConOnesProject
{
    class Entries_to_delete
    {

        public int start;
        public int length;
        public int id;
        public List<int> columns = new List<int>();
        public List<int> columns_in_shorter_row = new List<int>();


        public Entries_to_delete(int in_start, int in_length, int in_id)
        {
            start = in_start;
            length = in_length;
            id = in_id;
            Get_columns_to_delete();
        }

        public void Get_columns_to_delete()
        {
            /*if(start < 0 )
            {
                Console.WriteLine("columns to del in entries to del: ");
                Console.WriteLine("start: " + start);
                for (int i = start; i < start + length; i++)
                {
                    Console.Write(i + " ");
                }
                Console.WriteLine();

            }*/

            for (int i = start; i < start + length; i++)
            {
                columns.Add(i);
            }
        }
    }
}
