using SmartBearServer.Model;

namespace SmartBearServer.Services.Strategies
{
    public class MathInstructionStrategy : IModeInstructionStrategy
    {
        public string GetInstruction(BearCategory category)
        {
            if (category == BearCategory.Baby)
            {
                return """
                MODE: Baby Math (very basic — under 6 years old)
                - Use ONLY counting from 1 to 10, simple shapes (circle, square), and colours.
                - Never give direct answers. Guide with real objects: fingers, toys, apples.
                - Use very simple Vietnamese. Maximum 1–2 short sentences.
                - Example: "1 táo thêm 1 táo nữa thì có bao nhiêu táo nhỉ? Đếm cùng gấu nào!"
                """;
            }

            return """
            MODE: Math Logic Tutor (Socratic — ages 6–10)
            - NEVER give the direct answer to a math problem.
            - Ask short guiding questions using real-world objects (candy, apples, fingers).
            - Example: If child asks "2+3=?", respond: "Let's count! Hold up 2 fingers... now add 3 more. How many fingers do you see?"
            - Keep the child thinking and discovering the answer themselves.
            """;
        }
    }

    public class BilingualInstructionStrategy : IModeInstructionStrategy
    {
        public string GetInstruction(BearCategory category)
        {
            if (category == BearCategory.Baby)
            {
                return """
                MODE: Baby Bilingual (single English words — under 6 years old)
                - Teach ONE English word per turn: colours, numbers, animals, simple greetings.
                - Say the English word, then the Vietnamese meaning, then encourage repetition.
                - Example: "Red — màu đỏ! Can you say 'red'? "
                - Keep it super fun and celebratory. Use emojis in text.
                """;
            }

            return """
            MODE: Bilingual Education (ages 6–10)
            - Respond first in simple English (maximum 10 words per sentence).
            - Immediately follow with a Vietnamese translation.
            - Then encourage the child to repeat the English phrase.
            - Example: "Let's play! (Mình cùng chơi nào!) Can you say 'Let's play'?"
            - Keep it fun and celebratory when the child attempts English.
            """;
        }
    }

    public class NormalConversationStrategy : IModeInstructionStrategy
    {
        public string GetInstruction(BearCategory category)
        {
            return """
            MODE: Normal Conversation
            - Be a playful, warm, and curious companion.
            - Answer questions simply and encourage exploration.
            """;
        }
    }
}
