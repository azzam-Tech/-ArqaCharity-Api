namespace ArqaCharity.Core.Entities
{
    public class MembershipType
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public MembershipType(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}

