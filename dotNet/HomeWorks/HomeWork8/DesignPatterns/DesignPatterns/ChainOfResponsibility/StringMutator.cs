using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.ChainOfResponsibility
{
    public class StringMutator : IStringMutator
    {
        private IStringMutator stringMutator;
        public virtual string Mutate(string str)
        {
            if (this.stringMutator != null)
            {
                return this.stringMutator.Mutate(str);
            }
            else
            {
                return str;
            }
        }

        public virtual IStringMutator SetNext(IStringMutator next)
        {
            this.stringMutator = next;
            return next;
        }
    }
}
