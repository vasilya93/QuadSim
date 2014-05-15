using Microsoft.Research.DynamicDataDisplay;
using QuadcopterSimulation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuadcopterSimulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            XCoordinate.Description = new PenDescription("X");
            YCoordinate.Description = new PenDescription("Y");
            ZCoordinate.Description = new PenDescription("Z");

            Roll.Description = new PenDescription("Крен");
            Pitch.Description = new PenDescription("Тангаж");
            Yaw.Description = new PenDescription("Рыскание");

            FirstMotorGraph.Description = new PenDescription("1");
            SecondMotorGraph.Description = new PenDescription("2");
            ThirdMotorGraph.Description = new PenDescription("3");
            FourthMotorGraph.Description = new PenDescription("4");

            _viewModel = new MainViewModel();
            this.DataContext = _viewModel;
        }
    }
}
