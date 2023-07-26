<PackageReference Include="Microsoft.Identity.Client" Version="4.49.1" />
<PackageReference Include="Newtonsoft.Json" Version="13.0.2" />

string tenantId = "{azure-tenantId}";
string clientId = "{clientId}";
string clientSecret = "{client secret of registered app}";
string subscriptionId = "{azure-subscriptionId}";
string resourceGroupName = "{azure-resourceGroupName}";
string vmName = "{azure-vm-name}";
var app = ConfidentialClientApplicationBuilder.Create(clientId).WithClientSecret(clientSecret).WithTenantId(tenantId).Build();
var authResult = await app.AcquireTokenForClient(new string[] {
    "https://management.azure.com/.default"
}).ExecuteAsync();
// Use the access token to authenticate the API call
var httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);
// API Call to Get VM Details
// https://learn.microsoft.com/en-us/rest/api/compute/virtual-machines/get?tabs=HTTP
var response = await httpClient.GetAsync($ "https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{vmName}?api-version=2022-08-01");
if (response.IsSuccessStatusCode) {
    // Retrieve the response content as a JSON string
    var jsonString = await response.Content.ReadAsStringAsync();
    Console.WriteLine("-------------------Get VM Details--------------------------");
    Console.WriteLine(JObject.Parse(jsonString));
} else {
    throw new Exception($ "Failed to retrieve VM metadata: {response.StatusCode} - {response.ReasonPhrase}");
}
