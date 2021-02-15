using System.Linq;

namespace DesignPatterns.ChainOfResponsibility
{
    public class InvertMutator : StringMutator
    {
        public override string Mutate(string str)
        {
            return base.Mutate(new string(str.Reverse().ToArray()));
        }
    }
}