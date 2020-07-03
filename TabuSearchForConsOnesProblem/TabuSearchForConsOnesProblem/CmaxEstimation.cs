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
        public List<int> columns_to_delete_in_matrix;

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

        List<int> Get_column_to_delete_from_one_row(Row row)
        {

            //Console.WriteLine("______________________________________________");
            List<Entries_to_delete> best_entry = new List<Entries_to_delete>();
            List<int> columns_to_delete = new List<int>();
            if (row.number_of_results == 1)
            {
                for (int x = 0; x < row.entries_to_delete.Count; x++)
                {
                    for (int y = 0; y < row.entries_to_delete[x].columns.Count; y++)
                    {
                        columns_to_delete.Add(row.entries_to_delete[x].columns[y]);
                        //Console.WriteLine("tu1: " + row.entries_to_delete[x].columns[y]);
                    }
                    if(! best_entry.Contains(row.entries_to_delete[x]))
                        best_entry.Add(row.entries_to_delete[x]);
                }
                
            }
            else
            {
                //Console.WriteLine("------------ columns in row -------------------");
                int number_of_groups = row.number_of_groups;
                for (int x = 0; x < number_of_groups; x++)
                {
                    List<Entries_to_delete> entry_to_del_by_len = new List<Entries_to_delete>();
                    for (int y = 0; y < row.entries_to_delete.Count; y++)
                    {
                        if (row.entries_to_delete[y].id == x)
                        {
                            entry_to_del_by_len.Add(row.entries_to_delete[y]);
                            //for(int a = 0; a< row.entries_to_delete[y].columns.Count;a++)
                              //  Console.Write(row.entries_to_delete[y].columns[a] + " ");
                        }//Console.WriteLine();
                        entry_to_del_by_len = entry_to_del_by_len.OrderBy(o => o.length).ToList();
                    }

                    for (int i = 0; i < entry_to_del_by_len.Count; i++)
                    {
                        Entries_to_delete candidate_to_del = entry_to_del_by_len[i];

                        if (best_entry.Count != 0)
                        {
                            int end = best_entry[best_entry.Count - 1].start + best_entry[best_entry.Count - 1].length;
                        }

                        if (best_entry.Count == 0) 
                        {
                            for (int j = 0; j < candidate_to_del.columns.Count; j++)
                            {

                                columns_to_delete.Add(candidate_to_del.columns[j]);
                                //Console.WriteLine("tu2: " + candidate_to_del.columns[j]);
                                
                            }
                            best_entry.Add(candidate_to_del);
                        i = entry_to_del_by_len.Count;
                        }


                        else
                        {
                            if(candidate_to_del.start != best_entry[best_entry.Count - 1].start + best_entry[best_entry.Count - 1].length)
                            {
                                if (!(candidate_to_del.start <= best_entry[best_entry.Count - 1].start && candidate_to_del.start + candidate_to_del.length >= best_entry[best_entry.Count - 1].start))
                                {

                                    for (int j = 0; j < candidate_to_del.columns.Count; j++)
                                    {

                                        columns_to_delete.Add(candidate_to_del.columns[j]);
                                        //Console.WriteLine("tu3: " + candidate_to_del.columns[j]);
                                        /*if(candidate_to_del.columns[j]  < 0)
                                        {
                                            Console.WriteLine("candidate to del columns: ");
                                            for(int a = 0; a<candidate_to_del.columns.Count;a++)
                                            {
                                                Console.Write(candidate_to_del.columns[a] + " ");
                                            }Console.WriteLine();
                                        }*/

                                    }
                                    best_entry.Add(candidate_to_del);
                                    i = entry_to_del_by_len.Count;
                                }
                            }

                        }
                        

                    }

                }

            }

           // for(int i = 0; i < columns_to_delete.Count; i++)
            //{
              //  Console.Write(columns_to_delete[i] + " ");
            //}Console.WriteLine();

            List<int> columns_to_delete_from_matrix = row.Get_columns_to_delete_from_matrix(columns_to_delete);

            return columns_to_delete_from_matrix;

        }


       List<int> Get_columns_to_delete(List<Row> rows_object)
        {
            List<int> columns_to_delete = new List<int>();

            while (rows_object.Count != 0)
            {
                for (int i = 0; i < rows_object.Count; i++) //dla kazdego wiersza oblicz entries do usuniecia
                {
                    rows_object[i].Get_entries_to_delete();
                }

                List<Row> rows_sorted_by_results = rows_object.OrderBy(o => o.number_of_results).ToList(); //posortuj wyniki po ilosci mozliwych wynikow
                Row current_row = rows_sorted_by_results[0]; //wybierz pierwszy wynik

                current_row.Change_row(columns_to_delete); //usuwamy usuniete juz kolumny z wiersza
                current_row.Get_entries_to_delete(); //teraz obliczamy entries

                List<int> columns_to_delete_from_row = Get_column_to_delete_from_one_row(current_row); //dodajemy kolumny do usuniecia

                for (int x = 0; x < columns_to_delete_from_row.Count; x++)
                {
                    columns_to_delete.Add(columns_to_delete_from_row[x]);
                }
                int current_row_id = current_row.position_in_matrix;
                int rows_object_size = rows_object.Count;
              
                for (int i = 0; i < rows_object_size; i++)
                {
                    if(rows_object[i].position_in_matrix == current_row_id)
                    {
                        rows_object.Remove(rows_object[i]);
                        break;
                    }
                }
            }

           
            columns_to_delete.Sort();
            columns_to_delete_in_matrix = columns_to_delete;

            return columns_to_delete;
        }

        bool IsMatrixConsecutiveOnes(int[,] matrix)
        {
            int matrixWidth = matrix.GetLength(1);
            int matrixHeight = matrix.GetLength(0);
            for (int i = 0; i < matrixHeight; i++)
            {
                bool foundOne = false;
                bool foundZeroAfterOne = false;

                for (int j = 0; j < matrixWidth; j++)
                {
                    if (!foundOne && matrix[i, j] == 1)
                    {
                        foundOne = true;
                    }
                    if (foundOne && matrix[i, j] == 0)
                    {
                        foundZeroAfterOne = true;
                    }
                    if (foundZeroAfterOne && matrix[i, j] == 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static int[,] TrimArray(int rowToRemove, int columnToRemove, int[,] originalArray)
        {
            int[,] result = new int[originalArray.GetLength(0) - 1, originalArray.GetLength(1) - 1];

            for (int i = 0, j = 0; i < originalArray.GetLength(0); i++)
            {
                if (i == rowToRemove)
                    continue;

                for (int k = 0, u = 0; k < originalArray.GetLength(1); k++)
                {
                    if (k == columnToRemove)
                        continue;

                    result[j, u] = originalArray[i, k];
                    u++;
                }
                j++;
            }

            return result;
        }


        int[,] get_cleaned_matrix(List<int>columns, int [,] matrix)
        {
            for(int i=0;i<columns.Count;i++)
            {
                matrix = TrimArray(0, columns[i], matrix);
                Console.WriteLine(matrix.GetLength(1));
            }

            return matrix;
        }


        public int GetCmaxValue()
        {
            int matrixHeight = matrix.GetLength(0);
            int matrixWidth = matrix.GetLength(1);
            List<Row> rows_object = new List<Row>();
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

            List<int> columns_to_delete = Get_columns_to_delete(rows_object);


            return columns_to_delete.Count;


        }

    }
}
