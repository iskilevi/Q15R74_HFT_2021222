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
    public class PlayerEditorWindowViewModel : ObservableRecipient
    {

        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<Player> Players { get; set; }

        private Player selectedPlayer;

        public Player SelectedPlayer
        {
            get { return selectedPlayer; }
            set
            {
                if (value != null)
                {
                    selectedPlayer = new Player()
                    {
                        Name = value.Name,
                        PlayerId = value.PlayerId,
                        Age = value.Age,
                        Salary = value.Salary,
                        ClubId = value.ClubId
                    };
                    OnPropertyChanged();
                    (DeletePlayerCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public ICommand CreatePlayerCommand { get; set; }
        public ICommand DeletePlayerCommand { get; set; }
        public ICommand UpdatePlayerCommand { get; set; }

        public static bool isInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public PlayerEditorWindowViewModel()
        {
            if (!isInDesignMode)
            {
                Players = new RestCollection<Player>("http://localhost:53910/", "player", "hub");

                CreatePlayerCommand = new RelayCommand(() =>
                {
                    Players.Add(new Player()
                    {
                        Name = SelectedPlayer.Name,
                        Age = SelectedPlayer.Age,
                        Salary = selectedPlayer.Salary
                    });

                });

                DeletePlayerCommand = new RelayCommand(() =>
                {
                    Players.Delete(SelectedPlayer.PlayerId);
                },
                () =>
                {
                    return SelectedPlayer != null;
                });

                UpdatePlayerCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Players.Update(SelectedPlayer);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                SelectedPlayer = new Player();
            }
        }
    }
}
