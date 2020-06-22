﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabuSearchForConsOnesProblem
{
    class Row
    {
        List<int> row;
        List<int> rev_row;
        int position_in_matrix;
        public int number_of_results;
        public List<Entries_to_delete> entries_to_delete = new List<Entries_to_delete>();
        public int number_of_groups;


        public Row(List<int> in_row, int id)
        {
            row = in_row;
            //in_row.Reverse();
            //reverse_row = in_row;
            position_in_matrix = id;
        }


        private List<int> Get_ones_position(bool reverse)
        {
            List<int> ones_position = new List<int>();
            if (reverse == false)
            {
                for (int i = 0; i < row.Count; i++)
                {
                    if (row[i] == 1)
                    {
                        ones_position.Add(i);
                    }
                }
            }
            else
            {
                for (int i = 0; i < rev_row.Count; i++)
                {
                    if (rev_row[i] == 1)
                    {
                        ones_position.Add(i);
                    }
                }
            }
            return ones_position;
        }


        private List<Ones_entries> Get_ones_entries(bool reverse)
        {
            List<int> ones_position = Get_ones_position(reverse);
            List<Ones_entries> ones_entries = new List<Ones_entries>();
            int prev_position = ones_position[0];
            int seq_start_idx = ones_position[0];
            int seq_length = 0;

            for(int i = 0; i< ones_position.Count; i++)
            {
                if (ones_position[i] - prev_position <2)
                {
                    seq_length += 1;
                }
                else
                {
                    Ones_entries entry = new Ones_entries(seq_start_idx, seq_length, position_in_matrix);
                    ones_entries.Add(entry);
                    seq_length = 1;
                    seq_start_idx = ones_position[i];
                }
                prev_position = ones_position[i];
            }
            Ones_entries last_entry = new Ones_entries(seq_start_idx, seq_length, position_in_matrix);
            ones_entries.Add(last_entry);

            return ones_entries;
        }


        public List<int> Get_rev_row()
        {
            List<int> rev_row = new List<int>();
            for(int i = 0; i < row.Count; i++)
            {
                rev_row.Add(row[i]);
            }
            rev_row.Reverse();
            return rev_row;
        }

        bool check_entry_in_list(List<Entries_to_delete> entries,int start, int length)
        {
            bool check = false;
            for(int i = 0; i< entries.Count; i++)
            {
                if (entries[i].start == start && entries[i].length == length)
                {
                    check = true;
                }
            }
            return check;
        }

        public void Get_entries_to_delete()
        {
            List<Entries_to_delete> entry_to_delete = Column_to_delete(false);

            /*for (int i = 0; i < entry_to_delete.Count; i++)
            {
                Console.WriteLine(entry_to_delete[i].start + " " + entry_to_delete[i].length + " " + entry_to_delete[i].row_id);
            }*/
            


            //Console.WriteLine();
            rev_row = Get_rev_row();
            List<Entries_to_delete> rev_entry_to_delete = Column_to_delete(true);
            /*
            for (int i = 0; i < rev_entry_to_delete.Count; i++)
            {
                Console.WriteLine(rev_entry_to_delete[i].start + " " + rev_entry_to_delete[i].length + " " + rev_entry_to_delete[i].row_id);
            }*/

            //Console.WriteLine(row.Count);
            
            for(int i = 0; i < rev_entry_to_delete.Count; i ++)
            {
                int new_start;
                new_start = row.Count - (rev_entry_to_delete[i].start + rev_entry_to_delete[i].length);
                bool is_entry_in_list = check_entry_in_list(entry_to_delete, new_start, rev_entry_to_delete[i].length);
                if (is_entry_in_list == false)
                {
                    entry_to_delete.Add(new Entries_to_delete(new_start, rev_entry_to_delete[i].length, rev_entry_to_delete[i].row_id));

                }
            }


            /*Console.WriteLine();
            for (int i = 0; i < entry_to_delete.Count; i++)
            {
                Console.Write(entry_to_delete[i].start + " " + entry_to_delete[i].length + " " + entry_to_delete[i].row_id + " columns: " );
                for(int j=0;j<entry_to_delete[i].columns.Count; j++)
                {
                    Console.Write(entry_to_delete[i].columns[j] + " ");
                }Console.WriteLine();
            }*/


            /*
            for(int i=0;i<rev_row.Count; i++)
            {
                Console.Write(rev_row[i] + " ");
            }
            Console.WriteLine();

            for (int i = 0; i < row.Count; i++)
            {
                Console.Write(row[i] + " ");
            }
            Console.WriteLine();
            */

              
            number_of_results = Count_of_results(entry_to_delete);

            List<int> list_of_groups = new List<int>();

            for (int i = 0; i < entry_to_delete.Count; i++)
            {
                if (!list_of_groups.Contains(entry_to_delete[i].row_id))
                {
                    list_of_groups.Add(entry_to_delete[i].row_id);
                }

            }

            number_of_groups = list_of_groups.Count;
//            Console.WriteLine(number_of_results);

            entries_to_delete = entry_to_delete;

        }


        


        int Count_of_results(List<Entries_to_delete> entries) // dodaj check czy mogą stać obok siebie rozwiązania
        {

            List<int> list_of_groups = new List<int>();
            for(int i=0;i<entries.Count;i++)
            {
                if(! list_of_groups.Contains(entries[i].row_id))
                {
                    list_of_groups.Add(entries[i].row_id);
                }
               
            }

            int number_of_result= 1;
            for(int i = 0; i < list_of_groups.Count; i++)
            {
                int numer_of_results_in_group = 0;
                for(int j = 0; j < entries.Count; j++)
                {
                    if (entries[j].row_id == list_of_groups[i])
                        numer_of_results_in_group += 1;
                }
                number_of_result = number_of_result * numer_of_results_in_group;

            }

            return number_of_result;
             
        }

        public List<Entries_to_delete> Column_to_delete(bool reverse)
        {
            /*if (reverse)
            {
                for (int x = 0; x < row.Count; x++)
                {
                    Console.Write(reverse_row[x] + " ");
                }
                Console.WriteLine();
            }
            else
            {
                for (int x = 0; x < row.Count; x++)
                {
                    Console.Write(row[x] + " ");
                }
                Console.WriteLine();
            }*/
     
            List<Ones_entries> ones_entries = Get_ones_entries(reverse);
            //for(int i = 0; i < ones_entries.Count; i++)
            //{
              //  Console.Write(ones_entries[i].start + " " + ones_entries[i].length);
               // Console.WriteLine();
            //}

            int ones_entries_len = ones_entries.Count;
            int last_seq_start_idx = ones_entries_len - 1;

            int previous_zero_length = 0;
            int seq_len = ones_entries[0].length;
            int final_seq_start = ones_entries[0].start;
            List<Entries_to_delete> entry_to_delete = new List<Entries_to_delete>();

            for (int i = 0; i < ones_entries_len; i++)
            {
                if(i == last_seq_start_idx)
                {
                    break;
                }
                

                //Entries_to_delete entries;
                int seq_start = ones_entries[i].start;
                int seq_end = ones_entries[i].start + ones_entries[i].length - 1;

                int next_seq_start = ones_entries[i + 1].start;
                int next_seq_len = ones_entries[i + 1].length;
                int next_seq_end = next_seq_start + next_seq_len - 1;

                int zeros_length = next_seq_start - seq_end - 1 + previous_zero_length;

                if (seq_len >= zeros_length && next_seq_len >= zeros_length)
                {
                    //Console.WriteLine("tu1");
                    if(seq_len == zeros_length && zeros_length  == next_seq_len)
                    {
                        entry_to_delete.Add(new Entries_to_delete(seq_start, seq_start + seq_len, i));
                        entry_to_delete.Add(new Entries_to_delete(seq_end + 1, zeros_length, i));
                        entry_to_delete.Add(new Entries_to_delete(next_seq_start, next_seq_len, i));
                    }
                    else if(seq_len == zeros_length && zeros_length != next_seq_len)
                    {
                        entry_to_delete.Add(new Entries_to_delete(seq_start, seq_start + seq_len, i));
                        entry_to_delete.Add(new Entries_to_delete(seq_end + 1, zeros_length, i));
                    }
                    else if(seq_len != zeros_length && zeros_length == next_seq_len)
                    {
                        entry_to_delete.Add(new Entries_to_delete(seq_end + 1, zeros_length, i));
                        entry_to_delete.Add(new Entries_to_delete(next_seq_start, next_seq_len, i));
                    }
                    else
                    {
                        entry_to_delete.Add(new Entries_to_delete(seq_end + 1, zeros_length, i));
                    }
                    seq_len += next_seq_len;
                }

                if (next_seq_len >= seq_len && seq_len < zeros_length)
                {
                    //Console.WriteLine("tu2");
                    //Console.WriteLine("tu2 " + seq_len + ' ' + next_seq_len + ' ' + zeros_length);
                    if (seq_len == next_seq_len)
                    {
                        entry_to_delete.Add(new Entries_to_delete(seq_start, seq_len, i));
                        entry_to_delete.Add(new Entries_to_delete(next_seq_start, next_seq_len, i));
                    }
                    else if(i==0)
                    {
                        entry_to_delete.Add(new Entries_to_delete(seq_start, seq_len, i));
                        //entry_to_delete.Add(new Entries_to_delete(ones_entries[i-1].start, ones_entries[i-1].start + ones_entries[i-1].length, i));
                    }
                    else
                    {
                        entry_to_delete.Add(new Entries_to_delete(ones_entries[i - 1].start, ones_entries[i - 1].start + ones_entries[i - 1].length, i));
                    }
                    seq_len = next_seq_len;
                    previous_zero_length = 0;
                    final_seq_start = next_seq_start;
                }

                if(next_seq_len < zeros_length && seq_len > next_seq_len)
                {
 
                    //Console.WriteLine("tu3");
                    //Console.WriteLine(seq_start + " " + seq_len);
                    //Console.WriteLine(next_seq_len + " " + zeros_length + " " + seq_len);
                    //Console.WriteLine("i, last_seq_start_idx: " + i + ' ' + last_seq_start_idx);
                    if (i + 1 != last_seq_start_idx)
                    {
                        int next_zeros_seq_start = next_seq_end + 1;
                        int next_zeros_seq_len = (ones_entries[i + 2].start) - (next_seq_end + 1);
                        if(next_seq_len == next_zeros_seq_len)
                        {
                            entry_to_delete.Add(new Entries_to_delete(next_zeros_seq_start, next_zeros_seq_len, i));
                            //Console.WriteLine(next_zeros_seq_start + " " + next_zeros_seq_len);
                        }
                    }
                    entry_to_delete.Add(new Entries_to_delete(next_seq_start, next_seq_len, i));
                    //Console.WriteLine(next_seq_start + " " + next_seq_len);
                    previous_zero_length = zeros_length;
                }
             }

            
            //sprawdz odwrotny wiersz i zobacz czy nie ma mneiejszej ilosci kolumn do usuniecia
            //get_result_permutation_for_row(entry_to_delete);

            return entry_to_delete;
        }
        

        void get_result_permutation_for_row(List<Entries_to_delete> entries) //odwrotna lista nie wiem dlaczego + dodaj, że po id ma sprawdzać by dodawać do listy z każdej grupy id
        {
            List<Result_permutation_for_row> permutation = new List<Result_permutation_for_row>();
            for(int i = 0; i < entries.Count; i++)
            {
                Console.WriteLine(entries[i].row_id);
                if (entries[i].row_id == 0)
                {
                    permutation.Add(new Result_permutation_for_row(entries[i], entries[i].row_id));
                }
                else
                {

                    for(int j = 0; j < permutation.Count; j++)
                    {
                        if (entries[i].start != permutation[j].Get_end_of_last_element_in_list())
                        {
                            permutation[j].entries.Add(entries[i]);
                        }
                    }
                }
            }

            for(int i= 0; i < permutation.Count; i++)
            {
                for(int j = 0;j< permutation[i].entries.Count; j++)
                {
                    Console.WriteLine(permutation[i].entries[j].start + " " + permutation[i].entries[j].length);
                }
            }

        }


    }
}