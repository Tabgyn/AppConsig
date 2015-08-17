using System.Collections.Generic;

namespace AppConsig.Web.Base.Entidades
{
    public sealed class TreeViewNode
    {
        public TreeViewNode()
        {
            Filhos = new HashSet<TreeViewNode>();
        }
        public long Id { get; set; }
        public string Texto { get; set; }
        public long? ParenteId { get; set; }
        public bool Selecionado { get; set; }

        public ICollection<TreeViewNode> Filhos { get; set; }
    }
}