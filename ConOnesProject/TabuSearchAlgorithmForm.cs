using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ConOnesProject
{
    public partial class TabuSearchAlgorithmForm : Form
    {
        int[,] matrix;
        int[,] greedyHeuristicsMatrix;
        bool stopTabuAlgorithm = false;
        int diversifyMatrixCount;
        int neighborhoodsCount;
        int theSameResultCount;
        int tabuListSize;
        
        public int cmax;
        public int[,] resultMatrix;
        public int[,] orderedColumnsMatrix;
        public List<int> columnsOrder;
        public List<int> columnsToDelete;



        private IFormsMatrixInformations menuForm;
        public TabuSearchAlgorithmForm(IFormsMatrixInformations callingForm, int [,] in_matrix)
        {
            menuForm = callingForm;
            matrix = in_matrix;
            InitializeComponent();
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


        bool IsMoveOnTabuList(List<MovedColumnAndNeighborhood> TabuList, CurrentMatrix currentMatrix, int column, int randomPosition, int randomColumnIndex)
        {
            List<int> tmpindexList = currentMatrix.columnsOrder;
            List<int> indexList = new List<int>();
            for (int i = 0; i < tmpindexList.Count; i++)
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

        CurrentMatrix ChangeInMatrix(CurrentMatrix currentMatrix, List<MovedColumnAndNeighborhood> TabuList)
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

            if (leftNeighborhoodIndex != 0 && rightNeighborhoodIndex != 0)
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

            while (numberOfMovements != numberOfMovementsCount)
            {
                int randomColumnIndex = r.Next(matrixWidth);
                int randomPosition = r.Next(matrixWidth);
                int column = indexList[randomColumnIndex];

                if (!IsMovementInList(movements, column, randomPosition))
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


        void ShowResultMatrix(int[,] matrix, List<int> columns_to_delete)
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


        void runTabuSearchAlgorithm()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var elapsedMs = watch.ElapsedMilliseconds;
            string seconds = "";
            //Console.WriteLine("Input matrix:");
            //ShowMatrix(matrix);
            GreedyHeuristic initialMatrix = new GreedyHeuristic(matrix);
            greedyHeuristicsMatrix = initialMatrix.GreedyHeuristicAlgorythm();

            //Console.WriteLine("Greedy heuristics matrix:");
            //ShowMatrix(greedyHeuristicsMatrix);

            CmaxEstimation cmaxEstimator = new CmaxEstimation(greedyHeuristicsMatrix);
            int cmaxValue = cmaxEstimator.GetCmaxValue();
            //Console.WriteLine("Cmax value: ");
            //Console.WriteLine(cmaxValue);

            List<int> greedyHeuristicsMatrixsOrder = initialMatrix.columnsOrderInOryginalMatrix;
            CurrentResult result = new CurrentResult(cmaxValue, greedyHeuristicsMatrixsOrder, greedyHeuristicsMatrix, cmaxEstimator.columns_to_delete_in_matrix);

            CurrentMatrix currentMatrix = new CurrentMatrix(greedyHeuristicsMatrix, greedyHeuristicsMatrixsOrder); //macierz wejściowa
            List<MovedColumnAndNeighborhood> TabuList = new List<MovedColumnAndNeighborhood>(); //inicjacja listy tabu

            int maxStepWithoutBetterResult = theSameResultCount;
            int stepWithoutBetterResultCount = 0;
            int generateDiversifyingMovementsCount = diversifyMatrixCount;
            int NeighbornHoodsCountToVisit = neighborhoodsCount;

            int progresBarMaximumSize = generateDiversifyingMovementsCount;
            progressBar1.Maximum = progresBarMaximumSize;
            progressBar1.Value = 0;

            while (generateDiversifyingMovementsCount != 0)
            {
                textBox1.Text = result.cmax.ToString();
                progressBar1.Value = progresBarMaximumSize - generateDiversifyingMovementsCount + 1;

                if (stopTabuAlgorithm)
                {
                    watch.Stop();
                    elapsedMs = watch.ElapsedMilliseconds;
                    if (elapsedMs < 1000)
                        seconds = elapsedMs.ToString() + "ms";
                    else
                        seconds = (elapsedMs / 1000).ToString() + "s";
                    cmax = result.cmax;
                    resultMatrix = result.matrix;
                    columnsOrder = result.columnsOrder;
                    columnsToDelete = result.columnToDelete;
                    menuForm.resultMatrix(resultMatrix, cmax, columnsOrder, columnsToDelete, diversifyMatrixCount, neighborhoodsCount, tabuListSize, theSameResultCount, seconds);
                    this.Close();

                    Thread.CurrentThread.Abort();
                }

                int tabuListMaxSize = tabuListSize;
                currentMatrix = ChangeInMatrix(currentMatrix, TabuList); // robimy ruch
                NeighbornHoodsCountToVisit -= 1;
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

                if (result.cmax <= cmaxValue)
                {
                    stepWithoutBetterResultCount += 1; // gdy wynik nie polepsza sie bez przerwy
                }
                else
                {
                    stepWithoutBetterResultCount = 0;
                }

                if (stepWithoutBetterResultCount == maxStepWithoutBetterResult || NeighbornHoodsCountToVisit == 0) //jesli nie ma poprawy
                {
                    int numberOfColumnToChange = (int)(0.2 * matrix.GetLength(1));
                    currentMatrix = GenerateDiversifyingMovements(currentMatrix, numberOfColumnToChange); //robimy nowe rozwiazanie poczatkowe 
                    NeighbornHoodsCountToVisit = neighborhoodsCount;
                    generateDiversifyingMovementsCount -= 1;
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

            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;

            if (elapsedMs < 1000)
                seconds = elapsedMs.ToString() + "ms";
            else
                seconds = (elapsedMs / 1000).ToString() + "s";

            cmax = result.cmax;
            resultMatrix = result.matrix;
            columnsOrder = result.columnsOrder;
            columnsToDelete = result.columnToDelete;
            menuForm.resultMatrix(resultMatrix, cmax, columnsOrder, columnsToDelete, diversifyMatrixCount, neighborhoodsCount, tabuListSize, theSameResultCount,seconds);

            Thread.Sleep(900);

            this.Close();

            /*
            for (int i = 0; i < result.columnsOrder.Count; i++)
            {
                Console.Write(result.columnsOrder[i] + " ");
            }
            Console.WriteLine();
            */
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            diversifyMatrixCount = Int32.Parse(textBox2.Text);
            neighborhoodsCount = Int32.Parse(textBox3.Text);
            theSameResultCount = Int32.Parse(textBox4.Text);
            tabuListSize = Int32.Parse(textBox5.Text);

            CheckForIllegalCrossThreadCalls = false;
            stopTabuAlgorithm = false;
            Thread t = new Thread(runTabuSearchAlgorithm);
            t.Name = "TabuSearchAlgorithm";
            t.Start();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            stopTabuAlgorithm = true;
        }

    }
}