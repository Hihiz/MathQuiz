using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathQuiz
{
    public partial class Form1 : Form
    {
        // Случайные числа 
        Random randomizer = new Random();

        // Числа для сложения
        int addend1;
        int addend2;

        // Числа вычитания.
        int minuend;
        int subtrahend;

        // Числа умножения.
        int multiplicand;
        int multiplier;

        // Числа для деления.
        int dividend;
        int divisor;

        // Для оставшегося времени
        int timeLeft;

        public Form1()
        {
            InitializeComponent();
        }

        //
        public void StartTheQuiz()
        {
            // Два случайных числа для операции +.
            // Сохраняем значения в переменных 'addend1' и 'addend2'
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);

            // Преобразовать два случайно сгенерированных числа
            // в строки, чтобы их можно было отобразить
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            // sum - это имя элемента управления NumericUpDown.
            sum.Value = 0;

            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            // Заполните задачу на вычитание.
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // Заполните задачу на умножение.
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // Заполните задачу о разделении.
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // Запуск таймера.
            timeLeft = 30;
            timeLabel.Text = "30 секунд";
            timer1.Start();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
        }

        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value) && (minuend - subtrahend == difference.Value) && (multiplicand * multiplier == product.Value) && (dividend / divisor == quotient.Value))
                return true;
            else
                return false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                // Если функция CheckTheAnswer() возвращает значение true, то пользователь
                // получил правильный ответ. Остановите таймер
                // и показать окно сообщений.
                timer1.Stop();
                MessageBox.Show("Вы ответили правильно на все вопросы!",
                                "Поздравляю!");
                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                // Если функция CheckTheAnswer() возвращает значение false, продолжайте подсчет
                // вниз. Уменьшите оставшееся время на одну секунду и
                // отобразите новое оставшееся время, обновив метку
                // Оставшееся время.
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " секунд";
            }
            else
            {
                // Если у пользователя закончилось время, остановите таймер, покажите
                // окно сообщения и заполните ответы.
                timer1.Stop();
                timeLabel.Text = "Время вышло!";
                MessageBox.Show("У вас закончилось время, Извините!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
            }
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            // Выберите полный ответ в элементе управления NumericUpDown
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }
    }
}
