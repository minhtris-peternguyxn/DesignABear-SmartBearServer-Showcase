using System;
using System.Collections.Generic;

namespace SmartBearServer.Services.Strategies
{
    public class ModeInstructionStrategyFactory
    {
        private readonly Dictionary<string, IModeInstructionStrategy> _strategies;

        public ModeInstructionStrategyFactory()
        {
            _strategies = new Dictionary<string, IModeInstructionStrategy>(StringComparer.OrdinalIgnoreCase)
            {
                { "Math", new MathInstructionStrategy() },
                { "Bilingual", new BilingualInstructionStrategy() }
            };
        }

        public IModeInstructionStrategy GetStrategy(string? mode)
        {
            if (string.IsNullOrEmpty(mode) || !_strategies.TryGetValue(mode, out var strategy))
            {
                return new NormalConversationStrategy();
            }
            return strategy;
        }
    }
}
