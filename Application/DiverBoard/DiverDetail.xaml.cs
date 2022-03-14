using System;
using System.Windows;
using DiverBoard.Models;


namespace DiverBoard
{
    public partial class DiverDetail : Window
    {
        Bunk _bunk;
        Dive _dive;

        public DiverDetail(Bunk bunk, Dive dive)
        {


            InitializeComponent();
            _bunk = bunk;
            _dive = dive;

            textBlockDepth.Text = _dive.MaxDepth;
            textBlockTBottomTime.Text = _dive.BottomTime;
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void textBlockDepth_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _dive.MaxDepth = textBlockDepth.Text;
        }

        private void textBlockTBottomTime_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _dive.BottomTime = textBlockTBottomTime.Text;
        }
    }
}
