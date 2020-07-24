using EmailSender.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender.Models
{
    class TargetEmail : INotifyPropertyChanged, IEntity
    {
        private string name { get; set; }
        private string smtp { get; set; }
        private string email { get; set; }
        private string info { get; set; }
        private int port { get; set; }
        public int Id { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Name)));
            }
        }

        public string Smtp
        {
            get { return smtp; }
            set
            {
                smtp = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Smtp)));
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Email)));
            }
        }

        public string Info
        {
            get { return info; }
            set
            {
                info = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Info)));
            }
        }

        public int Port
        {
            get { return port; }
            set
            {
                port = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Port)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
