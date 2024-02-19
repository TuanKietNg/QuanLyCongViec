using HDDT_BE.Models.Core;

namespace HDDT_BE.ViewModels
{
    public class DonViTreeVM
    {

        public DonViTreeVM(string id, string label)
        {
            this.Id = id;
            this.Label = label;
        }
        public DonViTreeVM(DonViTreeVM model)
        {
            this.Id = model.Id;
            this.Label = model.Label;
        }
        public DonViTreeVM(DonVi model)
        {
            this.Id = model.Id;
            this.Label = model.Ten;
            this.Selected = false;
            this.Opened = false;
        }
        public string Id { get; set; }
        public string Label { get; set; }
        

        public bool Selected { get; set; } = false;
        public bool Opened { get; set; } = false;
        
        public List<DonViTreeVM> Children { get; set; }
    }
}