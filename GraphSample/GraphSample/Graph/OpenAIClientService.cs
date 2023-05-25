using AzureOpenAIClient.Http;
namespace GraphSample.Data
{
    public class OpenAIClientService
    {
        private readonly OpenAIClient _openAiClient;
        public OpenAIClientService(OpenAIClient client)
        {
            _openAiClient = client;
        }
        public async Task<CompletionResponse?> GetTextCompletionResponse(
            string input, int maxTokens)
        {
            var completionRequest = new CompletionRequest()
            {
                Prompt = input,
                MaxTokens = maxTokens
            };
            
            return await _openAiClient
                .GetTextCompletionResponseAsync(completionRequest);
        }
    }
}