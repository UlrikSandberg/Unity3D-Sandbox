using System.Collections.Generic;

namespace SandScript.Language.Parser
{
    public class SyntaxRoot : SyntaxNode
    {
        public List<SyntaxNode> RootNodes { get; }

        public SyntaxRoot(List<SyntaxNode> rootNodes)
        {
            RootNodes = rootNodes;
        }
    }
}