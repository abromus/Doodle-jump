namespace DoodleJump.Core.Factories
{
    public interface IUiFactory : IFactory
    {
        public UiFactoryType UiFactoryType { get; }
    }
}
