namespace DotnetDocViewer.DocumentationParser
{
    public struct MemberElement
    {
        public string Name;
        public string Summary;

        public MemberElement(string name, string? summary)
        {
            Name = name;
            Summary = summary ?? "<NULL>";
        }
    }
}
