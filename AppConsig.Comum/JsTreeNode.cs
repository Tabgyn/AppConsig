﻿using System.Collections.Generic;

namespace AppConsig.Comum
{
    public class JsTreeNode
    {
        public long id;
        public string text;
        public state state;
        public List<JsTreeNode> children;
    }

    public class state
    {
        public bool selected;
    }
}