using CommunityToolkit.Mvvm.ComponentModel;
using ControlStore.UI.Demo.UI.Menu_Main;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ControlStore.UI.Demo.MVVM.Menu
{
    public partial class MenuMainNavegation : ObservableObject
    {
        public ObservableCollection<ItemOption> OptionsMenu { get; set; }

        [ObservableProperty]
        private ItemOption _itemSelected;

        [ObservableProperty]
        private object _contentPage;

        public MenuMainNavegation()
        {
            var Options = new ObservableCollection<ItemOption>
            { 
                new ItemOption("Temas"),
                new ItemOption("Tiprografia"),
                new ItemOption("Botones"),
                new ItemOption("Targetas de Información"),
                new ItemOption("Iconos de Control Store")

            };
            this.OptionsMenu = Options;

            ItemSelected = this.OptionsMenu.FirstOrDefault();

        }

        partial void OnItemSelectedChanged(ItemOption value)
        {
            var title = value.Title;
            switch(title)
            {
                case "Temas":
                    ContentPage = Activator.CreateInstance(typeof(ThemesView));
                    break;
                case "Tiprografia":
                    ContentPage = Activator.CreateInstance(typeof(TypographyStyles));
                    break;
                case "Botones":
                    ContentPage = Activator.CreateInstance(typeof(StylesButtons));
                    break;
                case "Targetas de Información":
                    ContentPage = Activator.CreateInstance(typeof(BusinessCardControl));
                    break;
                case "Iconos de Control Store":
                    ContentPage = Activator.CreateInstance(typeof(IconControlStore));
                    break;
            }
        }
    }


    public class ItemOption
    {
        public ItemOption(string title)
        {
            this.title = title;
        }

        private string title;
        public string Title { get => title; }
    }

   
}
