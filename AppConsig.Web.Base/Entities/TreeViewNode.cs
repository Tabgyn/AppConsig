using System.Collections.Generic;

namespace AppConsig.Web.Base.Entities
{
    public sealed class TreeViewNode
    {
        public TreeViewNode()
        {
            Children = new HashSet<TreeViewNode>();
        }
        public long Id { get; set; }
        public string Text { get; set; }
        public long? ParentId { get; set; }
        public bool Selected { get; set; }

        public ICollection<TreeViewNode> Children { get; set; }
    }
}