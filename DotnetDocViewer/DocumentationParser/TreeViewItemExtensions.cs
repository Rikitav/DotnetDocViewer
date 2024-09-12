using System.Windows.Controls;

namespace DotnetDocViewer.DocumentationParser
{
    public static class TreeViewItemExtensions
    {
        public static TreeViewItem FirstOrCreateItem(this TreeViewItem thisItem, string Name)
        {
            foreach (TreeViewItem item in thisItem.Items)
            {
                if (item.Header.Equals(Name))
                    return item;
            }

            TreeViewItem newItem = new TreeViewItem()
            {
                Header = Name
            };

            thisItem.Items.Add(newItem);
            return newItem;
        }
    }
}
