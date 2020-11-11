﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Markdown.Parsers
{
    public class BoldParser : ITokenParser
    {
        public Token ParseToken(IEnumerable<string> text, int position)
        {
            var tokenValue = new StringBuilder();
            var parserOperator = new ParserOperator();
            var isIntoToken = false;
            var offset = 0;
            foreach (var part in text)
            {
                if (TokenReader.IsFormattingString(part))
                {
                    if (isIntoToken)
                    {
                        parserOperator.Position = offset;
                        parserOperator.AddTokenPart(part);
                    }
                    isIntoToken = true && !isIntoToken;
                }
                else if (!isIntoToken)
                {
                    tokenValue.Append(part);
                    offset += part.Length;
                }
                if (isIntoToken)
                    parserOperator.AddTokenPart(part);
            }
            var nestedTokens = parserOperator.GetTokens();
            var token = new Token(position, tokenValue.ToString(), TokenType.Bold);
            token.SetNestedTokens(nestedTokens);
            return token;
        }
    }
}
