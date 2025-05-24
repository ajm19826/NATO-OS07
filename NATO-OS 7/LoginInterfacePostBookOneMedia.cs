using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Identity.Client; // For Microsoft Authentication
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Diagnostics; // Add this for debugging

namespace PostBookOneMedia.AuthLoginForm.Scope.FormTemplate.three
{
  /*  public partial class AuthForm : Form
    {
        // OAuth 2.0 Configuration
        private const string GoogleClientId = "YOUR_GOOGLE_CLIENT_ID";
        private const string GoogleClientSecret = "YOUR_GOOGLE_CLIENT_SECRET";
        private const string GoogleRedirectUri = "http://localhost";
        private const string GoogleAuthorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
        private const string GoogleTokenEndpoint = "https://oauth2.googleapis.com/token";
        private const string GoogleUserInfoEndpoint = "https://www.googleapis.com/oauth2/v3/userinfo";
        private const string GoogleScope = "openid profile email";

        private const string GitHubClientId = "YOUR_GITHUB_CLIENT_ID";
        private const string GitHubClientSecret = "YOUR_GITHUB_CLIENT_SECRET";
        private const string GitHubRedirectUri = "http://localhost";
        private const string GitHubAuthorizationEndpoint = "https://github.com/login/oauth/authorize";
        private const string GitHubTokenEndpoint = "https://github.com/login/oauth/access_token";
        private const string GitHubUserInfoEndpoint = "https://api.github.com/user";
        private const string GitHubScope = "user:email";

        private const string MicrosoftClientId = "YOUR_MICROSOFT_CLIENT_ID";
        private const string MicrosoftTenantId = "common";
        private const string MicrosoftRedirectUri = "http://localhost";
        private const string MicrosoftAuthorizationEndpoint = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize"; // Corrected
        private const string MicrosoftTokenEndpoint = "https://login.microsoftonline.com/common/oauth2/v2.0/token";     // Corrected
        private const string MicrosoftScope = "openid profile email User.Read";

        private const string SuccessUrl = "http://localhost";
        private const string ErrorUrl = "http://localhost/error";

        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _appTitle = "Multi-Factor Authentication";

        private static IPublicClientApplication _msPublicClientApp;
        private WebBrowser authBrowser; // Declare authBrowser at the class level

        public AuthForm()
        {
            InitializeComponent();
            InitializeHttpClient();
            InitializeMicrosoftAuth();
            InitializeWebBrowser();
        }

        private void InitializeComponent()
        {
            // This method was originally missing. It's where you create your form's controls.
            // In a real WinForms app, the Visual Studio designer generates this code.
            // Since we're creating it manually, we have to do it ourselves.
            this.GoogleAuthButton = new Button();
            this.GitHubAuthButton = new Button();
            this.MicrosoftAuthButton = new Button();
            this.authBrowser = new WebBrowser(); //instantiate the authBrowser
            this.SuspendLayout();
            //
            // GoogleAuthButton
            //
            this.GoogleAuthButton.Location = new System.Drawing.Point(12, 12);
            this.GoogleAuthButton.Name = "GoogleAuthButton";
            this.GoogleAuthButton.Size = new System.Drawing.Size(120, 23);
            this.GoogleAuthButton.TabIndex = 0;
            this.GoogleAuthButton.Text = "Login with Google";
            this.GoogleAuthButton.UseVisualStyleBackColor = true;
            this.GoogleAuthButton.Click += new System.EventHandler(this.GoogleAuthButton_Click);
            //
            // GitHubAuthButton
            //
            this.GitHubAuthButton.Location = new System.Drawing.Point(138, 12);
            this.GitHubAuthButton.Name = "GitHubAuthButton";
            this.GitHubAuthButton.Size = new System.Drawing.Size(120, 23);
            this.GitHubAuthButton.TabIndex = 1;
            this.GitHubAuthButton.Text = "Login with GitHub";
            this.GitHubAuthButton.UseVisualStyleBackColor = true;
            this.GitHubAuthButton.Click += new System.EventHandler(this.GitHubAuthButton_Click);
            //
            // MicrosoftAuthButton
            //
            this.MicrosoftAuthButton.Location = new System.Drawing.Point(264, 12);
            this.MicrosoftAuthButton.Name = "MicrosoftAuthButton";
            this.MicrosoftAuthButton.Size = new System.Drawing.Size(120, 23);
            this.MicrosoftAuthButton.TabIndex = 2;
            this.MicrosoftAuthButton.Text = "Login with Microsoft";
            this.MicrosoftAuthButton.UseVisualStyleBackColor = true;
            this.MicrosoftAuthButton.Click += new System.EventHandler(this.MicrosoftAuthButton_Click);
            //
            // authBrowser
            //
            this.authBrowser.Location = new System.Drawing.Point(12, 41);
            this.authBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.authBrowser.Name = "authBrowser";
            this.authBrowser.Size = new System.Drawing.Size(760, 400); // Set appropriate size
            this.authBrowser.TabIndex = 3;
            this.authBrowser.Visible = false;
            this.authBrowser.Navigated += new WebBrowserNavigatedEventHandler(this.authBrowser_Navigated);
            //
            // AuthForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450); // Set appropriate size
            this.Controls.Add(this.GoogleAuthButton);
            this.Controls.Add(this.GitHubAuthButton);
            this.Controls.Add(this.MicrosoftAuthButton);
            this.Controls.Add(this.authBrowser);
            this.Name = "AuthForm";
            this.Text = "Multi-Factor Authentication";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        private void InitializeHttpClient()
        {
            _httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("MyApp/1.0");
        }

        private void InitializeMicrosoftAuth()
        {
            try
            {
                _msPublicClientApp = PublicClientApplicationBuilder.Create(MicrosoftClientId)
                    .WithAuthority(MicrosoftAuthorizationEndpoint)
                    .WithRedirectUri(MicrosoftRedirectUri)
                    .Build();
            }
            catch (Exception ex)
            {
                LogError($"Error initializing Microsoft Authentication: {ex.Message}");
                MessageBox.Show($"Failed to initialize Microsoft Authentication. Check the configuration and try again. Error: {ex.Message}", _appTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Consider disabling the Microsoft Login button here to prevent further errors
                MicrosoftAuthButton.Enabled = false;
            }
        }

        private void InitializeWebBrowser()
        {
            authBrowser.Visible = false;
            authBrowser.Dock = DockStyle.Fill;
            Controls.Add(authBrowser);
            authBrowser.BringToFront();
        }

        private async void GoogleAuthButton_Click(object sender, EventArgs e)
        {
            await AuthenticateWithProvider("Google");
        }

        private async void GitHubAuthButton_Click(object sender, EventArgs e)
        {
            await AuthenticateWithProvider("GitHub");
        }

        private async void MicrosoftAuthButton_Click(object sender, EventArgs e)
        {
            await AuthenticateWithProvider("Microsoft");
        }

        private async Task AuthenticateWithProvider(string provider)
        {
            try
            {
                authBrowser.Visible = true;
                string authorizationUrl = "";
                string tokenEndpoint = "";
                string userInfoEndpoint = "";
                string clientId = "";
                string clientSecret = "";
                string redirectUri = "";
                string scope = "";

                switch (provider)
                {
                    case "Google":
                        clientId = GoogleClientId;
                        clientSecret = GoogleClientSecret;
                        redirectUri = GoogleRedirectUri;
                        scope = GoogleScope;
                        authorizationUrl = $"{GoogleAuthorizationEndpoint}?client_id={clientId}&redirect_uri={redirectUri}&response_type=code&scope={scope}";
                        tokenEndpoint = GoogleTokenEndpoint;
                        userInfoEndpoint = GoogleUserInfoEndpoint;
                        break;
                    case "GitHub":
                        clientId = GitHubClientId;
                        clientSecret = GitHubClientSecret;
                        redirectUri = GitHubRedirectUri;
                        scope = GitHubScope;
                        authorizationUrl = $"{GitHubAuthorizationEndpoint}?client_id={clientId}&redirect_uri={redirectUri}&scope={scope}";
                        tokenEndpoint = GitHubTokenEndpoint;
                        userInfoEndpoint = GitHubUserInfoEndpoint;
                        break;
                    case "Microsoft":
                        clientId = MicrosoftClientId;
                        redirectUri = MicrosoftRedirectUri;
                        scope = MicrosoftScope;
                        try
                        {
                            UriBuilder uriBuilder = new UriBuilder(MicrosoftAuthorizationEndpoint);
                            var queryParams = new Dictionary<string, string>
                            {
                                { "client_id", clientId },
                                { "redirect_uri", redirectUri },
                                { "response_type", "code" },
                                { "scope", scope },
                            };

                            string query = string.Join("&", queryParams.Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));
                            uriBuilder.Query = query;
                            authorizationUrl = uriBuilder.ToString();
                        }
                        catch (Exception ex)
                        {
                            LogError($"Error building Microsoft auth URL: {ex.Message}");
                            MessageBox.Show($"Error building Microsoft auth URL: {ex.Message}", _appTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        tokenEndpoint = MicrosoftTokenEndpoint;
                        userInfoEndpoint = "https://graph.microsoft.com/v1.0/me";
                        break;
                    default:
                        MessageBox.Show("Provider not supported.", _appTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                authBrowser.Navigate(authorizationUrl);

                authBrowser.Tag = new AuthInfo
                {
                    Provider = provider,
                    ClientId = clientId,
                    ClientSecret = clientSecret,
                    RedirectUri = redirectUri,
                    TokenEndpoint = tokenEndpoint,
                    UserInfoEndpoint = userInfoEndpoint,
                    Scope = scope
                };
            }
            catch (Exception ex)
            {
                LogError($"Error during authentication: {ex.Message}");
                MessageBox.Show($"Error during authentication: {ex.Message}", _appTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void authBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            LogInfo($"authBrowser_Navigated: URL = {e.Url}");
            if (e.Url.ToString().StartsWith(SuccessUrl) || e.Url.ToString().StartsWith(ErrorUrl))
            {
                await HandleOAuthCallback(e.Url.ToString());
            }
        }

        private async Task HandleOAuthCallback(string url)
        {
            authBrowser.Visible = false;
            string code = null;
            string error = null;

            if (url.StartsWith(ErrorUrl))
            {
                error = Uri.UnescapeDataString(url.Substring(url.IndexOf("error=") + 6));
                LogError($"OAuth Callback Error: {error}");
                MessageBox.Show($"Authentication Error: {error}", _appTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Uri uri = new Uri(url);
            var queryParams = HttpUtility.ParseQueryString(uri.Query);
            code = queryParams["code"];
            error = queryParams["error"];

            if (error != null)
            {
                LogError($"OAuth Callback Error: {error}");
                MessageBox.Show($"Authentication error: {error}", _appTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (code == null)
            {
                LogError("OAuth Callback Error: Authorization code is missing.");
                MessageBox.Show("Authorization code is missing.", _appTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AuthInfo authInfo = authBrowser.Tag as AuthInfo;
            if (authInfo == null)
            {
                LogError("OAuth Callback Error: Authentication information is missing.");
                MessageBox.Show("Authentication information is missing.  Please try again.", _appTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string accessToken = await GetAccessToken(code, authInfo.Provider, authInfo.ClientId, authInfo.ClientSecret, authInfo.RedirectUri, authInfo.TokenEndpoint);
                if (!string.IsNullOrEmpty(accessToken))
                {
                    await GetUserInfoAndDisplay(accessToken, authInfo.Provider, authInfo.UserInfoEndpoint);
                }
            }
            catch (Exception ex)
            {
                LogError($"Error during token exchange or user info retrieval: {ex.Message}");
                MessageBox.Show($"Error during token exchange or user info retrieval: {ex.Message}", _appTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<string> GetAccessToken(string code, string provider, string clientId, string clientSecret, string redirectUri, string tokenEndpoint)
        {
            try
            {
                string tokenRequestBody = "";
                StringContent content = null;
                HttpResponseMessage tokenResponse = null;

                switch (provider)
                {
                    case "Google":
                        tokenRequestBody = $"code={code}&client_id={clientId}&client_secret={clientSecret}&redirect_uri={redirectUri}&grant_type=authorization_code";
                        content = new StringContent(tokenRequestBody, Encoding.UTF8, "application/x-www-form-urlencoded");
                        tokenResponse = await _httpClient.PostAsync(tokenEndpoint, content);
                        break;
                    case "GitHub":
                        tokenRequestBody = $"code={code}&client_id={clientId}&client_secret={clientSecret}&redirect_uri={redirectUri}&grant_type=authorization_code";
                        content = new StringContent(tokenRequestBody, Encoding.UTF8, "application/x-www-form-urlencoded");
                        tokenResponse = await _httpClient.PostAsync(tokenEndpoint, content);
                        break;
                    case "Microsoft":
                        try
                        {
                            // Corrected code: Use the appropriate MSAL method for exchanging the code for a token.
                            AuthenticationResult authResult = await _msPublicClientApp.AcquireTokenByAuthorizationCode(new[] { "User.Read" }, code)
                                .WithRedirectUri(MicrosoftRedirectUri) // Add this line
                                .ExecuteAsync();
                            LogInfo($"Microsoft Access Token acquired successfully.");
                            return authResult.AccessToken;
                        }
                        catch (MsalException ex)
                        {
                            LogError($"MSAL Error acquiring token: {ex.Message}  ErrorCode: {ex.ErrorCode}");
                            MessageBox.Show($"MSAL Error acquiring token: {ex.Message}  ErrorCode: {ex.ErrorCode}", _appTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return null;
                        }
                    default:
                        LogError($"Provider not supported for token exchange: {provider}");
                        MessageBox.Show("Provider not supported for token exchange.", _appTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                }

                if (tokenResponse != null && tokenResponse.IsSuccessStatusCode)
                {
                    string responseContent = await tokenResponse.Content.ReadAsStringAsync();
                    LogInfo($"Token response ({(int)tokenResponse.StatusCode}): {responseContent}");
                    dynamic tokenJson = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);
                    return tokenJson.access_token;
                }
                else if (tokenResponse != null)
                {
                    string errorContent = await tokenResponse.Content.ReadAsStringAsync();
                    LogError($"Error getting token: {tokenResponse.StatusCode} - {errorContent}");
                    MessageBox.Show($"Error getting token: {tokenResponse.StatusCode} - {errorContent}", _appTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                else
                {
                    LogError("Error getting token: Empty or null response.");
                    MessageBox.Show("Error getting token: Empty or null response.", _appTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogError($"Error in GetAccessToken: {ex.Message}");
                MessageBox.Show($"Error in GetAccessToken: {ex.Message}", _appTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private async Task GetUserInfoAndDisplay(string accessToken, string provider, string userInfoEndpoint)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                HttpResponseMessage userInfoResponse = await _httpClient.GetAsync(userInfoEndpoint);

                if (userInfoResponse.IsSuccessStatusCode)
                {
                    string userInfoContent = await userInfoResponse.Content.ReadAsStringAsync();
                    LogInfo($"User Info Response: {userInfoContent}");
                    dynamic userInfoJson = Newtonsoft.Json.JsonConvert.DeserializeObject(userInfoContent);

                    string message = $"Provider: {provider}\n";
                    switch (provider)
                    {
                        case "Google":
                            message += $"User ID: {userInfoJson.id}\n";
                            message += $"Name: {userInfoJson.name}\n";
                            message += $"Email: {userInfoJson.email}\n";
                            break;
                        case "GitHub":
                            message += $"User ID: {userInfoJson.id}\n";
                            message += $"Login: {userInfoJson.login}\n";
                            message += $"Email: {userInfoJson.email}\n";
                            message += $"Name: {userInfoJson.name}\n";
                            break;
                        case "Microsoft":
                            message += $"User ID: {userInfoJson.id}\n";
                            message += $"Display Name: {userInfoJson.displayName}\n";
                            message += $"Email: {userInfoJson.mail ?? userInfoJson.userPrincipalName}\n";
                            break;
                    }
                    MessageBox.Show(message, $"{provider} User Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string errorContent = await userInfoResponse.Content.ReadAsStringAsync();
                    LogError($"Error getting user info: {userInfoResponse.StatusCode} - {errorContent}");
                    MessageBox.Show($"Error getting user info: {userInfoResponse.StatusCode} - {errorContent}", _appTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                LogError($"Error in GetUserInfoAndDisplay: {ex.Message}");
                MessageBox.Show($"Error in GetUserInfoAndDisplay: {ex.Message}", _appTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            authBrowser.Visible = false;
            authBrowser.Dock = DockStyle.Fill;
            this.Controls.Add(authBrowser);
            authBrowser.BringToFront();
        }

        private Button GoogleAuthButton;
        private Button GitHubAuthButton;
        private Button MicrosoftAuthButton;

        private void LogError(string message)
        {
            // Use Debug.WriteLine or your preferred logging method
            Debug.WriteLine($"ERROR: {message}");
        }

        private void LogInfo(string message)
        {
            // Use Debug.WriteLine or your preferred logging method
            Debug.WriteLine($"INFO: {message}");
        }
    }

    internal class AuthInfo
    {
        public string Provider { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUri { get; set; }
        public string TokenEndpoint { get; set; }
        public string UserInfoEndpoint { get; set; }
        public string Scope { get; set; }
    }
    */
}
