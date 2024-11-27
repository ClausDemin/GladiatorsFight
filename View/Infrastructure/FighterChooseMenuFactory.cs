using GladiatorsFight.Model.Enums;
using GladiatorsFight.Service;
using System.Drawing;

namespace GladiatorsFight.View.Infrastructure
{
    public class FighterChooseMenuFactory
    {
        private FighterService _fighterService;
        private Action _clickHandler;

        public FighterChooseMenuFactory(FighterService fighterService, Action clickHandler)
        {
            _fighterService = fighterService;
            _clickHandler = clickHandler;
        }

        public SwitchableMenu CreateSwitchableMenu(Point position)
        {
            var menuItems = new List<MenuItem>();

            foreach (var type in Enum.GetValues<FighterType>())
            {
                if (type != FighterType.None) 
                {
                    menuItems.Add(CreateMenuItem(type));
                }
            }

            var menu = new SwitchableMenu(position, menuItems);

            return menu;
        }

        private MenuItem CreateMenuItem(FighterType type)
        {
            var item = new MenuItem(_fighterService.GetFighterName(type));

            item.OnClick += _clickHandler;

            return item;
        }
    }
}
