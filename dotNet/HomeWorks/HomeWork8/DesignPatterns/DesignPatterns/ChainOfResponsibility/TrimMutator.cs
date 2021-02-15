namespace DesignPatterns.ChainOfResponsibility
{
    public class TrimMutator : StringMutator
    {
        public override string Mutate(string str)
        {
            return base.Mutate(str.Trim());
        }
    }
}