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

namespace Занятие_в_аудитории_1_29._08._2023__ADO.NET_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataContext dataContext;
        public MainWindow()
        {
            InitializeComponent();
            dataContext = new();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DepartmentsCountLabel.Content = dataContext.Departments.Count().ToString();
            TopChiefCountLabel.Content = dataContext.Managers.Where(manager => manager.IdChief == null).Count().ToString();
            HasChiefCountLabel.Content = dataContext.Managers.Where(manager => manager.IdChief != null).Count().ToString();
            Guid itG = Guid.Parse(dataContext.Departments.Where(depart => depart.Name == "IT відділ").Select(depart => depart.Id).First().ToString());
            ITCountLabel.Content = dataContext.Managers.Where(manager => manager.IdMainDep == itG || manager.IdSecDep == itG).Count().ToString();
            TwoDepartmentsCountLabel.Content = dataContext.Managers.Where(manager => manager.IdMainDep != null && manager.IdSecDep != null).Count().ToString();

        }
    }
}
