using System;

namespace SandScript.Language.Lexer
{
    public class LexicalException : Exception
    {
        public LexicalException(string message) : base(message) { }
        public LexicalException(string context, string expected, string actual) :
            base($"Required lexical context for '${context}' is not present. Expected: ${expected} - Actual: ${actual}") { }
        public LexicalException(string context, char expected, char actual) :
            base($"Required lexical context for '${context}' is not present. Expected: ${expected} - Actual: ${actual}") { }
    }
}