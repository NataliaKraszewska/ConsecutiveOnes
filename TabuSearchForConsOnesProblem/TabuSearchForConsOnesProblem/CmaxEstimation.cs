using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabuSearchForConsOnesProblem
{
    class CmaxEstimation
    {
        int[,] matrix;

        public CmaxEstimation(int[,] inputMatrix)
        {
            matrix = inputMatrix;
        }


        bool IsRowConsecutiveOnes(List<int> row)
        {
            bool foundOne = false;
            bool foundZeroAfterOne = false;

            for (int i = 0; i < row.Count; i++)
            {
               if (!foundOne && row[i] == 1)
                {
                    foundOne = true;
                }
                if (foundOne && row[i] == 0)
                {
                    foundZeroAfterOne = true;
                }
                if (foundZeroAfterOne && row[i] == 1)
                {
                    return false;
                }
            }
            return true;
        }

        /*        
        List <ConsString> getListOfConesStrings(List <int> row)
        {
            List <ConsString> consStrings = new List<ConsString>();
            for (int i = 0; i < row.Count - 1; i++)
            {
                if (row[i] == row[i + 1])
                {

                }




            }
            return consStrings;
        }
        */

        public int GetCmaxValue()
        {
            int matrixHeight = matrix.GetLength(0);
            int matrixWidth = matrix.GetLength(1);
            List<Row> rows_object = new List<Row>();
            /*
            List<int> row = new List<int>();
            for (int i = 0; i < matrixWidth; i++)
            {
                Console.Write(matrix[0, i] + " ");
                row.Add(matrix[0, i]);
            }
            */
            for (int i = 0; i < matrixHeight; i++)
            {
                List<int> row = new List<int>();
                for (int j = 0; j < matrixWidth; j++)
                {
                    row.Add(matrix[i, j]);
                }

                Row current_row = new Row(row, i);
                rows_object.Add(current_row);
                current_row = null;
            }


            //rows_object[0].Column_to_delete();
            //rows_object[1].Column_to_delete();
            //rows_object[0].Get_entries_to_delete();

            for(int i = 0; i< rows_object.Count; i++)
            {
                rows_object[i].Get_entries_to_delete();
            }

            List<Row> rows_sorted_by_results = rows_object.OrderBy(o => o.number_of_results).ToList();

           /*for(int i = 0; i< rows_sorted_by_results.Count; i++)
            {
                if (rows_sorted_by_results[i].number_of_results != 1)
                {
                    for(int j = 0;j < rows_sorted_by_results[i].entries_to_delete.Count; j++)
                    {
                        Console.WriteLine("start: " + rows_sorted_by_results[i].entries_to_delete[j].start + " len: " + rows_sorted_by_results[i].entries_to_delete[j].length + " group: " + rows_sorted_by_results[i].entries_to_delete[j].row_id);
                    }
                }


            }*/

            List<int> columns_to_delete = new List<int>();

            for (int i = 0; i < rows_sorted_by_results.Count; i++)
            {
                //Console.WriteLine(rows_sorted_by_results[i].number_of_results);

                if(rows_sorted_by_results[i].number_of_results == 1)
                {
                    for(int x = 0; x < rows_sorted_by_results[i].entries_to_delete.Count; x++)
                    {
                        for(int y = 0; y<rows_sorted_by_results[i].entries_to_delete[x].columns.Count; y++)
                        {
                            if(! columns_to_delete.Contains(rows_sorted_by_results[i].entries_to_delete[x].columns[y]))
                                columns_to_delete.Add(rows_sorted_by_results[i].entries_to_delete[x].columns[y]);
                        }
                    }
                }
                else
                {
                    List<Entries_to_delete> best_entry = new List<Entries_to_delete>();
                    int number_of_groups = rows_sorted_by_results[i].number_of_groups;
                    for(int x = 0; x < number_of_groups; x++)
                    {

                        for(int y = 0; y < rows_sorted_by_results[i].entries_to_delete.Count; y++)
                        {
                            int best_score = 0;

                            if (rows_sorted_by_results[i].entries_to_delete[y].row_id == x)
                            {
                                if (x == 0)
                                {
                                    int entry_size = rows_sorted_by_results[i].entries_to_delete[y].columns.Count;
                                    for (int col_num = 0; col_num < entry_size; col_num++)
                                    {
                                        if(columns_to_delete.Contains(rows_sorted_by_results[i].entries_to_delete[y].columns[col_num]))
                                        {
                                            entry_size -= 1;
                                        }
                                    }
                                   
                                    if(best_score == 0 || best_score > entry_size)
                                    {
                                        best_score = entry_size;
                                        best_entry.Add(rows_sorted_by_results[i].entries_to_delete[y]);

                                        for (int j = 0; j < rows_sorted_by_results[i].entries_to_delete[y].columns.Count; j++)
                                        {
                                            if (!columns_to_delete.Contains(rows_sorted_by_results[i].entries_to_delete[y].columns[j]))
                                                columns_to_delete.Add(rows_sorted_by_results[i].entries_to_delete[y].columns[j]);

                                        }
                                        //Console.WriteLine("tu1 "  + best_score + ' ' + y +' '+ i);
                                        y = rows_sorted_by_results[i].entries_to_delete.Count;
                                    }
                                }
                                else
                                {
                                    //Console.WriteLine(best_entry[best_entry.Count - 1].start);
                                    if(rows_sorted_by_results[i].entries_to_delete[y].start != best_entry[best_entry.Count -1].length + best_entry[best_entry.Count - 1].start)
                                    {
                                        int entry_size = rows_sorted_by_results[i].entries_to_delete[y].columns.Count;
                                        for (int col_num = 0; col_num < entry_size; col_num++)
                                        {
                                            if (columns_to_delete.Contains(rows_sorted_by_results[i].entries_to_delete[y].columns[col_num]))
                                            {
                                                entry_size -= 1;
                                            }
                                        }

                                        if (best_score == 0 || best_score > entry_size)
                                        {
                                            best_score = entry_size;
                                            best_entry.Add(rows_sorted_by_results[i].entries_to_delete[y]);

                                            for (int j = 0; j < rows_sorted_by_results[i].entries_to_delete[y].columns.Count; j++)
                                            {
                                                if (!columns_to_delete.Contains(rows_sorted_by_results[i].entries_to_delete[y].columns[j]))
                                                    columns_to_delete.Add(rows_sorted_by_results[i].entries_to_delete[y].columns[j]);

                                            }
                                            //Console.WriteLine("tu2 " + best_score + ' ' + y + ' ' + i);
                                            y = rows_sorted_by_results[i].entries_to_delete.Count;
                                        }
                                    }
                                }
                                                              

                            }
                        }
                    }
                }

            }

            columns_to_delete.Sort();
            Console.Write("Columns to delete: ");
            for(int i =0;i<columns_to_delete.Count; i++)
            {
                Console.Write(columns_to_delete[i] + " ");
            }Console.WriteLine();

            Console.WriteLine("Cmax = " + columns_to_delete.Count);

            return 0;
        }

    }
}
