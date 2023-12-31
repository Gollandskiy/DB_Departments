﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public ObservableCollection<Department> DepartmentsV { get; set; }
        private ICollectionView departmentsListView;
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
            dataContext.Departments.Load();
            DepartmentsV = dataContext.Departments.Local.ToObservableCollection();
            DepartmentsV = new ObservableCollection<Department>(dataContext.Departments.Where(depart => depart.DeleteDt == null));
            departmentsList.ItemsSource = DepartmentsV;
            departmentsListView = CollectionViewSource.GetDefaultView(DepartmentsV);
            departmentsListView.Filter = item => (item as Department)?.DeleteDt == null;
            //DepartmentsV.CollectionChanged(null, new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset));

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

        private int _N;
        public int N { get => _N++; set => _N = value; }
        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            N = 1;
            var query = dataContext.Departments.Select(d => new Pair() { Key = (N).ToString(), Value = d.Name });


            Pairs.Clear();
            foreach (var pair in query)
            {
                Pairs.Add(pair);
            }
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            N = 1;
            var query = dataContext.Departments.
                OrderBy(d => d.Name).
                AsEnumerable().Select(d => new Pair
                {
                    Key = (N).
                ToString(),
                    Value = d.Name
                });
            foreach (var pair in query)
            {
                Pairs.Add(pair);
            }
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            var query = dataContext.Departments.
                GroupJoin(dataContext.Managers, d => d.Id, m => m.IdMainDep,
                (dep, mans) => new Pair { Key = dep.Name, Value = mans.Count().ToString() });
            foreach (var pair in query)
            {
                Pairs.Add(pair);
            }
        }

        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            var query = dataContext.Managers
                .GroupJoin(dataContext.Managers, c => c.Id, d => d.IdChief,
                (c, d) => new Pair { Key = $"{c.Surname} {c.Name[0]}.{c.Secname[0]}.", Value = d.Count().ToString() }).
                OrderByDescending(pair => Convert.ToInt32(pair.Value)).Take(3);

            foreach (var pair in query)
            {
                Pairs.Add(pair);
            }

        }
        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            var query = dataContext.Managers
                .GroupBy(m => m.Surname)
                .Select(group => new Pair
                {
                    Key = group.Key,
                    Value = group.Count().ToString()
                })
                .Where(p => Convert.ToInt32(p.Value) > 1)
                .ToList();

            for (int i = 0; i < query.Count; i++)
            {
                query[i].Key = (i + 1) + ". " + query[i].Key;
                Pairs.Add(query[i]);
            }
        }
        private void Button10_Click(object sender, RoutedEventArgs e)
        {
            var query = dataContext.Departments.GroupJoin(
                                dataContext.Managers,
                                d => d.Id,
                                m => m.IdSecDep,
                                (d, m) => new Pair { Key = d.Name, Value = m.Count().ToString() }
                            );

            foreach (var pair in query)
            {
                Pairs.Add(pair);
            }
        }
        private void Button11_Click(object sender, RoutedEventArgs e)
        {
            var query = dataContext.Managers.Include(m => m.MainDep).
                Select(m => new Pair { Key = m.Surname, Value = $"{m.MainDep.Name}/{m.SecDep.Name}" });

            foreach (var pair in query)
            {
                Pairs.Add(pair);
            }
        }

        private void Button12_Click(object sender, RoutedEventArgs e)
        {
            var query = dataContext.Managers.Include(m => m.SecDep).
                Select(m => new Pair 
                { Key = m.Surname, 
                    Value = m.SecDep == null ? "--" : m.SecDep.Name });

            foreach (var pair in query)
            {
                Pairs.Add(pair);
            }
        }
        private void Button13_Click(object sender, RoutedEventArgs e) // DZ
        {
            var query = dataContext.Managers.Include(m => m.Chief).
                Select(m => new Pair()
                {
                    Key = $"{m.Surname} {m.Name[0]}.{m.Secname[0]}",
                    Value = m.Chief == null ? "--" : $"{m.Chief.Surname} {m.Chief.Name[0]}.{m.Chief.Secname[0]}"
                });
            foreach (var pair in query)
            {
                Pairs.Add(pair);
            }
        }
        private void Button14_Click(object sender, RoutedEventArgs e)
        {
            var query = dataContext.Departments.Include(d => d.MainManagers).Where(d => d.DeleteDt == null)
                .Select(d => new Pair { Key = d.Name, Value = d.MainManagers.Count().ToString() });
            Pairs.Clear();
            foreach (var pair in query)
            {
                Pairs.Add(pair);
            }
        }
        private void Button15_Click(object sender, RoutedEventArgs e)
        {
            var query = dataContext.Departments.Include(d => d.SecManagers).Where(d => d.DeleteDt == null)
                .Select(d => new Pair { Key = d.Name, Value = d.SecManagers.Count().ToString() });
            Pairs.Clear();
            foreach (var pair in query)
            {
                Pairs.Add(pair);
            }
        }
        private void Button16_Click(object sender, RoutedEventArgs e)
        {
            IQueryable<Pair> query = dataContext.Departments
             .Where(d => d.DeleteDt == null)
             .OrderByDescending(d => Convert.ToInt32(d.MainManagers.Count()))
             .Select(d => new Pair
             {
                 Key = d.Name,
                 Value = (d.MainManagers.Count() != 0) ? d.MainManagers.Count().ToString() : "закрытый"
             });

            Pairs.Clear();
            foreach (var pair in query)
            {
                Pairs.Add(pair);
            }
        }
        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem item)
            {
                if (item.Content is Department department)
                {
                    CrudDepartment dialog = new();
                    dialog.Department = department;
                    if (dialog.ShowDialog() ?? false)
                    {
                        if (dialog.Department != null)
                        {
                            if (dialog.isDeleted)
                            {
                                department.DeleteDt = DateTime.Now;
                                dataContext.SaveChanges();
                                departmentsListView.Refresh();
                            }
                            else
                            {
                                var dep = dataContext.Departments.Find(department.Id);
                                if (dep != null)
                                {
                                    dep.Name = department.Name;
                                }
                                dataContext.SaveChanges();
                                departmentsListView.Refresh();
                                //DepartmentsV.Clear();
                                //dataContext.Departments.Load();
                                //departmentsList.ItemsSource = DepartmentsV;
                               //dataContext.Departments.Load();
                               //DepartmentsV = dataContext.Departments.Local.ToObservableCollection();
                               //departmentsList.ItemsSource = DepartmentsV;
                               //departmentsListView.Refresh();
                                //departmentsListView.Filter = item => true;
                                //departmentsListView.Filter = item => true;
                                //departmentsListView.Filter = item => (item as Department)?.DeleteDt == null;

                                //int index = DepartmentsV.IndexOf(department);
                                //DepartmentsV.Remove(department);
                                //DepartmentsV.Insert(index, department);
                            }
                        }
                        else
                        {
                            dataContext.Departments.Remove(department);
                            dataContext.SaveChanges();
                        }
                        }
                    }
            }
        }
        private void AddDepartmentButton_Click(object sender, RoutedEventArgs e)
        {
            Department NDepartment = new()
            {
                Id = Guid.NewGuid(),
                Name = null
            };
            CrudDepartment dialog = new();
            dialog.Department = NDepartment;
            if (dialog.ShowDialog() ?? false)
            {
                dataContext.Departments.Add(NDepartment);
                dataContext.SaveChanges();
            }
        }

        public class Pair
        {
            public String Key { get; set; } = null;
            public String? Value { get; set; }
        }
    }
}
