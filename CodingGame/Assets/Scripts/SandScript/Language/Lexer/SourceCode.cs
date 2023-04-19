namespace SandScript.Language.Lexer
{
    public class SourceCode
    {
        private readonly string _rawSourceCode;

        public SourceCode(string rawSourceCode)
        {
            _rawSourceCode = rawSourceCode;
        }

        public char CharAt(int index)
        {
            if (index < 0)
            {
                return _rawSourceCode[0];
            }

            if (index >= _rawSourceCode.Length)
            {
                return '\0';
            }

            return _rawSourceCode[index];
        }
    }
}