using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Options;
using static System.Environment;

namespace GraphSample.AI
{
    public class OpenAIService
    {
        private readonly OpenAIClient client;
        public readonly string engine = "text-davinci-003";
        public readonly string endpoint = "https://plannerai.openai.azure.com/";
        public readonly string key = "63448da86da048d49845249803372807";

        public OpenAIService()
        {
            client = new OpenAIClient(new Uri(endpoint), new AzureKeyCredential(key));
        }
    }
}
