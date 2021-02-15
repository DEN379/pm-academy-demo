namespace DesignPatterns.ChainOfResponsibility
{
    public class ToUpperMutator : StringMutator
    {
        public override string Mutate(string str)
        {
            return base.Mutate(str.ToUpper());
        }
    }
}