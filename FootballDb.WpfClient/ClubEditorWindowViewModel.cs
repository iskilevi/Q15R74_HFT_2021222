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
    class ClubEditorWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<Club> Clubs { get; set; }

        private Club selectedClub;

        public Club SelectedClub
        {
            get { return selectedClub; }
            set
            {
                if (value != null)
                {
                    selectedClub = new Club()
                    {
                        Name = value.Name,
                        ManagerId = value.ManagerId
                    };
                    OnPropertyChanged();
                    (DeleteClubCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public ICommand CreateClubCommand { get; set; }
        public ICommand DeleteClubCommand { get; set; }
        public ICommand UpdateClubCommand { get; set; }

        public static bool isInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public ClubEditorWindowViewModel()
        {
            if (!isInDesignMode)
            {
                Clubs = new RestCollection<Club>("http://localhost:53910/", "club", "hub");

                CreateClubCommand = new RelayCommand(() =>
                {
                    Clubs.Add(new Club()
                    {
                        Name = SelectedClub.Name,
                    });

                });

                DeleteClubCommand = new RelayCommand(() =>
                {
                    Clubs.Delete(SelectedClub.ClubId);
                },
                () =>
                {
                    return SelectedClub != null;
                });

                UpdateClubCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Clubs.Update(SelectedClub);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                SelectedClub = new Club();
            }
        }
    }
}
