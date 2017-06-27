using System.Collections.Generic;
using Xamarin.Forms;

namespace PolySketch.UI.Tests.MenuTests
{
    public class MenuListView : ListView
    {
        public MenuListView( MenuListData _data )
        {
            List<MenuItem> data = _data;
            ItemsSource = data;
            VerticalOptions = LayoutOptions.FillAndExpand;
            BackgroundColor = Color.Transparent;
            var cell = new DataTemplate(typeof(ImageCell));
            cell.SetBinding(TextCell.TextProperty , "Text");
            cell.SetBinding(ImageCell.ImageSourceProperty , "IconSource");

            ItemTemplate = cell;
            SelectedItem = data[0];
        }
    }
}