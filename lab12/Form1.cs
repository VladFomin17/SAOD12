using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace lab12
{
    public partial class SortingForm : Form
    {
        public SortingForm()
        {
            InitializeComponent();
            dataGridView1.RowCount = 7;
            dataGridView1.ColumnCount = 6;
            dataGridView1.Rows[0].Cells[1].Value = "Обмен";
            dataGridView1.Rows[1].Cells[1].Value = "Выбор";
            dataGridView1.Rows[2].Cells[1].Value = "Включение";
            dataGridView1.Rows[3].Cells[1].Value = "Шелла";
            dataGridView1.Rows[4].Cells[1].Value = "Быстрая";
            dataGridView1.Rows[5].Cells[1].Value = "Линейная";
            dataGridView1.Rows[6].Cells[1].Value = "Встроенная";

            dataGridView1.Rows[0].Cells[0].Value = true;
            dataGridView1.Rows[1].Cells[0].Value = true;
            dataGridView1.Rows[2].Cells[0].Value = true;
        }

        bool IsSorted(int[] a)
        {
            for (int i = 0; i < a.Length - 1; i++)
                if (a[i] > a[i + 1])
                    return false;
            return true;
        }

        void BubbleSort(int[] a, out int comparisons, out int swaps, out int time)
        {
            int n = a.Length;
            comparisons = 0;
            swaps = 0;
            bool sorted = false;
            int t1 = Environment.TickCount;
            while (!sorted)
            {
                sorted = true;
                for (int i = 0; i < n - 1; i++)
                {
                    comparisons++;
                    if (a[i] > a[i + 1])
                    {
                        sorted = false;
                        int temp = a[i];
                        a[i] = a[i + 1];
                        a[i + 1] = temp;
                        swaps++;
                    }
                }
                n--;
            }
            time = Environment.TickCount - t1;
        }

        void SelectionSort(int[] a, out int comparisons, out int swaps, out int time)
        {
            comparisons = 0;
            swaps = 0;

            int t1 = Environment.TickCount;
            for (int i = a.Length - 1; i > 0; i--)
            {
                int maxIndex = 0;

                for (int j = 1; j <= i; j++)
                {
                    comparisons++;
                    if (a[j] > a[maxIndex])
                        maxIndex = j;
                }

                int t = a[i];
                a[i] = a[maxIndex];
                a[maxIndex] = t;
                swaps++;
            }
            time = Environment.TickCount - t1;
        }

        private void InsertionSortWithBarrier(int[] array, out int comparisons, out int swaps, out int time)
        {
            int n = array.Length;
            comparisons = 0;
            swaps = 0;

            int t1 = Environment.TickCount;

            int minIndex = 0;
            for (int j = 1; j < n; j++)
            {
                if (array[j] < array[minIndex])
                    minIndex = j;
            }

            (array[0], array[minIndex]) = (array[minIndex], array[0]);
            swaps++;

            for (int i = 2; i < n; i++)
            {
                int key = array[i];
                int j;
                for (j = i - 1; array[j] > key; j--)
                {
                    array[j + 1] = array[j];
                    swaps++;
                    comparisons++;
                }
                array[j + 1] = key;
                swaps++;
            }

            time = Environment.TickCount - t1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void onSortClick(object sender, EventArgs e)
        {
            int n = (int)arraySize.Value;
            Random rnd = new Random();

            int[] source = new int[n];
            for (int i = 0; i < n; i++)
                source[i] = rnd.Next(n);

            int comparisons, swaps;

            if ((bool)dataGridView1.Rows[0].Cells[0].Value)
            {
                int[] sortingArray = (int[])source.Clone();
                int time = 0;

                BubbleSort(sortingArray, out comparisons, out swaps, out time);

                dataGridView1.Rows[0].Cells[2].Value = comparisons;
                dataGridView1.Rows[0].Cells[3].Value = swaps;
                dataGridView1.Rows[0].Cells[4].Value = time;
                dataGridView1.Rows[0].Cells[5].Value = IsSorted(sortingArray) ? "Да" : "Нет";
            }

            if ((bool)dataGridView1.Rows[1].Cells[0].Value)
            {
                int[] sortingArray = (int[])source.Clone();
                int time = 0;

                SelectionSort(sortingArray, out comparisons, out swaps, out time);

                dataGridView1.Rows[1].Cells[2].Value = comparisons;
                dataGridView1.Rows[1].Cells[3].Value = swaps;
                dataGridView1.Rows[1].Cells[4].Value = time;
                dataGridView1.Rows[1].Cells[5].Value = IsSorted(sortingArray) ? "Да" : "Нет";
            }


            if ((bool)dataGridView1.Rows[2].Cells[0].Value)
            {
                int[] sortingArray = (int[])source.Clone();
                int time = 0;

                InsertionSortWithBarrier(sortingArray, out comparisons, out swaps, out time);

                dataGridView1.Rows[2].Cells[2].Value = comparisons;
                dataGridView1.Rows[2].Cells[3].Value = swaps;
                dataGridView1.Rows[2].Cells[4].Value = time;
                dataGridView1.Rows[2].Cells[5].Value = IsSorted(sortingArray) ? "Да" : "Нет";
            }
        }
    }
}
