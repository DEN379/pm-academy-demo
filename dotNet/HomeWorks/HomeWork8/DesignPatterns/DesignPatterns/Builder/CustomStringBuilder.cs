using System.Collections.Generic;

namespace DesignPatterns.Builder
{
    public class CustomStringBuilder : ICustomStringBuilder
    {
        private LinkedList<string> list;
        public CustomStringBuilder()
        {
            list = new LinkedList<string>();
        }

        public CustomStringBuilder(string text)
        {
            list = new LinkedList<string>();
            list.AddFirst(text);
        }

        public ICustomStringBuilder Append(string str)
        {
            list.AddLast(str);
            return this;
        }

        public ICustomStringBuilder Append(char ch)
        {
            
            list.AddLast(ch+"");
            return this;
        }

        public ICustomStringBuilder AppendLine()
        {
            list.AddLast("\n");
            return this;
        }

        public ICustomStringBuilder AppendLine(string str)
        {
            list.AddLast(str + "\n");
            return this;
        }

        public ICustomStringBuilder AppendLine(char ch)
        {
            list.AddLast(ch + "\n");
            return this;
        }

        public string Build()
        {
            return string.Join("", list);
        }
    }
}