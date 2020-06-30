using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabuSearchForConsOnesProblem
{
    class TabuSearch
    {
        public int[,] matrix;
        public int[,] greedyHeuristicsMatrix;


        public TabuSearch(int[,] inputMatrix)
        {
            matrix = inputMatrix;
            TabuSearchAlgorythm();
        }


        public int[,] OutputMatrix()
        {
            return matrix;
        }


        public void ShowMatrix(int[,] matrix)
        {
            int matrixHeight = matrix.GetLength(0);
            int matrixWidth = matrix.GetLength(1);
            for (int i = 0; i < matrixHeight; i++)
            {
                for (int j = 0; j < matrixWidth; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

       
        int[,] Get_random_matrix()
        {
            Random r = new Random();
            int randomHeight = r.Next(20);
            int randomWidth = r.Next(20);
            int[,] matrix = new int[randomHeight, randomWidth];
            Random x = new Random();

            for (int i=0;i<randomHeight;i++)
            {
                for(int j=0;j<randomWidth-1;j++)
                {
                    int randomValue = x.Next();
                    int value = 0;
                    if(randomValue%2 == 0)
                    {
                        value = 1;
                    }
                    matrix[i, j] = value;
                }
                matrix[i, randomWidth - 1] = 1;
            }
            
           
            return matrix;
        }



        bool IsMoveOnTabuList(List<MovedColumnAndNeighborhood> TabuList, CurrentMatrix currentMatrix, int column, int randomPosition, int randomColumnIndex)
        {
            List<int> tmpindexList = currentMatrix.columnsOrder;
            List<int> indexList = new List<int>();
            for(int i = 0; i< tmpindexList.Count; i++)
            {
                indexList.Add(tmpindexList[i]);
            }

            //wykonujemy zmiane
            indexList.RemoveAt(randomColumnIndex);
            indexList.Insert(randomPosition, column);
   

            //sprawdzamy sąsiadow
            int leftNeighborhoodIndex = 0;
            int rightNeighborhoodIndex = 0;
            int neighborhood = 0;
            if (randomPosition == 0)
            {
                neighborhood = indexList[randomPosition + 1];
            }
            if (randomPosition == indexList.Count - 1)
            {
                neighborhood = indexList[randomPosition - 1];
            }
            else if (randomPosition != 0 && randomPosition != indexList.Count)
            {
                leftNeighborhoodIndex = indexList[randomPosition - 1];
                rightNeighborhoodIndex = indexList[randomPosition + 1];
            }

            //sprawdzamy czy mamy taki ruch w liście tabu

            for (int i = 0; i < TabuList.Count; i++)
            {
                if ((TabuList[i].column == column && TabuList[i].neighborhood == neighborhood) || (TabuList[i].column == column && TabuList[i].left_neighborhood == leftNeighborhoodIndex && TabuList[i].right_neighborhood == rightNeighborhoodIndex))
                {
                    return true;
                }
            }

            return false;

        }

        CurrentMatrix ChangeInMatrix( CurrentMatrix currentMatrix, List<MovedColumnAndNeighborhood> TabuList)
        {
            //int[,] matrix = currentMatrix.newMatrix;
            List<int> indexList = currentMatrix.columnsOrder;

            Random r = new Random(); //losujemy zmiane
            int matrixWidth = matrix.GetLength(1);
            int matrixHeight = matrix.GetLength(0);
            int randomColumnIndex = r.Next(matrixWidth);
            int randomPosition = r.Next(matrixWidth);
            int column = indexList[randomColumnIndex];
            var changedMatrix = new int[matrixHeight, matrixWidth];

           // Console.WriteLine(randomColumnIndex + " " + randomPosition + " " + column);
            //sprawdzamy czy taka zmiana jest w liście tabu - musimy znalezc lewego i prawego sąsiada naszego rozwiązania

            if (IsMoveOnTabuList(TabuList, currentMatrix, column, randomPosition, randomColumnIndex) || randomColumnIndex == randomPosition)
            {
                randomColumnIndex = r.Next(matrixWidth);
                randomPosition = r.Next(matrixWidth);
                column = indexList[randomColumnIndex];
            }

            //wykonujemy zmiane
            indexList.RemoveAt(randomColumnIndex);
            /*Console.WriteLine("After remove: ");
            for (int i = 0; i < indexList.Count; i++)
            {
                Console.Write(indexList[i] + " ");
            }

            Console.WriteLine();
            */

            //Console.WriteLine("after insert: ");
            indexList.Insert(randomPosition, column);
            /*for (int i = 0; i < indexList.Count; i++)
            {
                Console.Write(indexList[i] + " ");
            }
            Console.WriteLine();
            */
            //sprawdzamy sąsiadow
            int leftNeighborhoodIndex = 0;
            int rightNeighborhoodIndex = 0;
            int neighborhood = 0;
            if (randomPosition == 0)
            {
                neighborhood = indexList[randomPosition + 1];
            }
            if (randomPosition == indexList.Count - 1)
            {
                neighborhood = indexList[randomPosition - 1];
            }
            else if (randomPosition != 0 && randomPosition != indexList.Count)
            {
                leftNeighborhoodIndex = indexList[randomPosition - 1];
                rightNeighborhoodIndex = indexList[randomPosition + 1];
            }

            

           // Console.WriteLine("Candidate info: ");
            //Console.WriteLine("Column: " + column + " neigh: " + neighborhood + " left: " + leftNeighborhoodIndex + " right: " + rightNeighborhoodIndex);
               
            if(leftNeighborhoodIndex !=0 && rightNeighborhoodIndex !=0)
            {
                MovedColumnAndNeighborhood columnAndNeighborhood = new MovedColumnAndNeighborhood(column, leftNeighborhoodIndex, rightNeighborhoodIndex);
                TabuList.Add(columnAndNeighborhood);
            }
            else
            {
                MovedColumnAndNeighborhood columnAndNeighborhood = new MovedColumnAndNeighborhood(column, neighborhood);
                TabuList.Add(columnAndNeighborhood);
            }

           /* Console.WriteLine("Tabu list: ");
            for (int i = 0; i < TabuList.Count; i++)
            {
                Console.WriteLine("Column in tabu: " + TabuList[i].column + " left: " + TabuList[i].left_neighborhood + " right: " + TabuList[i].right_neighborhood + " neigh" + TabuList[i].neighborhood);
            }
            */
            for (int i = 0; i < matrixHeight; i++)
            {
                for (int j = 0; j < matrixWidth; j++)
                {
                    changedMatrix[i, j] = matrix[i, indexList[j]];
                }
            }
            //Console.WriteLine("new matrix:");
            //ShowMatrix(changedMatrix);
            CurrentMatrix newMatrix = new CurrentMatrix(changedMatrix, indexList);

            return newMatrix;
        }


        bool IsMovementInList(List<DiversifyingMovements> movements, int column, int randomPosition)
        {
            for (int i = 0; i < movements.Count; i++)
            {
                if (movements[i].column == column && movements[i].newPosition == randomPosition)
                {
                    return true;
                }
            }

            return false;
        }


        CurrentMatrix GenerateDiversifyingMovements(CurrentMatrix currentMatrix, int numberOfMovements)
        {
            int[,] resultMatrix = currentMatrix.newMatrix;
            List<int> indexList = currentMatrix.columnsOrder;

            Random r = new Random(); //losujemy zmiane
            int matrixWidth = matrix.GetLength(1);
            int matrixHeight = matrix.GetLength(0);
            var changedMatrix = new int[matrixHeight, matrixWidth];

            int numberOfMovementsCount = 0;
            List<DiversifyingMovements> movements = new List<DiversifyingMovements>();

            while(numberOfMovements != numberOfMovementsCount)
            {
                int randomColumnIndex = r.Next(matrixWidth);
                int randomPosition = r.Next(matrixWidth);
                int column = indexList[randomColumnIndex];

                if (! IsMovementInList(movements, column, randomPosition))
                {
                    DiversifyingMovements movement = new DiversifyingMovements(column, randomPosition);
                    movements.Add(movement);

                    indexList.RemoveAt(randomColumnIndex);
                    indexList.Insert(randomPosition, column);
                    numberOfMovementsCount += 1;
                }
            }

            for (int i = 0; i < matrixHeight; i++)
            {
                for (int j = 0; j < matrixWidth; j++)
                {
                    changedMatrix[i, j] = matrix[i, indexList[j]];
                }
            }
            CurrentMatrix newMatrix = new CurrentMatrix(changedMatrix, indexList);

            return newMatrix;
        }


        void ShowResultMatrix(int[,] matrix, List<int> columns_to_delete )
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (!columns_to_delete.Contains(j))
                    {
                        Console.Write(matrix[i, j] + " ");
                    }
                }
                Console.WriteLine();
            }
        }

        public void TabuSearchAlgorythm()
        {
            
            //macierz wejsciowa
            //greedy heuristics + cmax obliczamy i to najlepszy cmax do tej pory - current result(od 0 do count) - kolejnosc kolumn i cmax
            //robimy drobną zmianę w macierzy - zapisujemy ruch, losujemy kolumnę i miejsce do włożenia - zapamiętujemy ten ruch - obiekt ruch(pozycja kolumny, miejsce wstawienia, kolejnosc kolumn po ruchu)
            //-jesli ruch na liscie tabu to sprawdzamy czy kolejnosc kolumn po ruchu inna - jesli tak to robimy ruch
            //dodajemy ruch do listy tabu 
            //obliczamy cmax
            //jesli cmax jest lepszy od poprzedniego to dodajemy current result - kolejnosc kolumn i cmax
            //jeśli po 60%*ilosc kolumn ruchach nie polepsza sie wynik to losujemy nowy greedy heuristics
            //jeśli po 50%ilosc kolumn losowaniach greedy nie polepszył sie wynik to dajemy wynik 


            //30% ruchy dywersyfikujące



            Console.WriteLine("Input matrix:");
            ShowMatrix(matrix);
            GreedyHeuristic initialMatrix = new GreedyHeuristic(matrix);
            greedyHeuristicsMatrix = initialMatrix.GreedyHeuristicAlgorythm();

            //Console.WriteLine("Greedy heuristics matrix:");
            //ShowMatrix(greedyHeuristicsMatrix);

            CmaxEstimation cmaxEstimator = new CmaxEstimation(greedyHeuristicsMatrix);
            int cmaxValue = cmaxEstimator.GetCmaxValue();
            Console.WriteLine("Cmax value: ");
            Console.WriteLine(cmaxValue);

            List<int> greedyHeuristicsMatrixsOrder = initialMatrix.columnsOrderInOryginalMatrix;
            CurrentResult result = new CurrentResult(cmaxValue, greedyHeuristicsMatrixsOrder, greedyHeuristicsMatrix, cmaxEstimator.columns_to_delete_in_matrix);

            //Console.WriteLine("Columns order: ");
            //for(int i = 0; i< greedyHeuristicsMatrixsOrder.Count; i++)
            //{
              //  Console.Write(greedyHeuristicsMatrixsOrder[i] + " ");
            //}Console.WriteLine();
       

            CurrentMatrix currentMatrix = new CurrentMatrix(greedyHeuristicsMatrix, greedyHeuristicsMatrixsOrder); //macierz wejściowa
            List<MovedColumnAndNeighborhood> TabuList = new List<MovedColumnAndNeighborhood>(); //inicjacja listy tabu

            int maxStepWithoutBetterResult = (int)(0.8 * matrix.GetLength(1)); //80% * liczba kolumn
            int stepWithoutBetterResultCount = 0;
            int GenerateDiversifyingMovementsCount = (int)(0.5 * matrix.GetLength(1)); ;



            Console.WriteLine("_____________________________________");


            while (GenerateDiversifyingMovementsCount != 0)
            {
                //Console.WriteLine(GenerateDiversifyingMovementsCount + " " + stepWithoutBetterResultCount);
                int tabuListMaxSize = (int) (0.3 * matrix.GetLength(1));
                currentMatrix = ChangeInMatrix(currentMatrix, TabuList); // robimy ruch
                cmaxEstimator = new CmaxEstimation(currentMatrix.newMatrix); //obliczamy cmax
                cmaxValue = cmaxEstimator.GetCmaxValue();

                if (result.cmax > cmaxValue)
                {
                    result = new CurrentResult(cmaxValue, currentMatrix.columnsOrder, currentMatrix.newMatrix, cmaxEstimator.columns_to_delete_in_matrix);
                }
                else
                {
                    currentMatrix = new CurrentMatrix(result.matrix, result.columnsOrder);
                }

                if (TabuList.Count > tabuListMaxSize) //update tabu list
                {
                    TabuList.Remove(TabuList[0]);
                } 

                if(result.cmax <= cmaxValue)
                {
                    stepWithoutBetterResultCount += 1; // gdy wynik nie polepsza sie bez przerwy
                }
                else
                {
                    stepWithoutBetterResultCount = 0;
                }

                if(stepWithoutBetterResultCount == maxStepWithoutBetterResult) //jesli nie ma poprawy
                {
                    int numberOfColumnToChange = (int)(0.2 * matrix.GetLength(1));
                    currentMatrix = GenerateDiversifyingMovements(currentMatrix, numberOfColumnToChange); //robimy nowe rozwiazanie poczatkowe 
                    GenerateDiversifyingMovementsCount -= 1;
                    stepWithoutBetterResultCount = 0;

                    cmaxEstimator = new CmaxEstimation(currentMatrix.newMatrix);
                    cmaxValue = cmaxEstimator.GetCmaxValue();
                    if (result.cmax > cmaxValue)
                    {
                        result = new CurrentResult(cmaxValue, currentMatrix.columnsOrder, currentMatrix.newMatrix, cmaxEstimator.columns_to_delete_in_matrix);
                    }
                    else
                    {
                        currentMatrix = new CurrentMatrix(result.matrix, result.columnsOrder);
                    }
                }
                
            }

            Console.WriteLine("Cmax: ");
            Console.WriteLine(result.cmax);
            Console.WriteLine("Input Matrix: ");
            ShowMatrix(result.matrix);
            Console.WriteLine("Result Matrix: ");
            ShowResultMatrix(result.matrix, result.columnToDelete);
            


            

        }
    }
}
