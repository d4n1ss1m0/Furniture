using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furniture.ViewModels
{
    public class TableInterfaceViewModel
    {
        private string selectedItem;
        public TableInterfaceViewModel()
        {

        }

        public void Table()
        {

            
        }

        public string SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                Table();
            }
        }


    }
}
