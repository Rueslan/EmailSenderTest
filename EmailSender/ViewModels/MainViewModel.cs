using System;
using EmailSender.Models;
using EmailSender.ViewModels.Base;
using System.Windows.Input;
using EmailSender.Infrastructure.Commands;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

namespace EmailSender.ViewModels
{
    class MainViewModel : ViewModel
    {
        public EmailInfo mail { get; set; } = new EmailInfo();
        public ObservableCollection<String> SenderEmails { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<TargetEmail> TargetEmails { get; set; } = new ObservableCollection<TargetEmail>();
        public Dictionary<string, int> smtpServers { get; set; } = new Dictionary<string, int>();
        public EmailSendService mailSendService { get; set; } = new EmailSendService();
        public string Log { get; set; } = String.Empty;
        public string cbSender { get; set; } = String.Empty;
        public string pbPassword { get; set; } = String.Empty;
        public string tbMessage { get; set; } = String.Empty;        
        public string tbTarget { get; set; } = String.Empty;        
        public string tbSmtp { get; set; } = String.Empty;
        public string tbPort { get; set; } = String.Empty;
        public string tbSubject { get; set; } = String.Empty;
        public string Journal { get; set; } = String.Empty;
        public int lbTargetEmailsCount { get; set; } = 0;
        public int SenderEmailsSelectedIndex { get; set; } = -1;
        public int TargetEmailsSelectedIndex { get; set; } = -1;
        public bool cbMassSendToggle { get; set; } = false;

        EventPresenter eventPresenter; //Временное решение



        #region Commands

        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        private bool CanCloseApplicationCommandExecute(object p) => true;
        #endregion

        #region TrySendMessageCommand
        public ICommand TrySendMessageCommand { get; }

        private void OnTrySendMessageExecuted(object p)
        {
            if (IsFilledCorrect())
            {
                TrySend();
            }            
        }

        private bool CanTrySendMessageExecute(object p) => true;
        #endregion

        #region AddSenderCommand
        public ICommand AddSenderCommand { get; }

        private void OnAddSenderCommandExecuted(object p)
        {
            eventPresenter.AddSenderEmail(); 
        }

        private bool CanAddSenderCommandExecute(object p) => true;
        #endregion

        #region ChangeSenderCommand
        public ICommand ChangeSenderCommand { get; }

        private void OnChangeSenderCommandExecuted(object p)
        {
            eventPresenter.ChangeSenderEmail(SenderEmailsSelectedIndex);
        }

        private bool CanChangeSenderCommandExecute(object p) => true;
        #endregion

        #region DeleteSenderCommand
        public ICommand DeleteSenderCommand { get; }

        private void OnDeleteSenderCommandExecuted(object p)
        {
            eventPresenter.DeleteSenderEmail(SenderEmailsSelectedIndex);
        }

        private bool CanDeleteSenderCommandExecute(object p) => true;
        #endregion

        #region AddTargetEmailCommand
        public ICommand AddTargetEmailCommand { get; }

        private void OnAddTargetEmailCommandExecuted(object p)
        {
            eventPresenter.AddTargetEmail();
        }

        private bool CanAddTargetEmailCommandExecute(object p) => true;
        #endregion

        #region ChangeTargetEmailCommand
        public ICommand ChangeTargetEmailCommand { get; }

        private void OnChangeTargetEmailCommandExecuted(object p)
        {
            eventPresenter.ChangeTargetEmail(TargetEmailsSelectedIndex);
        }

        private bool CanChangeTargetEmailCommandExecute(object p) => true;
        #endregion

        #region DeleteTargetEmailCommand
        public ICommand DeleteTargetEmailCommand { get; }

        private void OnDeleteTargetEmailCommandExecuted(object p)
        {
            eventPresenter.DeleteTargetEmail(TargetEmailsSelectedIndex);
        }

        private bool CanDeleteTargetEmailCommandExecute(object p) => true;
        #endregion

        #region ClearLogCommand
        public ICommand ClearLogCommand { get; }

        private void OnClearLogCommandExecuted(object p)
        {
            Journal = "";
        }

        private bool CanClearLogCommandExecute(object p) => true;
        #endregion

        #endregion



        public MainViewModel()
        {
            eventPresenter = new EventPresenter(SenderEmails, TargetEmails, smtpServers);
            WriteLog();
            smtpServers.Add("smtp.mail.ru", 25);
            smtpServers.Add("smtp.yandex.ru", 25);
            SenderEmails.Add("ivanivanovtest82@mail.ru");
            TargetEmails.Add(new TargetEmail { Name = "Иван", Email = "ivanivanovtest82@mail.ru", Smtp = "smtp.mail.ru", Port = 25, Info = "Тестовый аккаунт" });

            #region Commands
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            TrySendMessageCommand = new LambdaCommand(OnTrySendMessageExecuted, CanTrySendMessageExecute);
            AddSenderCommand = new LambdaCommand(OnAddSenderCommandExecuted, CanAddSenderCommandExecute);
            ChangeSenderCommand = new LambdaCommand(OnChangeSenderCommandExecuted, CanChangeSenderCommandExecute);
            DeleteSenderCommand = new LambdaCommand(OnDeleteSenderCommandExecuted, CanDeleteSenderCommandExecute);
            AddTargetEmailCommand = new LambdaCommand(OnAddTargetEmailCommandExecuted, CanAddTargetEmailCommandExecute);
            ChangeTargetEmailCommand = new LambdaCommand(OnChangeTargetEmailCommandExecuted, CanChangeTargetEmailCommandExecute);
            DeleteTargetEmailCommand = new LambdaCommand(OnDeleteTargetEmailCommandExecuted, CanDeleteTargetEmailCommandExecute);
            ClearLogCommand = new LambdaCommand(OnClearLogCommandExecuted, CanClearLogCommandExecute);
            #endregion
        }
        private bool IsFilledCorrect()
        {
            if (cbSender.Length == 0)
            {
                MessageBox.Show("Отправитель не выбран", "Ошибка");
                return false;
            }
            if (pbPassword != String.Empty)
            {
                MessageBox.Show("Не введен пароль", "Ошибка");
                return false;
            }
            if (tbMessage.Length == 0)
            {
                MessageBox.Show("Отсутствует текст сообщения", "Ошибка");
                return false;
            }
            if (cbMassSendToggle == false)
            {
                if (tbSmtp.Length == 0)
                {
                    MessageBox.Show("Не указан smtp сервер", "Ошибка");
                    return false;
                }
                if (tbTarget.Length == 0)
                {
                    MessageBox.Show("Отсутствует адресат", "Ошибка");
                    return false;
                }
            }
            else
            {
                if (lbTargetEmailsCount == 0)
                {
                    MessageBox.Show("Список адресатов пуст", "Ошибка");
                    return false;
                }
            }

            return true;
        }

        private void WriteLog()
        {
            Log += $"{DateTime.Now} \r\n {mailSendService.Status} \r\n {mailSendService.ErrorInfo} \r\n";
            Journal += Log;
        }

        public void TrySend()
        {
            mail.Sender = cbSender;
            mail.Password = pbPassword;
            mail.Subject = tbSubject;
            mail.Body = tbMessage;
            if (cbMassSendToggle == true)
            {
                for (int i = 0; i < TargetEmails.Count; i++)
                {
                    mail.Target = TargetEmails[i].Email;
                    mail.SmtpClient = TargetEmails[i].Smtp;
                    mail.Port = TargetEmails[i].Port;
                    mailSendService.Send(mail);
                    WriteLog();
                }
            }
            else
            {
                mail.Target = tbTarget;
                mail.SmtpClient = tbSmtp;
                mail.Port = smtpServers[tbSmtp];
                mailSendService.Send(mail);
                WriteLog();
            }
        }
        
    }
}
