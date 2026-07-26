using System.ClientModel;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using OpenAI;

// 1. Secrets
var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

string googleApiKey = config["Gemini:ApiKey"] 
    ?? throw new InvalidOperationException("Gemini:ApiKey is missing!");

// 2. Client Setup
var clientOptions = new OpenAIClientOptions
{
    Endpoint = new Uri("https://generativelanguage.googleapis.com/v1beta/openai/")
};

var openAIClient = new OpenAIClient(new ApiKeyCredential(googleApiKey), clientOptions);

IChatClient chatClient = openAIClient.GetChatClient("gemini-1.5-flash").AsIChatClient();

// 4. Wrap & Run
AIAgent agent = chatClient.AsAIAgent();

var response = await agent.RunAsync("What is the capital of France?");

Console.WriteLine(response);