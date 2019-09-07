using System;
using System.Threading.Tasks;
using DustInTheWind.MedicX.Domain;
using DustInTheWind.MedicX.RequestBusModel;

namespace DustInTheWind.MedicX.Application.SetCurrentItem
{
    public class SetCurrentItemRequest
    {
        public object NewCurrentItem { get; set; }
    }

    public class SetCurrentItemRequestHandler : IRequestHandler<SetCurrentItemRequest>
    {
        private readonly MedicXApplication medicXApplication;

        public SetCurrentItemRequestHandler(MedicXApplication medicXApplication)
        {
            this.medicXApplication = medicXApplication ?? throw new ArgumentNullException(nameof(medicXApplication));
        }

        public Task Handle(SetCurrentItemRequest request)
        {
            return Task.Run(() =>
            {
                MedicXProject currentProject = medicXApplication.CurrentProject;
                currentProject.CurrentItem = request.NewCurrentItem;
            });
        }
    }
}