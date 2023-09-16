using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System;
using System.Linq;

namespace profiles2
{
    public class ApplicationViewModel : INotifyPropertyChanged 
    {
        private Profile selectedProfile;

        IFileService fileService;
        IDialogService dialogService;

        public ObservableCollection<Profile> Profiles { get; set; } // коллекция профилей выдающая уведомления

        // команда сохранения файла XML
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.SaveFileDialog() == true)
                          {
                              fileService.Save(dialogService.FilePath, Profiles.ToList());
                              dialogService.ShowMessage("Файл сохранен");
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }


        // команда открытия файла XML
        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                  (openCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.OpenFileDialog() == true)
                          {
                              var profiles = fileService.Open(dialogService.FilePath);
                              Profiles.Clear();
                              foreach (var p in profiles)
                                  Profiles.Add(p);
                              dialogService.ShowMessage("Файл открыт");
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }


        // команда добавления нового профиля
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      Profile profile = new Profile();
                      Profiles.Insert(0, profile);
                      SelectedProfile = profile;
                  }));
            }
        }
        // команда удаления профиля
        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand(obj =>
                  {
                      Profile profile = obj as Profile;
                      if (profile != null)
                      {
                          Profiles.Remove(profile);
                      }
                  },
                 (obj) => Profiles.Count > 0));
            }
        }
        public Profile SelectedProfile // выбранный профиль
        {
            get { return selectedProfile; }
            set
            {
                selectedProfile = value;
                OnPropertyChanged("SelectedProfile");
            }
        }

        public ApplicationViewModel(IDialogService dialogService, IFileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;

            // Данные по умолчанию
            Profiles = new ObservableCollection<Profile>
            {
                new Profile {LastName="Кривой", FirstName="Армен", Patronymic="Иванович", Country="Россия" },
                new Profile {LastName="Ху", FirstName="Дзе", Patronymic ="Пан", Country="Китай" },
                new Profile {LastName="Бубуто", FirstName="Мамука", Patronymic="Аджабра", Country="Буркина-Фасо" },
                new Profile {LastName="Жюко", FirstName="Мануэль", Patronymic="Пожо", Country="Франция" }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged; 
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }      
       
    }
   

}