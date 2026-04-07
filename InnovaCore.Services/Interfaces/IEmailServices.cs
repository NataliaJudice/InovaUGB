using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnovaCore.Services.Interfaces
{
    public interface IEmailServices
    {
        Task SendStatusUpdateEmailAsync(string userEmail, string taskTitle, string status);
        Task SendStatusUpdateEmailAsyncSetores(string setorEmail, string taskTitle, string descricaoTarefa, string status);
    }
}
