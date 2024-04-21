using Application.Common.Behaviors;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Base
{
    public interface IHelper
    {
        string Encryptor(string strText);
        string DecryptFromBase64String(string stringToDecrypt);
        string GetRandomPassword(int length);
        Task<bool> SendEmail(string toEmail, string body, string subject, string from, string mailTitle);
        string FormatString<T>(string text, T values);
        string CurrencyFormat(decimal amount);
        string GetTemplate(Statuses.TemplatesName templateName);
        string GetResponseMessage(string key);
        bool DeleteAllAttachments(DirectoryDetails directoryDetails);
        string AddInAttachmentStore(IFormFile Attachment, DirectoryDetails directoryDetail);
        string AddInAttachmentStore(IFormFileCollection Attachments, DirectoryDetails directoryDetail);
    }
}
