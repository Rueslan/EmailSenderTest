using EmailSender.Models;
using EmailSender.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender.Services
{
    class TargetEmailsRepository : RepositoryInMemory<TargetEmail>
    {
        protected override void Update(TargetEmail Source, TargetEmail Destination)
        {
            Destination.Email = Source.Email;
            Destination.Name = Source.Name;
            Destination.Smtp = Source.Smtp;
            Destination.Port = Source.Port;
            Destination.Info = Source.Info;
        }
    }
}
