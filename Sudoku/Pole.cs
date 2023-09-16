using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sudoku
{
    internal class Pole
    {
        internal int dimension;
        private enum Variant
        {
            ColumnOneAndTwo,
            ColumnTwoAndThree,
            ColunmnOneAndThree,
            ColumnFourAndFive,
            ColumnFiveAndSix,
            ColumnFourAndSix,
            ColumnSevenAndEight,
            ColumnSevenAndNine,
            ColumnEightAndNine,
            RowOneAndTwo,
            RowTwoAndThree,
            RowOneAndThree,
            RowFourAndFive,
            RowFiveAndSix,
            RowFourAndSix,
            RowSevenAndEight,
            RowSevenAndNine,
            RowEightAndNine
        }

        private int[,] array;
        public int[,] Array 
        {
            get => array;
        }
        public TextBox[,] ArrayTextBoxes { get; set; }
        public Pole () 
        {
            dimension = 1;
            array = new int[1, 1];
            ArrayTextBoxes = new TextBox[1, 1];
        }
        public Pole (int size)
        {
            dimension = size;
            array = new int[size, size];
            ArrayTextBoxes = new TextBox[size, size];

            InitArray();

            SortForComplexity();
        }
        public void InitArray ()
        {
            for (int i = 0; i < this.dimension; i++)
            {
                for (int j = 0; j < this.dimension; j++)
                {
                    array[i, j] = (i * (int)Math.Sqrt(this.dimension) + i / (int)Math.Sqrt(this.dimension) + j) % (this.dimension) + 1;
                }
            }
        }
        /// <summary>
        /// SortForComplexity 
        /// </summary>
        /// <param name="n"></param>
        public void SortForComplexity()
        {
            Random random = new Random();

            for (int i = 0; i < random.Next(10, 40); i++)
            {
                if (this.dimension == 4)
                {
                    Variant variant = (Variant)random.Next(1, 4);
                    switch (variant)
                    {
                        case Variant.ColumnOneAndTwo:
                            Sort(0, 1, true);
                            break;
                        case Variant.ColunmnOneAndThree:
                            Sort(2, 3, true);
                            break;
                        case Variant.ColumnTwoAndThree:
                            Sort(0, 1, false);
                            break;
                        case Variant.ColumnFourAndFive:
                            Sort(2, 3, false);
                            break;
                        default:
                            break;
                    }
                }
                else if (this.dimension == 9)
                {
                    Variant variant = (Variant)random.Next(1, 19);
                    switch (variant)
                    {
                        case Variant.ColumnOneAndTwo:
                            Sort(0, 1, true);
                            break;
                        case Variant.ColunmnOneAndThree:
                            Sort(0, 2, true);
                            break;
                        case Variant.ColumnTwoAndThree:
                            Sort(1, 2, true);
                            break;
                        case Variant.ColumnFourAndFive:
                            Sort(3, 4, true);
                            break;
                        case Variant.ColumnFiveAndSix:
                            Sort(3, 5, true);
                            break;
                        case Variant.ColumnFourAndSix:
                            Sort(4, 5, true);
                            break;
                        case Variant.ColumnSevenAndEight:
                            Sort(6, 7, true);
                            break;
                        case Variant.ColumnSevenAndNine:
                            Sort(6, 8, true);
                            break;
                        case Variant.ColumnEightAndNine:
                            Sort(7, 8, true);
                            break;
                        case Variant.RowOneAndTwo:
                            Sort(0, 1, false);
                            break;
                        case Variant.RowTwoAndThree:
                            Sort(1, 2, false);
                            break;
                        case Variant.RowOneAndThree:
                            Sort(0, 2, false);
                            break;
                        case Variant.RowFourAndFive:
                            Sort(3, 4, false);
                            break;
                        case Variant.RowFiveAndSix:
                            Sort(4, 5, false);
                            break;
                        case Variant.RowFourAndSix:
                            Sort(3, 5, false);
                            break;
                        case Variant.RowSevenAndEight:
                            Sort(6, 7, false);
                            break;
                        case Variant.RowSevenAndNine:
                            Sort(7, 8, false);
                            break;
                        case Variant.RowEightAndNine:
                            Sort(6, 8, false);
                            break;
                        default:
                            break;
                    }
                }     
            }

            void Sort(int firstValue, int secondValue, bool isColumnOrRow)
            {
                if (isColumnOrRow)
                {
                    for (int i = 0; i < this.dimension; i++)
                    {
                        (array[i, firstValue], array[i, secondValue]) = (array[i, secondValue], array[i, firstValue]);
                    }
                }
                else
                {
                    for (int i = 0; i < this.dimension; i++)
                    {
                        (array[firstValue, i], array[secondValue, i]) = (array[secondValue, i], array[firstValue, i]);
                    }
                }
            }
        }
    }
}
