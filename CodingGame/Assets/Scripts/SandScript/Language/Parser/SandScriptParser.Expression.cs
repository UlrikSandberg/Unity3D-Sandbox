using System;
using System.Collections.Generic;
using System.Linq;
using SandScript.Language.Lexer;
using SandScript.Language.Syntax.Expressions;
using SandScript.Language.Syntax.Expressions.Enums;

namespace SandScript.Language.Parser
{
    public partial class SandScriptParser
    {
        private Expression ParseExpression()
        {
            return ParseAdditivePrecedence();
        }

        private Expression ParseAdditivePrecedence()
        {
            var left = ParseMultiplicativePrecedence();

            if (Current == TokenType.Addition || Current == TokenType.Subtraction)
            {
                var op = ConsumeToken(ParseOperator);
                left = new ArithmeticExpression(left, ParseAdditivePrecedence(), op);
            }
            
            return left;
        }

        private Expression ParseMultiplicativePrecedence()
        {
            var left = ParseTerminalExpression();

            if (Current == TokenType.Multiplication || Current == TokenType.Divition || Current == TokenType.Modulu)
            {
                var op = ConsumeToken(ParseOperator);
                left = new ArithmeticExpression(left, ParseMultiplicativePrecedence(), op);
            }
            
            return left;
        }

        private Expression ParseTerminalExpression()
        {
            if (Current.TokenCategory == TokenCategory.Literal)
            {
                if (Current.TokenType == TokenType.StringLiteral)
                {
                    throw new NotImplementedException("String literal not implemented");
                }

                return ConsumeToken(ParseNumber);
            }

            if (Current.TokenType == TokenType.Identifier)
            {
                if (Next == TokenType.Dot)
                {
                    return ParseObjectAccessExpression();
                }

                if (Next == TokenType.LeftParen)
                {
                    return ParseMethodCallExpression();
                }
                
                return ConsumeToken(ParseIdentifier);
            }

            throw new SyntaxException("Unexpected token scanning for expression terminal");
        }

        private Expression ParseObjectAccessExpression()
        {
            var objectAccessChain = new Stack<IdentifierExpression>();
            objectAccessChain.Push(ConsumeToken(ParseIdentifier) as IdentifierExpression);

            while (Current == TokenType.Dot)
            {
                ConsumeToken(TokenType.Dot);
                objectAccessChain.Push(ConsumeToken(ParseIdentifier) as IdentifierExpression);
            }

            if (Current == TokenType.LeftParen)
            {
                return ParseMethodCallExpression(objectAccessChain);
            }

            return new ObjectAccessExpression(new Stack<IdentifierExpression>(objectAccessChain));
        }

        private Expression ParseMethodCallExpression(Stack<IdentifierExpression> objectAccessChain = null)
        {
            if (objectAccessChain == null)
            {
                if (Current != TokenType.Identifier)
                    throw new SyntaxException("Unexpected token parsing method call. Expected identifier.");

                if (Next != TokenType.LeftParen)
                    throw new SyntaxException("Unexpected token when parsing method call. Expected open parenthesis");
                
                var methodName = ConsumeToken(ParseIdentifier);

                ConsumeToken(TokenType.LeftParen);
                var args = ParseArgs();
                ConsumeToken(TokenType.RightParen);
                
                return new MethodCallExpression(methodName, null, args.ToArray());
            }
            else
            {
                if (objectAccessChain.Count < 2)
                    throw new SyntaxException("Unexpected objectAccessChain length encountered when parsing MethodCall. An object access chain should at least consist of two elements.");
                
                var methodName = objectAccessChain.Pop();
                ConsumeToken(TokenType.LeftParen);
                var args = ParseArgs();
                ConsumeToken(TokenType.RightParen);
                return new MethodCallExpression(methodName, new ObjectAccessExpression(objectAccessChain), args.ToArray());   
            }
        }

        private List<Expression> ParseArgs()
        {
            var args = new List<Expression>();

            while (Current != TokenType.RightParen)
            {
                if (Current == TokenType.Comma)
                    ConsumeToken(TokenType.Comma);
                
                args.Add(ParseExpression());
            }

            return args;
        }

        private Expression ParseIdentifier() => new IdentifierExpression(Current.Value);
        
        private Expression ParseNumber() => Current.TokenType switch
        {
            TokenType.FloatingPointLiteral => new ConstantExpression(Current.Value, ConstantType.Float),
            TokenType.IntegerLiteral => new ConstantExpression(Current.Value, ConstantType.Integer),
            _ => throw new SyntaxException($"Unexpected token {Current.TokenType} while parsing number")
        };

        private ArithmeticOperator ParseOperator() => Current.TokenType switch
        {
            TokenType.Addition => ArithmeticOperator.Plus,
            TokenType.Subtraction => ArithmeticOperator.Minus,
            TokenType.Multiplication => ArithmeticOperator.Multiply,
            TokenType.Divition => ArithmeticOperator.Divide,
            TokenType.Modulu => ArithmeticOperator.Modulo,
            _ => throw new SyntaxException($"Unexpected token {Current.TokenType} while parsing operator")
        };
    }
}