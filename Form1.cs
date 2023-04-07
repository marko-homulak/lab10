using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    public partial class Form1 : Form
    {

        private Random random = new Random();

        private int[,] matrixValues;

        private int currentMin = 0;

        private int currentMax = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void generateArray_Click(object sender, EventArgs e)
        {
            int sizeCount = 0;
            try 
            {
                GetUserInputs(ref sizeCount);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            FillMatrix(sizeCount);
            ShowMatrixInWindow(sizeCount);
        }

        private void GetUserInputs(ref int sizeCount)
        {
            sizeCount = int.Parse(sizeField.Text);
            validateInputs(sizeCount);
            currentMin = int.Parse(minField.Text);
            currentMax = int.Parse(maxField.Text);
        }

        private void validateInputs(int sizeCount)
        {
            if (sizeCount < 1)
            {
                throw new ArgumentException("Size must be greater than 0!");
            }
            else if (sizeCount > 20)
            {
                throw new ArgumentException("The maximum matrix size is 20!");
            }
        }

        private void FillMatrix(int sizeCount)
        {
            matrixValues = new int[sizeCount, sizeCount];
            for (int rowI = 0; rowI < matrixValues.GetLength(0); rowI++)
            {
                for (int colI = 0; colI < matrixValues.GetLength(1); colI++)
                {
                    matrixValues[rowI, colI] = random.Next(currentMin, currentMax);
                }
            }
        }

        private void ShowMatrixInWindow(int sizeCount)
        {
            matrix.RowCount = sizeCount;
            matrix.ColumnCount = sizeCount;
            for (int rowI = 0; rowI < matrixValues.GetLength(0); rowI++)
            {
                for (int colI = 0; colI < matrixValues.GetLength(1); colI++)
                {
                    matrix.Rows[rowI].Cells[colI].Style.BackColor = Color.White;
                    matrix.Rows[rowI].Cells[colI].Value = matrixValues[rowI, colI];
                }
            }
            matrix.Show();
        }

        private void calculations_Click(object sender, EventArgs e)
        {
            int sumOfRowsThatHaveNegativeNumbers = CountSumOfRowsThatHaveNegativeNumbers();
            int k = CountSameRowsAndColls();
            int sumOfElementsOfSecondaryDiagonal = CountSumOfElementsOfSecondaryDiagonal();
            MessageBox.Show("1 sub task: " + sumOfRowsThatHaveNegativeNumbers + "\n" +
                "2 sub task: " + k + "\n" +
                "3 sub task: " + sumOfElementsOfSecondaryDiagonal);
        }

        private int CountSumOfRowsThatHaveNegativeNumbers()
        {
            int count = 0;
            for (int rowI = 0; rowI < matrixValues.GetLength(0); rowI++)
            {
                int rowCount = 0;
                bool negativeNumberWasFound = false;
                for (int colI = 0; colI < matrixValues.GetLength(1); colI++)
                {
                    if (matrixValues[rowI, colI] < 0)
                    {
                        negativeNumberWasFound = true;
                        rowCount += matrixValues[rowI, colI];
                    }
                    else
                    {
                        break;
                    }
                }
                if(negativeNumberWasFound)
                {
                    count += rowCount;
                }
            }
            return count;
        }

        private int CountSameRowsAndColls()
        {
            for (int rowI = 0; rowI <= matrixValues.GetUpperBound(0); rowI++)
            {
                int[] ArrCol = new int[matrixValues.GetLength(0)];
                int[] ArrRow = new int[matrixValues.GetLength(0)];

                // отримуємо елементи рядка і стовпця
                for (int colI = 0; colI <= matrixValues.GetUpperBound(1); colI++)
                {
                    ArrCol[colI] = matrixValues[colI, rowI];
                    ArrRow[colI] = matrixValues[rowI, colI];
                }

                // порівнюємо елементи рядка і стовпця
                if (ArrCol.SequenceEqual(ArrRow))
                {
                    // замальовуємо рядок та стовпець у зелений колір
                    for (int colI = 0; colI <= matrixValues.GetUpperBound(1); colI++)
                    {
                        matrix.Rows[colI].Cells[rowI].Style.BackColor = Color.Green;
                        matrix.Rows[rowI].Cells[colI].Style.BackColor = Color.Green;
                    }
                    return rowI;
                }
            }
            return 0;
        }

        private int CountSumOfElementsOfSecondaryDiagonal()
        {
            int sum = 0;
            for (int i = 0; i < matrixValues.GetLength(0); i++)
            {
                sum += matrixValues[i, matrixValues.GetLength(0) - i - 1];

                matrix.Rows[i].Cells[matrixValues.GetLength(0) - i - 1].Style.BackColor = Color.Yellow;
            }
            return sum;
        }
    }
}
