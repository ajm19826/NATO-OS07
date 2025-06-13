using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms; // Already present, but good to ensure
using PostBookOneMedia.Servers.NATO.Build.DesignerCode.GeminiAgentResponce;

namespace PostBookOneMedia.Servers.NATO.Build.DesignerCode.DesignerCodeUIAgent
{
    // You can put this class in a new file (e.g., GeminiApiClient.cs) or directly in Form2.cs outside the Form2 class.
    public static class GeminiApiClient
    {
        private static readonly HttpClient client = new HttpClient();
        // IMPORTANT: Replace "YOUR_GOOGLE_GEMINI_API_KEY" with your actual Gemini API Key.
        // For a real application, do not hardcode API keys. Use environment variables or a secure configuration system.
        private const string API_KEY = "AIzaSyDcdQwmJDrxOPnt5R8_29oAeke_QJkOiwI";
        private const string API_URL = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=";

        /// <summary>
        /// Generates or modifies C# code based on a prompt and optional existing code.
        /// Separates code blocks from non-code text in the response.
        /// </summary>
        /// <param name="prompt">The natural language prompt describing the desired code or modification.</param>
        /// <param name="existingCode">Optional: Existing C# code to be modified or to provide context.</param>
        /// <returns>A GeminiResponse object containing separated code and non-code text.</returns>
        public static async Task<GeminiResponse> GenerateCodeFromPrompt(string prompt, string existingCode = null)
        {
            GeminiResponse response = new GeminiResponse();

            if (string.IsNullOrWhiteSpace(prompt))
            {
                response.NonCodeText = "Please enter a prompt to generate or modify code.";
                return response;
            }

            try
            {
                string fullPrompt = "Generate C# Windows Forms code based on the following instructions. Provide code blocks within ```csharp``` markers. If there's any non-code explanation, provide it outside these blocks.\n\n";

                if (!string.IsNullOrWhiteSpace(existingCode))
                {
                    fullPrompt += "Existing Code:\n```csharp\n" + existingCode + "\n```\n\n";
                    fullPrompt += "Instructions to Modify/Add to Existing Code: ";
                }
                else
                {
                    fullPrompt += "Instructions to Generate Code: ";
                }
                fullPrompt += prompt;

                // Use Newtonsoft.Json for serialization
                var requestBody = new
                {
                    contents = new[]
                    {
                    new
                    {
                        parts = new[]
                        {
                            new { text = fullPrompt }
                        }
                    }
                }
                };

                string jsonRequestBody = JsonConvert.SerializeObject(requestBody); // <--- Changed
                var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponse = await client.PostAsync(API_URL + API_KEY, content);
                httpResponse.EnsureSuccessStatusCode(); // Throws an exception if the HTTP response status is an error code

                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                // Use Newtonsoft.Json for deserialization
                JObject jsonResponse = JObject.Parse(responseBody); // <--- Changed

                // Navigate through the JSON response using JPath or direct property access
                JToken candidatesToken = jsonResponse["candidates"];
                if (candidatesToken != null && candidatesToken.HasValues)
                {
                    JToken firstCandidate = candidatesToken.First;
                    JToken contentToken = firstCandidate?["content"];
                    JToken partsToken = contentToken?["parts"];

                    if (partsToken != null && partsToken.HasValues)
                    {
                        JToken firstPart = partsToken.First;
                        JToken textToken = firstPart?["text"];

                        if (textToken != null)
                        {
                            string generatedText = textToken.ToString();

                            // Regex to find C# code blocks
                            MatchCollection matches = Regex.Matches(generatedText, @"```csharp\s*([\s\S]*?)\s*```", RegexOptions.IgnoreCase);

                            StringBuilder codeBuilder = new StringBuilder();
                            StringBuilder nonCodeBuilder = new StringBuilder();
                            int lastIndex = 0;

                            foreach (Match match in matches)
                            {
                                // Add text before the current code block as non-code text
                                if (match.Index > lastIndex)
                                {
                                    nonCodeBuilder.Append(generatedText.Substring(lastIndex, match.Index - lastIndex).Trim() + Environment.NewLine);
                                }

                                // Add the extracted code
                                if (match.Groups.Count > 1)
                                {
                                    codeBuilder.Append(match.Groups[1].Value.Trim() + Environment.NewLine);
                                }
                                lastIndex = match.Index + match.Length;
                            }

                            // Add any remaining text after the last code block as non-code text
                            if (lastIndex < generatedText.Length)
                            {
                                nonCodeBuilder.Append(generatedText.Substring(lastIndex).Trim() + Environment.NewLine);
                            }

                            response.Code = codeBuilder.ToString().Trim();
                            response.NonCodeText = nonCodeBuilder.ToString().Trim();

                            // If no code blocks were found, the entire response is considered non-code text
                            if (string.IsNullOrWhiteSpace(response.Code) && !string.IsNullOrWhiteSpace(generatedText))
                            {
                                response.NonCodeText = generatedText.Trim();
                            }

                            return response;
                        }
                    }
                }

                response.NonCodeText = "Failed to parse API response. No generated text found.";
                return response;
            }
            catch (HttpRequestException e)
            {
                response.NonCodeText = $"Network error: {e.Message}. Please check your internet connection and API key.";
                return response;
            }
            catch (JsonSerializationException e) // <--- Changed for Newtonsoft.Json
            {
                response.NonCodeText = $"Error serializing/deserializing JSON: {e.Message}.";
                return response;
            }
            catch (JsonReaderException e) // <--- Changed for Newtonsoft.Json
            {
                response.NonCodeText = $"Error reading JSON: {e.Message}.";
                return response;
            }
            catch (Exception e)
            {
                response.NonCodeText = $"An unexpected error occurred: {e.Message}.";
                return response;
            }
        }
    }
}
