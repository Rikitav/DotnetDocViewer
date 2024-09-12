using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Xml;

namespace DotnetDocViewer.DocumentationParser
{
    public static class DotnetDocumentation
    {
        public static List<TreeViewItem> FromXmlFile(string FileName)
        {
            // Loading doc file
            XmlDocument document = new XmlDocument();
            document.Load(FileName);

            // Creating instance
            return FromXmlDocument(document);
        }

        public static List<TreeViewItem> FromXmlDocument(XmlDocument xmlDocument)
        {
            // Checking doc node
            XmlNode? DocNode = xmlDocument.LastChild;
            if (DocNode == null)
                throw new ArgumentNullException();

            if (!DocNode.Name.Equals("doc", StringComparison.CurrentCultureIgnoreCase))
                throw new InvalidDataException();

            // Reading Assembly define
            XmlNode? AssemblyNode = DocNode.FirstChild?.FirstChild;
            if (AssemblyNode == null)
                throw new ArgumentNullException();

            // Reading members nodes
            XmlNode? membersListNode = DocNode.LastChild;
            if (membersListNode == null)
                throw new ArgumentNullException();

            //
            TreeViewItem rootItem = new TreeViewItem();
            rootItem.Header = "Assembly";

            //
            List<TreeViewItem> list = new List<TreeViewItem>();
            foreach (XmlNode memberNode in membersListNode)
            {
                string memberName = memberNode.Attributes.Item(0).Value.Substring(2);
                string[] NamespaceSplit = memberName.Split('.');

                TreeViewItem? currentItem = list.FirstOrDefault(x => x.Header.Equals(NamespaceSplit[0]));
                if (currentItem == null)
                {
                    currentItem = new TreeViewItem();
                    currentItem.Header = NamespaceSplit[0];
                    list.Add(currentItem);
                }

                for (int i = 1; i < NamespaceSplit.Length; i++)
                {
                    currentItem = currentItem.FirstOrCreateItem(NamespaceSplit[i]);
                }
            }

            return list;
        }

        private static void ParseMemberElements(XmlNode memberNode, TreeViewItem treeViewItem)
        {
            foreach (XmlNode memberElementNode in memberNode.ChildNodes)
            {

            }
        }
    }
}
