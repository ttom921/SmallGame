using SmallGameLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfGuestNumber
{
    public enum GameStet
    {
        None,
        Start,
        Guess,
    }
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        GameStet gameState = GameStet.None;
        GuessNumber guessNumber = new GuessNumber();


        public MainWindow()
        {
            InitializeComponent();
            guessNumber.MessageLog = this.AddMessage;
            AddMessage("start");
            checkBtnState();
        }
        #region Message處理
        delegate void UpdateWPFMsg(string s);
        public void _UpDateString(string msg)
        {
            this.MsgTextBox.AppendText(DateTime.Now.ToString() + "  ");
            this.MsgTextBox.AppendText(msg);
            this.MsgTextBox.AppendText("\n");
            this.MsgTextBox.ScrollToEnd();
        }
        public void AddMessage(String msg)
        {
            this.Dispatcher.Invoke(new UpdateWPFMsg(this._UpDateString), new object[] { msg });
        }
        #endregion //Message處理

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (PlayerNumber.Text.Length < 4)
            {
                MessageBox.Show("請輸入4個不重覆的數字");
                return;
            }
            gameState = GameStet.Start;
            guessNumber.GenAnswerNum();
            checkBtnState();
            PCGuessNum.Text= guessNumber.GetGestNumber();

        }
        void checkBtnState()
        {
            if (gameState == GameStet.None)
            {
                BtnStart.IsEnabled = true;
                BtnSendAB.IsEnabled = false;
            }
            if (gameState == GameStet.Start)
            {
                BtnStart.IsEnabled = false;
                BtnSendAB.IsEnabled = true;
            }
        }

        private void BtnSendAB_Click(object sender, RoutedEventArgs e)
        {
            int a = 0, b = 0;
            String str = Tbnanb.Text;
            if(str.Length != 4)
            {
                MessageBox.Show("輸入錯誤, 格式為 ?A?B ");
                return;
            }
            a = str[0] - '0';
            b = str[2] - '0';
            int number = int.Parse(guessNumber.GetGestNumber());
            if (a == 4)
            {
                string msg = $"答案是=>{number}";
                MessageBox.Show(msg);
                resetguess();
                return;
            }
            guessNumber.reduce(number, a, b);
            guessNumber.GetNextGuessNumber();
            if(guessNumber.GetGestNumber()=="0-1")
            {
                MessageBox.Show("你騙人!! 根本沒這數字, ***!");
                resetguess();
                return;
            }
            PCGuessNum.Text = guessNumber.GetGestNumber();
        }
        private void resetguess()
        {
            gameState = GameStet.None;
            guessNumber.GenAnswerNum();
            PCGuessNum.Text = "";
            checkBtnState();
        }
    }
}
