using REST.Shared.Models.Contracts;

namespace REST.Shared.Controller.Contracts
{
    public interface IConfigController
    {
        IConfig Config { get; set; }

        void WriteFile();
        void ReadFile();
    }
}
