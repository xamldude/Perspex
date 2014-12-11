using Perspex.Dom;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perspex.Markup
{
    public class MarkupParser
    {
        public static readonly Parser<Child> Property = from name in Parse.LetterOrDigit.Or(Parse.Char('.').Token()).Many().Token()
                                                        from eq in Parse.Char('=').Token()
                                                        from value in Parse.LetterOrDigit.Or(Parse.Char(',').Token()).Or(Parse.Char('*').Token()).Many().Token()
                                                        from semi in Parse.Char(';').Token()
                                                        select new Property { Name = new string(name.ToArray()), Value = new string(value.ToArray()) };

        public static readonly Parser<Child> Node = from name in Parse.Letter.Many().Token()
                                                    from leftBrace in Parse.Char('{').Token()
                                                    from children in Children
                                                    from rightBrace in Parse.Char('}').Token()
                                                    select new Node { Name = new string(name.ToArray()), Children = children };

        public static readonly Parser<IEnumerable<Child>> Children = from children in Node.Or(Property).Many()
                                                                     select children;

        public static readonly Parser<Document> Document = from leading in Parse.WhiteSpace.Many()
                                                           from doc in Node.Select(n => new Document { RootNode = (Node)n }).End()
                                                           select doc;

        public static Document ParseMarkup(string markup)
        {
            return Document.Parse(markup);
        }
    }
}
