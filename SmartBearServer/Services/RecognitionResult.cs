namespace SmartBearServer.Services
{
    public class RecognitionResult
    {
        public string Transcript { get; set; }
        public string LanguageCode { get; set; }
        public string Format { get; set; }
        public int Channels { get; set; }
        public int SampleRate { get; set; }
    }
}
