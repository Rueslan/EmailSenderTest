using EmailSender.ViewModels;
using EmailSender.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;


// Временное решение...
namespace EmailSender.Models
{
    class EventPresenter
    {
        public ObservableCollection<string> SenderEmails { get; set; }
        public ObservableCollection<TargetEmail> TargetEmails { get; set; }
        public Dictionary<string, int> SmtpServers { get; set; }

        public EventPresenter(ObservableCollection<String> SenderEmails, ObservableCollection<TargetEmail> TargetEmails, Dictionary<string,int> SmtpServers)
        {
            this.SenderEmails = SenderEmails;
            this.TargetEmails = TargetEmails;
            this.SmtpServers = SmtpServers;
        }
        public void AddSenderEmail()
        {
            SenderEmailListEditViewModel senderEmailListEditViewModel = new SenderEmailListEditViewModel("Add");
            SenderEmailListEditView senderEmailListEditView = new SenderEmailListEditView();
            senderEmailListEditView.DataContext = senderEmailListEditViewModel;
            senderEmailListEditView.ShowDialog();
            if (senderEmailListEditView.DialogResult == true)
            {
                SenderEmails.Add(senderEmailListEditView.tbEmail.Text);
            }
        }
        public void ChangeSenderEmail(int SenderEmailsSelectedIndex)
        {

            if (SenderEmailsSelectedIndex >= 0)
            {
                SenderEmailListEditViewModel senderEmailListEditViewModel = new SenderEmailListEditViewModel("Edit", SenderEmails[SenderEmailsSelectedIndex]);
                SenderEmailListEditView senderEmailListEditView = new SenderEmailListEditView();
                senderEmailListEditView.DataContext = senderEmailListEditViewModel;
                senderEmailListEditView.ShowDialog();
                if (senderEmailListEditView.DialogResult == true)
                {
                    SenderEmails[SenderEmailsSelectedIndex] = senderEmailListEditView.tbEmail.Text;
                }
            }
            else
            {
                MessageBox.Show("Почтовый ящик не выбран", "Ошибка");
            }
        }
        public void DeleteSenderEmail(int SenderEmailsSelectedIndex)
        {
            if (SenderEmailsSelectedIndex >= 0)
            {
                if (MessageBox.Show("Удалить выбранный почтовый ящик?", "Подтверждение", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    SenderEmails.Remove(SenderEmails[(int)SenderEmailsSelectedIndex]);
                }
            }
            else
            {
                MessageBox.Show("Почтовый ящик не выбран", "Ошибка");
            }
        }
        public void AddTargetEmail()
        {
            TargetEmailListEditView targetEmailListEditView = new TargetEmailListEditView();
            targetEmailListEditView.Title = "Добавление адресата";
            targetEmailListEditView.btnAccept.Content = "Добавить";
            targetEmailListEditView.btnAccept.Click += delegate 
            {
                if (targetEmailListEditView.tbEmail.Text.Length == 0)
                { MessageBox.Show("Отсутствует email", "ошибка"); return; }
                if (targetEmailListEditView.tbSmtp.Text.Length == 0)
                { MessageBox.Show("Отсутствует smtp сервер", "ошибка"); return; }
                if (targetEmailListEditView.tbName.Text.Length == 0)
                     targetEmailListEditView.tbName.Text = "Без имени";
                targetEmailListEditView.DialogResult = true;
            };
            targetEmailListEditView.tbSmtp.ItemsSource = SmtpServers.Keys;
            targetEmailListEditView.ShowDialog();
            if (targetEmailListEditView.DialogResult.HasValue && targetEmailListEditView.DialogResult.Value == true)
            {
                TargetEmails.Add(new TargetEmail
                {
                    Email = targetEmailListEditView.tbEmail.Text,
                    Name = targetEmailListEditView.tbName.Text,
                    Smtp = targetEmailListEditView.tbSmtp.Text,
                    Port = SmtpServers[targetEmailListEditView.tbSmtp.Text],
                    Info = targetEmailListEditView.tbInfo.Text
                });
            }
        }
        public void ChangeTargetEmail(int TargetEmailsSelectedIndex)
        {
            if (TargetEmailsSelectedIndex >= 0)
            {
                int index = (int)TargetEmailsSelectedIndex;
                TargetEmailListEditView targetEmailListEditView = new TargetEmailListEditView();
                targetEmailListEditView.Title = "Изменение адресата";
                targetEmailListEditView.btnAccept.Content = "Изменить";
                targetEmailListEditView.btnAccept.Click += delegate
                {
                    if (targetEmailListEditView.tbEmail.Text.Length == 0)
                    { MessageBox.Show("Отсутствует email", "ошибка"); return; }
                    if (targetEmailListEditView.tbSmtp.Text.Length == 0)
                    { MessageBox.Show("Отсутствует smtp сервер", "ошибка"); return; }
                    if (targetEmailListEditView.tbName.Text.Length == 0)
                        targetEmailListEditView.tbName.Text = "Без имени";
                    targetEmailListEditView.DialogResult = true;
                };
                targetEmailListEditView.tbSmtp.ItemsSource = SmtpServers.Keys;
                targetEmailListEditView.tbSmtp.SelectedItem = SmtpServers[TargetEmails[index].Smtp];
                targetEmailListEditView.tbSmtp.Text = TargetEmails[index].Smtp;
                targetEmailListEditView.tbName.Text = TargetEmails[index].Name;
                targetEmailListEditView.tbEmail.Text = TargetEmails[index].Email;
                targetEmailListEditView.tbInfo.Text = TargetEmails[index].Info;
                targetEmailListEditView.ShowDialog();
                if (targetEmailListEditView.DialogResult.HasValue && targetEmailListEditView.DialogResult.Value == true)
                {
                    TargetEmails[index].Name = targetEmailListEditView.tbName.Text;
                    TargetEmails[index].Email = targetEmailListEditView.tbEmail.Text;
                    TargetEmails[index].Smtp = targetEmailListEditView.tbSmtp.Text;
                    TargetEmails[index].Port = SmtpServers[targetEmailListEditView.tbSmtp.Text];
                    TargetEmails[index].Info = targetEmailListEditView.tbInfo.Text;
                }
            }
            else
            {
                MessageBox.Show("Адресат не выбран", "Ошибка");
            }
        }
        public void DeleteTargetEmail(int TargetEmailsSelectedIndex)
        {
            if (TargetEmailsSelectedIndex >= 0)
            {
                if (MessageBox.Show("Удалить выбранный адресат?", "Подтверждение", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    TargetEmails.Remove(TargetEmails[(int)TargetEmailsSelectedIndex]);
                }
            }
            else
            {
                MessageBox.Show("Адресат не выбран", "Ошибка");
            }
        }

    }
}
