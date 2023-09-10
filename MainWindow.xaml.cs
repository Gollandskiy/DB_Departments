using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<Pair> Pairs { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            dataContext = new();
            Pairs = new();
            this.DataContext = this;
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

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
           var query = dataContext.Managers.Where(m => m.IdMainDep == Guid.Parse("131ef84b-f06e-494b-848f-bb4bc0604266"))
                .Select(m => new Pair { Key = m.Surname, Value = $"{m.Name[0]}.{m.Secname[0]}." });
            Pairs.Clear();
            foreach (var pair in query)
            {
                Pairs.Add(pair);
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            var query = dataContext.Managers.Join
                (dataContext.Departments, m => m.IdMainDep, d => d.Id,
                (m, d) => new Pair { Key = $"{m.Surname}", Value = d.Name }).Take(10);
                 Pairs.Clear();
                foreach (var pair in query)
                {
                    Pairs.Add(pair);
                }
            }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            var query = dataContext.Managers.Join
                (dataContext.Managers, m => m.IdChief, chief => chief.Id, (m, chief) => new Pair 
                { Key = m.Surname, Value = chief.Surname }).OrderBy(pair => pair.Key).ToList();
            Pairs.Clear();
            foreach (var pair in query)
            {
                Pairs.Add(pair);
            }

        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            var query = dataContext.Managers.Join(
                dataContext.Departments,
                m => m.IdMainDep,
                d => d.Id,
                (m, d) => new Pair
                {
                    Key = $"{m.Surname}, {m.Name[0]}.{m.Secname[0]}",
                    Value = d.Name,
                }).OrderBy(pair => pair.Value);

            Pairs.Clear();
            foreach (var pair in query)
            {
                Pairs.Add(pair);
            }
        }
    }

    public class Pair 
    {
        public String Key { get; set; } = null;
        public String? Value { get; set; }
    }

}
