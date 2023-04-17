using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScannerApplication.Helpers;
using ScannerApplication.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ScannerApplication.Api
{
    public class ApiService
    {
        public async Task AddAttachments(Guid realEstateId, List<AttachmentDto> attachments)
        {
            var baseUrl = ConfigurationManager.AppSettings["BaseUrl"];

            var attachmentUploadEndpoint = $"api/attachments/upload";
            var form = new MultipartFormDataContent();


            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };

            foreach (var attachment in attachments)
            {
                var fileContent = new ByteArrayContent(attachment.Picture.ToByteArray());
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

                form.Add(fileContent, "files", "filename.jpeg");
            }
            var token = await GetToken().ConfigureAwait(false);
            // Set Bearer token in request headers
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.PostAsync(attachmentUploadEndpoint, form).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var jsonResult = JsonSerializer.Deserialize<UploadAttachmentResponse>(responseContent);
            if (jsonResult == null)
            {
                throw new Exception("response is null");
            }
            var realEstateAddAttachmentEndpoint = $"api/services/app/realestate/AddRealEstateAttachments";
            var realEstateAddAttachmentInput = new RealEstateAddAttachmentInput
            {
               RealEstateId = realEstateId,
            };
            for (int i = 0; i < attachments.Count; i++)
            {
                realEstateAddAttachmentInput.Attachments.Add(new AttachmentInput()
                {
                    DocumentTypeId = attachments[i].FileType.Id,
                    AttachmentId = jsonResult.Result[i]
                });
            }

            var serializedRequest = JsonSerializer.Serialize(realEstateAddAttachmentInput);
            var content = new StringContent(serializedRequest, Encoding.UTF8, "application/json");
            var addAttachmentResponse = await httpClient.PostAsync(realEstateAddAttachmentEndpoint, content).ConfigureAwait(false);
            addAttachmentResponse.EnsureSuccessStatusCode();
        }

        public async Task<List<AttachmentTypeDto>> GetDocumentTypes()
        {
            var baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            var getDocumentTypesEndpoint = "api/services/app/realestate/GetDocumentTypesTableDropdown";

            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };
            var token = await GetToken().ConfigureAwait(false);
            // Set Bearer token in request headers
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync(getDocumentTypesEndpoint).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var jsonResult = JsonConvert.DeserializeObject<JObject>(responseContent);
            var items = jsonResult?["result"] as JArray;
            if (items == null)
            {
                throw new Exception("response is null");
            }
            return items.ToObject<List<AttachmentTypeDto>>();

        }

        public async Task<string> GetToken()
        {
            var baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            var getTokenEndpoint = "api/TokenAuth/Authenticate";

            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };
            var request = new GetTokenRequest()
            {
                UsernameOrEmailAddress = "admin",
                Password = "123qwe"
            };
            var serializedRequest = JsonSerializer.Serialize(request);
            var content = new StringContent(serializedRequest, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(getTokenEndpoint, content).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var jsonResult = JsonConvert.DeserializeObject<JObject>(responseContent);
            if (jsonResult?["result"]?["accessToken"] == null)
            {
                throw new Exception("response is null");
            }

            return jsonResult["result"]["accessToken"].ToString();
        }
    }

    public class GetTokenRequest
    {
        public string UsernameOrEmailAddress { get; set; }
        public string Password { get; set; }
    }
}