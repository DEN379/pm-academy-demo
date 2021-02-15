using System.Linq;

namespace DesignPatterns.ChainOfResponsibility
{
    public class RemoveNumbersMutator : StringMutator
    {
        public override string Mutate(string str)
        {
            return base.Mutate(new string(str.Where(numb => !int.TryParse(numb.ToString(), out int n)).ToArray()));
        }
    }
}