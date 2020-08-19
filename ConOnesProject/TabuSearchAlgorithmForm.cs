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
        int theSameResultCount;
        int tabuListSize;
        int newResultsCountVal;
        int divStepsVal;
        int percentageOfDivSteps;
        int percentageOfTabuList;
        int percentageOfTheSameResCount;
        Random r = new Random();
        public int cmax;
        public int[,] resultMatrix;
        public int[,] orderedColumnsMatrix;
        public List<int> columnsOrder;
        public List<int> columnsToDelete;
        private IFormsMatrixInformations menuForm;

        public TabuSearchAlgorithmForm(IFormsMatrixInformations callingForm, int[,] in_matrix)
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

            indexList.RemoveAt(randomColumnIndex);
            indexList.Insert(randomPosition, column);

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

            for (int i = 0; i < TabuList.Count; i++)
            {
                if ((TabuList[i].column == column && TabuList[i].neighborhood == neighborhood && TabuList[i].left_neighborhood == leftNeighborhoodIndex && TabuList[i].right_neighborhood == rightNeighborhoodIndex))
                {
                    return true;
                }
            }

            return false;
        }


        bool MoveGenerateBetterResult(List<MovedColumnAndNeighborhood> TabuList, CurrentMatrix currentMatrix, int column, int randomPosition, int randomColumnIndex)
        {
            CmaxEstimation cmaxEstimator1 = new CmaxEstimation(currentMatrix.newMatrix);
            int currentCmaxValue = cmaxEstimator1.GetCmaxValue();
            List<int> tmp = new List<int>();
            int matrixWidth = matrix.GetLength(1);
            int matrixHeight = matrix.GetLength(0);
            var changedMatrix = new int[matrixHeight, matrixWidth];

            for (int i = 0; i < currentMatrix.columnsOrder.Count; i++)
                tmp.Add(currentMatrix.columnsOrder[i]);

            tmp.RemoveAt(randomColumnIndex);
            tmp.Insert(randomPosition, column);

            for (int i = 0; i < matrixHeight; i++)
            {
                for (int j = 0; j < matrixWidth; j++)
                {
                    changedMatrix[i, j] = matrix[i, tmp[j]];
                }
            }

            CmaxEstimation cmaxEstimator2 = new CmaxEstimation(changedMatrix);
            int newCmaxValue = cmaxEstimator2.GetCmaxValue();
            if (newCmaxValue > currentCmaxValue)
                return true;

            return false;
        }


        MoveInTabuList GetMove(CurrentMatrix currentMatrix, List<MovedColumnAndNeighborhood> TabuList, int maxValue)
        {
            List<int> indexList = currentMatrix.columnsOrder;
            int randomColumnIndex = r.Next(maxValue);
            int randomPosition = r.Next(maxValue);
            int column = indexList[randomColumnIndex];
            bool foundMove = false;

            while (!foundMove)
            {
                if (!IsMoveOnTabuList(TabuList, currentMatrix, column, randomPosition, randomColumnIndex) && !(randomColumnIndex == randomPosition))
                {
                    foundMove = true;
                }
                else if (MoveGenerateBetterResult(TabuList, currentMatrix, column, randomPosition, randomColumnIndex))
                {
                    foundMove = true;
                }
                else
                {
                    randomColumnIndex = r.Next(maxValue);
                    randomPosition = r.Next(maxValue);
                    column = indexList[randomColumnIndex];
                }
            }

            return (new MoveInTabuList(randomColumnIndex, randomPosition, column));
        }


        CurrentMatrix ChangeInMatrix(CurrentMatrix currentMatrix, List<MovedColumnAndNeighborhood> TabuList)
        {
            List<int> indexList = currentMatrix.columnsOrder;
            int matrixWidth = matrix.GetLength(1);
            int matrixHeight = matrix.GetLength(0);
            var changedMatrix = new int[matrixHeight, matrixWidth];
            MoveInTabuList move = GetMove(currentMatrix, TabuList, matrixWidth);
            int randomColumnIndex = move.randomColumnIndex;
            int randomPosition = move.randomPosition;
            int column = move.column;
            
            indexList.RemoveAt(randomColumnIndex);           
            indexList.Insert(randomPosition, column);

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
            List<MovedColumnAndNeighborhood> TabuListForNewResult = new List<MovedColumnAndNeighborhood>();
            for (int i = 0; i < numberOfMovements; i++)
            {
                currentMatrix = ChangeInMatrix(currentMatrix, TabuListForNewResult);
            }
           
            return currentMatrix;
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


        int GetColumnInPosition(List<int> columnsInPosition, List<int> columnsOrderFromResults, int index)
        {
            List<int> top = columnsInPosition
                .GroupBy(i => i)
                .OrderByDescending(g => g.Count())
                .Take(columnsInPosition.Count())
                .Select(g => g.Key).ToList();

            if(top.Count() <= index)
            {
                int columnsCount = matrix.GetLength(1);
                for(int i = 0; i< columnsCount; i++)
                {
                    if (!columnsOrderFromResults.Contains(i))
                        return i;
                }
            }
            int val = top[index];

            return val;
        }


        int GetColumnFromResult(List<List<int>> resultsList, int index, List<int> columnsOrderFromResults)
        {
            List<int> columnsInPosition = new List<int>();
            for (int i = 0; i < resultsList.Count; i++)
            {
                columnsInPosition.Add(resultsList[i][index]);
            }
            int result = 0;
            bool found = false;
            index = 0;
            while(!found)
            {
                result = GetColumnInPosition(columnsInPosition, columnsOrderFromResults, index);
                if (columnsOrderFromResults.Contains(result))
                {
                    found = false;
                    index++;
                }
                else
                    found = true;
            }

            return result;
        }


        CurrentMatrix GetNewResult(CurrentMatrix currentMatrix, List<List<int>> resultsList)
        {
            int matrixWidth = matrix.GetLength(1);
            int matrixHeight = matrix.GetLength(0);
            var changedMatrix = new int[matrixHeight, matrixWidth];
            List<int> columnsOrderFromResults = new List<int>();

            if (resultsList.Count() == 1)
            {
                for (int i = 0; i < matrixWidth; i++)
                {
                    columnsOrderFromResults.Add(resultsList[0][i]);
                }
            }
            else
            {
                for (int i = 0; i < matrixWidth; i++)
                {
                    int columnIndexToAdd = GetColumnFromResult(resultsList, i, columnsOrderFromResults);
                    columnsOrderFromResults.Add(columnIndexToAdd);
                }
            }

            for (int i = 0; i < matrixHeight; i++)
            {
                for (int j = 0; j < matrixWidth; j++)
                {
                    changedMatrix[i, j] = matrix[i, columnsOrderFromResults[j]];
                }
            }
            CurrentMatrix newMatrix = new CurrentMatrix(changedMatrix, columnsOrderFromResults);

            return newMatrix;
        }


        void runTabuSearchAlgorithm()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var elapsedMs = watch.ElapsedMilliseconds;
            string seconds = "";

            GreedyHeuristic initialMatrix = new GreedyHeuristic(matrix);
            greedyHeuristicsMatrix = initialMatrix.GreedyHeuristicAlgorythm();
            CmaxEstimation cmaxEstimator = new CmaxEstimation(greedyHeuristicsMatrix);
            int cmaxValue = cmaxEstimator.GetCmaxValue();

            List<int> greedyHeuristicsMatrixsOrder = initialMatrix.columnsOrderInOryginalMatrix;
            CurrentResult result = new CurrentResult(cmaxValue, greedyHeuristicsMatrixsOrder, greedyHeuristicsMatrix, cmaxEstimator.columns_to_delete_in_matrix);
            CurrentResult globalResult = new CurrentResult(cmaxValue, greedyHeuristicsMatrixsOrder, greedyHeuristicsMatrix, cmaxEstimator.columns_to_delete_in_matrix);
            CurrentMatrix currentMatrix = new CurrentMatrix(greedyHeuristicsMatrix, greedyHeuristicsMatrixsOrder);
            List<MovedColumnAndNeighborhood> TabuList = new List<MovedColumnAndNeighborhood>();

            int stepWithoutBetterResultCount = 0;
            int maxStepWithoutBetterResult = theSameResultCount;
            int newResultsCount = newResultsCountVal;
            int divSteps = divStepsVal;
            bool showMatrix = true;

            int progresBarMaximumSize = newResultsCount;
            progressBar1.Maximum = progresBarMaximumSize;
            progressBar1.Value = 0;

            int value = globalResult.cmax;
            chart1.Series["Series1"].Points.Clear();

            List<List<int>> resultsList = new List<List<int>>();
            resultsList.Add(result.columnsOrder);

            while (newResultsCount != 0)
            {
                textBox1.Text = globalResult.cmax.ToString();
                if (globalResult.cmax < value) 
                {
                    value = globalResult.cmax;
                    Thread.Sleep(50);
                    chart1.Series["Series1"].Points.AddY(value);
                    chart1.Update();
                    Application.DoEvents();
                }

                if (stopTabuAlgorithm)
                {
                    watch.Stop();
                    elapsedMs = watch.ElapsedMilliseconds;
                    if (elapsedMs < 1000)
                        seconds = elapsedMs.ToString() + "ms";
                    else
                        seconds = (elapsedMs / 1000).ToString() + "s";

                    cmax = globalResult.cmax;
                    resultMatrix = globalResult.matrix;
                    columnsOrder = globalResult.columnsOrder;
                    columnsToDelete = globalResult.columnToDelete;

                    if (resultMatrix.GetLength(0) > 50 && resultMatrix.GetLength(1) > 50)
                    {
                        DialogResult dialogResult = MessageBox.Show("Matrix is big. Do you want to show matrix in program?", "Do you want to show matrix?", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.No)
                        {
                            showMatrix = false;
                        }
                    }
                    menuForm.resultMatrix(resultMatrix, cmax, columnsOrder, columnsToDelete, divStepsVal, newResultsCountVal, tabuListSize, theSameResultCount, seconds, showMatrix, percentageOfDivSteps);
                    this.Close();

                    Thread.CurrentThread.Abort();
                }

                int tabuListMaxSize = tabuListSize;
                currentMatrix = ChangeInMatrix(currentMatrix, TabuList);               
                cmaxEstimator = new CmaxEstimation(currentMatrix.newMatrix);
                cmaxValue = cmaxEstimator.GetCmaxValue();

                if (result.cmax >= cmaxValue)
                {
                    result = new CurrentResult(cmaxValue, currentMatrix.columnsOrder, currentMatrix.newMatrix, cmaxEstimator.columns_to_delete_in_matrix);
                    List<int> tmp = new List<int>();
                    for (int i = 0; i < result.columnsOrder.Count; i++)
                        tmp.Add(result.columnsOrder[i]);
                    resultsList.Add(tmp);
                }
                else
                {
                    currentMatrix = new CurrentMatrix(result.matrix, result.columnsOrder);
                }

                if (TabuList.Count > tabuListMaxSize)
                {

                    TabuList.Remove(TabuList[0]);
                }
                if (result.cmax <= cmaxValue)
                {
                    stepWithoutBetterResultCount += 1;
                }
                else
                {
                    stepWithoutBetterResultCount = 0;
                }

                if (stepWithoutBetterResultCount == Convert.ToInt32(0.8 * maxStepWithoutBetterResult))
                {
                    for (int i = 0; i < divSteps; i++)
                    {
                        if (stopTabuAlgorithm)
                            i = divSteps;

                        currentMatrix = ChangeInMatrix(currentMatrix, TabuList);
                        cmaxEstimator = new CmaxEstimation(currentMatrix.newMatrix);
                        cmaxValue = cmaxEstimator.GetCmaxValue();

                        if (result.cmax >= cmaxValue)
                        {
                            result = new CurrentResult(cmaxValue, currentMatrix.columnsOrder, currentMatrix.newMatrix, cmaxEstimator.columns_to_delete_in_matrix);
                            resultsList.Add(result.columnsOrder);
                        }
                        else
                        {
                            currentMatrix = new CurrentMatrix(result.matrix, result.columnsOrder);
                        }
                        if (TabuList.Count > tabuListMaxSize)
                        {

                            TabuList.Remove(TabuList[0]);
                        }
                    }

                    if (result.cmax <= cmaxValue)
                    {
                        stepWithoutBetterResultCount += 1;
                    }
                    else
                    {
                        stepWithoutBetterResultCount = 0;
                    }
                }
                progressBar1.Value = progresBarMaximumSize - newResultsCount + 1;
                if (stepWithoutBetterResultCount == maxStepWithoutBetterResult) //jesli nie ma poprawy
                {
                    if (globalResult.cmax > result.cmax) // zmieniamy globalne rozwiazanie
                    {
                        globalResult = result;
                        currentMatrix = new CurrentMatrix(globalResult.matrix, globalResult.columnsOrder);
                    }
                    currentMatrix = GetNewResult(currentMatrix, resultsList);
                    newResultsCount -= 1;
                    stepWithoutBetterResultCount = 0;
                    TabuList.Clear();

                    cmaxEstimator = new CmaxEstimation(currentMatrix.newMatrix);
                    cmaxValue = cmaxEstimator.GetCmaxValue();
                    currentMatrix = new CurrentMatrix(result.matrix, result.columnsOrder);
                    result = new CurrentResult(cmaxValue, currentMatrix.columnsOrder, currentMatrix.newMatrix, cmaxEstimator.columns_to_delete_in_matrix);
                }
            }

            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            if (elapsedMs < 1000)
                seconds = elapsedMs.ToString() + "ms";
            else
                seconds = (elapsedMs / 1000).ToString() + "s";

            cmax = globalResult.cmax;
            resultMatrix = globalResult.matrix;
            columnsOrder = globalResult.columnsOrder;
            columnsToDelete = globalResult.columnToDelete;

            if (resultMatrix.GetLength(0) > 50 && resultMatrix.GetLength(1) > 50)
            {
                DialogResult dialogResult = MessageBox.Show("Matrix is big. Do you want to show matrix in program?", "Do you want to show matrix?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    showMatrix = false;
                }
            }
            menuForm.resultMatrix(resultMatrix, cmax, columnsOrder, columnsToDelete, divStepsVal, newResultsCountVal, tabuListSize, theSameResultCount, seconds, showMatrix, percentageOfDivSteps);
            Thread.Sleep(900);
            this.Close();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox2.Text, out int i) || textBox2.Text == "0")
            {
                MessageBox.Show("Add value to all fields!!!");
                return;
            }
            else
            {
                newResultsCountVal = Convert.ToInt32(textBox2.Text);

            }

            if (comboBox1.Text == "0" || comboBox1.Text=="")
            {
                MessageBox.Show("Add int value to all fields!!!");
                return;
            }
            else
            {
                int matrixColumnsCount = matrix.GetLength(1);
                string val = comboBox1.Text; 
                percentageOfDivSteps = Convert.ToInt32(val.Substring(0, val.Length - 1));
                int maxValOfMovements = (matrixColumnsCount + 1) * (matrixColumnsCount - 1) * (matrixColumnsCount - 1) - (matrixColumnsCount - 1);
                divStepsVal = Convert.ToInt32(maxValOfMovements * percentageOfDivSteps / 100);
            }

            if (comboBox2.Text == "0"||comboBox2.Text == "") 
            {
                MessageBox.Show("Add int value to all fields!!!");
                return;
            }
            else
            {
                string val = comboBox2.Text;
                percentageOfTheSameResCount = Convert.ToInt32(val.Substring(0, val.Length - 1));
                int matrixColumnsCount = matrix.GetLength(1);
                int maxValOfMovements = (matrixColumnsCount + 1) * (matrixColumnsCount - 1) * (matrixColumnsCount - 1) - (matrixColumnsCount - 1);
                theSameResultCount = Convert.ToInt32((maxValOfMovements * percentageOfTheSameResCount) / 100);
            }

            if (comboBox3.Text == "0" || comboBox3.Text == "")
            {
                MessageBox.Show("Add int value to all fields!!!");
                return;
            }
            else
            {
                string val = comboBox3.Text;
                percentageOfTabuList = Convert.ToInt32(val.Substring(0, val.Length - 1));
                int matrixColumnsCount = matrix.GetLength(1);
                int maxValOfMovements = (matrixColumnsCount + 1) * (matrixColumnsCount - 1) * (matrixColumnsCount - 1) - (matrixColumnsCount - 1);
                tabuListSize = Convert.ToInt32((maxValOfMovements * percentageOfTabuList) / 100);
            }
            button1.Enabled = false;
            button2.Enabled = true;
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