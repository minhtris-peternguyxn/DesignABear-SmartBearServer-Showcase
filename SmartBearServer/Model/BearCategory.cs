namespace SmartBearServer.Model
{
    /// <summary>
    /// Classifies the bear's target age group, which determines AI behaviour and available features.
    /// </summary>
    public enum BearCategory
    {
        /// <summary>
        /// Gấu Bông Bé — for children under 6 years old (pre-school).
        /// Math and English are supported at the most basic level (counting, colours, greetings).
        /// </summary>
        Baby = 1,

        /// <summary>
        /// Gấu Khám Phá — for children aged 6–10 (primary school).
        /// Full Socratic Math tutoring and bilingual sentence-level English are available.
        /// </summary>
        Junior = 2
    }
}
