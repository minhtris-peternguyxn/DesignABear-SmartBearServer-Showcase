using SmartBearServer.Model;

namespace SmartBearServer.Services.Strategies
{
    public interface IModeInstructionStrategy
    {
        string GetInstruction(BearCategory category);
    }
}
