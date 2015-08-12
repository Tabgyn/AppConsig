using System.Collections.Generic;

namespace AppConsig.Web.Base.Entidades
{
    public class TreeViewNode
    {
        public TreeViewNode()
        {
            ChildNodes = new HashSet<TreeViewNode>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
        public virtual long? ParentNodeId { get; set; }
        public ICollection<TreeViewNode> ChildNodes { get; set; }
    }
}