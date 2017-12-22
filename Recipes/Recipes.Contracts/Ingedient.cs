namespace Recipes.Contracts
{
    public class Ingedient
    {
        public double Amount { get; }
        public Unit Unit { get; }
        public string Name { get; }
        public Ingedient(string name, double amount, Unit unit = Unit.none)
        {
            Amount = amount;
            Unit = unit;
            Name = name;
        }

        public override string ToString()
        {
            if (Unit == Unit.none)
            {
                if (Amount == 0)
                {
                    return $"{Name}";
                }
                return $"{Amount} {Name}";
            }
            return $"{Amount}{Unit.ToString()} {Name}";
        }
    }
}
