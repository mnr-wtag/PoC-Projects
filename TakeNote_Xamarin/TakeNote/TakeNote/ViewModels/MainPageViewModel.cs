using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace TakeNote.ViewModels
{
    internal class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<string> AllNotes { get; set; } = new ObservableCollection<string>();
        private string _title;
        private string _note;
        public string Title { 
            get { return _title; }
            set 
            { 
                _title = value;

                var args = new PropertyChangedEventArgs(nameof(Title));
                PropertyChanged?.Invoke(this, args);
            }
        }
        public string Note
        {
            get { return _note; }
            set
            {
                _note = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Note)));
            }
        }
        public string Error { get; set; }
        public MainPageViewModel()
        {
            EraseCommand = new Command(() =>
            {
                Title = string.Empty;
                Note = string.Empty;
            }) ;

            SaveCommand = new Command(() =>
            {
                try
                {
                    AllNotes.Add((Title.ToString() + ": " + Note.ToString()).ToString());
                    Title = string.Empty;
                    Note = string.Empty;
                }
                catch (Exception ex)
                {
                    Error = ex.Message;
                }
            } ) ;
        }

        public Command EraseCommand { get; }
        public Command SaveCommand { get; }
    }
}
