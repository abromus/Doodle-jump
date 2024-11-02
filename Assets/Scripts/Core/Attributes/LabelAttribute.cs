namespace DoodleJump.Core
{
    [System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class LabelAttribute : BaseAttribute
    {
        private readonly string _name;

        public string Name => _name;

        public LabelAttribute() : this(string.Empty) { }

        public LabelAttribute(string name)
        {
            _name = name;
        }
    }
}
