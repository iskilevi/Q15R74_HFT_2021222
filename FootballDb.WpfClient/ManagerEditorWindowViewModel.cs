using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Q15R74_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FootballDb.WpfClient
{
    class ManagerEditorWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<Manager> Managers { get; set; }

        private Manager selectedManager;

        public Manager SelectedManager
        {
            get { return selectedManager; }
            set
            {
                if (value != null)
                {
                    selectedManager = new Manager()
                    {
                        ManagerId = value.ManagerId,
                        Name = value.Name,
                        Salary = value.Salary,
                        Club = value.Club
                    };
                    OnPropertyChanged();
                    (DeleteManagerCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public ICommand CreateManagerCommand { get; set; }
        public ICommand DeleteManagerCommand { get; set; }
        public ICommand UpdateManagerCommand { get; set; }

        public static bool isInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public ManagerEditorWindowViewModel()
        {
            if (!isInDesignMode)
            {
                Managers = new RestCollection<Manager>("http://localhost:53910/", "manager", "hub");

                CreateManagerCommand = new RelayCommand(() =>
                {
                    Managers.Add(new Manager()
                    {
                        Name = SelectedManager.Name,
                        Club = SelectedManager.Club,
                        Salary = SelectedManager.Salary
                    });

                });

                DeleteManagerCommand = new RelayCommand(() =>
                {
                    Managers.Delete(SelectedManager.ManagerId);
                },
                () =>
                {
                    return SelectedManager != null;
                });

                UpdateManagerCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Managers.Update(SelectedManager);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                SelectedManager = new Manager();
            }
        }
    }
}
