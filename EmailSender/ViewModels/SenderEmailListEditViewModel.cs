using EmailSender.Infrastructure.Commands;
using EmailSender.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EmailSender.ViewModels
{
    class SenderEmailListEditViewModel : ViewModel, INotifyPropertyChanged
    {
        public string btnAcceptContext { get; set; }
        public string Title { get; set; }
        public string tbEmail { get; set; }
        public bool? DialogResult { get; set; }
        public SenderEmailListEditViewModel(string Mode, string email = "")
        {
            switch (Mode)
            {
                case "Edit":
                    btnAcceptContext = "Изменить";
                    Title = "Изменение email";
                    tbEmail = email;
                    break;
                default:
                    btnAcceptContext = "Добавить";
                    Title = "Добавление email";
                    break;
            }
            btnAcceptCommand = new LambdaCommand(OnbtnAcceptCommandExecuted, CanbtnAcceptCommandExecute);
        }
        public SenderEmailListEditViewModel() { }

        #region btnAcceptCommand
        public ICommand btnAcceptCommand { get; }

        private void OnbtnAcceptCommandExecuted(object p)
        {
            
            if (tbEmail.Length > 0) DialogResult = true; 
            else MessageBox.Show("email не может быть пустым", "ошибка"); 
        }

        private bool CanbtnAcceptCommandExecute(object p) => true;
        #endregion
    }
}
