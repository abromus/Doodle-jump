namespace DoodleJump.Game.Settings
{
    internal interface IUiServiceConfig : Core.Settings.IConfig
    {
        public System.Collections.Generic.IReadOnlyList<Services.IUiService> UiServices { get; }
    }
}
