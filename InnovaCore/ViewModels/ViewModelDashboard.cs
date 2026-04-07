using InnovaCore.Domain.Entities;

namespace InnovaCore.ViewModels
{
    public class ViewModelDashboard
    {
        public VwDashboardQtde VwDashboardQtde { get; set; }    
        public IEnumerable< VwQtdePorSetor> VwQtdePorSetor { get; set; }
    }
}
