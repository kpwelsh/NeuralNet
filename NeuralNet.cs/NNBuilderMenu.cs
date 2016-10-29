using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.cs
{
    class NNBuilderMenu : AMenu
    {

        public NNBuilderMenu()
        {
            MenuController.InitializeNN();
            
            Options.Add(
                new Tuple<int, string, CallBack>(
                    1,
                    "Add a layer",
                    () => AddLayer()
                ));
            Options.Add(
                new Tuple<int, string, CallBack>(
                    2,
                    "Edit a layer",
                    () => EditLayer()
                ));
        }

        public override string Header()
        {
            return "Here is the current network\n" + MenuController.DisplayNetSummary();
        }

        private void AddLayer()
        {
            int? val = Text.GetInt("Where would you like to add it " + MenuController.AvailableLayerPositions() + "? ");
            if (val != null)
            {
                Text.ShowMenu(new LayerDesigner());
                MenuController.InsertLayer((int)val);
            }
        }

        private void EditLayer()
        {
            if (MenuController.NumberOfLayers() == 0)
                return;
            int? val = Text.GetInt("Which would you like to edit " + MenuController.AvailableLayerPositions() + "? ");
            if (val != null)
            {
                Text.ShowMenu(new LayerDesigner());
                MenuController.InsertLayer((int)val);
            }
        }
    }
}
