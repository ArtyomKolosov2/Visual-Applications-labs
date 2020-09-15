﻿using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Math;

namespace Lab_Work_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string text_x1 = GetX1_Input.Text;
            string text_x2 = GetX2_Input.Text;
            string text_n = GetN_Input.Text;
            bool Is_Succes = true;
            if (!double.TryParse(text_x1, out double x1))
            {
                Is_Succes = false;
                text_x1 = "Invalid data!";
            }
            if (!double.TryParse(text_x2, out double x2))
            {
                Is_Succes = false;
                text_x2 = "Invalid data!";
            }
            if (!int.TryParse(text_n, out int n))
            {
                Is_Succes = false;
                text_n = "Invalid data!";
            }
            ResultTextBox.Clear();
            string resultString =
               "Лаб. раб. №3 Ст.Гр. 10701219 Колосов А.А\n" +
               $"x1 = {text_x1}\n" +
               $"x2 = {text_x2}\n" +
               $"N = {text_n}\n";
            ResultTextBox.Text+= resultString;
            if (Is_Succes)
            {
                ResultTextBox.Text += await StartCountAsync(x1, x2, n);
            }
        }

        private async Task<string> StartCountAsync(double xn, double xk, int n)
        {
            return await Task.Run(() => StartCount(xn, xk, n));
        }
        private string StartCount(double xn, double xk,  int n)
        {
            double h = (xk - xn) / n;
            StringBuilder result_str = new StringBuilder();
            for (int i = 0; i < n; i++, xn+=h)
            {
                xn = Round(xn, 4);
                result_str.Append
                (
                    $"{(i+1).ToString()}. xn = {xn}, S(x) = {Round(GetSXFunctionResult(xn), 4)} \t " +
                    $"Y(x) = {Round(GetYXFunctionResult(xn), 4)}\n"
                );
            }
            return result_str.ToString();
        }

        private double GetSXFunctionResult(double x)
        {
            double Co = 1;
            double f = Co;
            double sum = f;
            int k = 0;
            while (k < 500)
            {
                double T = -(Pow(x, 2) *(k + 1)) / ((2 * Pow(k, 2) + 1) * (2 * k + 1));
                f *= T;
                sum += f;
                k++;
            }
            return sum;
        }

        private double GetYXFunctionResult(double x)
        {
            return (1 - ((x * x) / 2d)) * Cos(x) - (x / 2d) * Sin(x);
        }
    }
}
