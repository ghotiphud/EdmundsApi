// http://haacked.com/archive/2009/01/14/named-formats-redux.aspx#70485

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Routing;
using System.Web.UI;

namespace EdmundsApi
{
    public static class NamedFormatter
    {
        public static string Format(this string format, object source)
        {
            if (format == null)
                throw new ArgumentNullException("format");

            var result = new StringBuilder(format.Length * 2);
            var expression = new StringBuilder();

            var e = format.GetEnumerator();
            while (e.MoveNext())
            {
                var ch = e.Current;
                if (ch == '{')
                {
                    while (true)
                    {
                        if (!e.MoveNext())
                            throw new FormatException();

                        ch = e.Current;
                        if (ch == '}')
                        {
                            result.Append(OutExpression(source, expression.ToString()));
                            expression.Length = 0;
                            break;
                        }
                        if (ch == '{')
                        {
                            result.Append(ch);
                            break;
                        }
                        expression.Append(ch);
                    }
                }
                else if (ch == '}')
                {
                    if (!e.MoveNext() || e.Current != '}')
                        throw new FormatException();
                    result.Append('}');
                }
                else
                {
                    result.Append(ch);
                }
            }

            return result.ToString();
        }

        private static string OutExpression(object source, string expression)
        {
            string format = "{0}";

            int colonIndex = expression.IndexOf(':');
            if (colonIndex > 0)
            {
                format = "{0:" + expression.Substring(colonIndex + 1) + "}";
                expression = expression.Substring(0, colonIndex);
            }

            try
            {
                if (source is RouteData)
                    source = ((RouteData)(source)).Values;

                var dict = source as IDictionary<string, object>;
                if (dict != null)
                {
                    return dict[expression].ToString();
                }

                return DataBinder.Eval(source, expression, format) ?? "";
            }
            catch (HttpException)
            {
                throw new FormatException();
            }
        }
    }
}
