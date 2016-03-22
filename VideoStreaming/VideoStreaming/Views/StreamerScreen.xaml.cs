﻿using System;
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
using UserControl = System.Windows.Controls.UserControl;

namespace VideoStreaming.WPF.Views
{
    /// <summary>
    /// Interaction logic for StreamerScreen.xaml
    /// </summary>
    public partial class StreamerScreen : UserControl
    {
        public StreamerScreen()
        {
          
            InitializeComponent();
        }


        private void BtnPlay_OnClick(object sender, RoutedEventArgs e)
        {

            MyMediaElement.Source = new Uri("http://star.010e.net:8000/live/ahmedmadi/123456/72.ts");
            MyMediaElement.Play();
        }
    }
}
