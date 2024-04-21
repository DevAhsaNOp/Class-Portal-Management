using Application.Common.Behaviors;
using Application.Features.GenericFeatures;
using Application.Interfaces.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using static Application.Common.Behaviors.Statuses;

namespace Persistence.Repositories
{
    public class HelperRepository : IHelper
    {
        private readonly IConfiguration _configuration;
        public HelperRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        const string KEY = "546C8DF2";

        public string CurrencyFormat(decimal amount)
        {
            string html = string.Format("{0:C}", amount);
            if (html.Contains("Rs"))
            {
                return html.Substring(2);
            }
            return html.Substring(1);
        }

        public string DecryptFromBase64String(string stringToDecrypt)
        {
            byte[] inputByteArray = new byte[stringToDecrypt.Length];
            byte[] byKey = [];
            byte[] IV = [18, 52, 86, 120, 144, 171, 205, 239];
            byKey = Encoding.UTF8.GetBytes(KEY);
            DES des = DES.Create();
            inputByteArray = Convert.FromBase64String(stringToDecrypt);
            MemoryStream ms = new();
            CryptoStream cs = new(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            Encoding encoding = Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }

        public string Encryptor(string strText)
        {
            byte[] byKey = [];
            byte[] IV = [18, 52, 86, 120, 144, 171, 205, 239];
            byKey = Encoding.UTF8.GetBytes(KEY);
            DES des = DES.Create();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
            MemoryStream ms = new();
            CryptoStream cs = new(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

        public string FormatString<T>(string text, T values)
        {
            throw new NotImplementedException();
        }

        public string GetRandomPassword(int length)
        {
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0",
                                            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "R", "U", "V", "W", "X", "Y", "Z",
                                            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            string sOTP = String.Empty;
            Random rand = new Random();
            for (int i = 0; i < length; i++)
            {
                _ = rand.Next(0, saAllowedCharacters.Length);
                string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                sOTP += sTempChars;
            }
            return sOTP;
        }

        public Task<bool> SendEmail(string toEmail, string body, string subject, string from, string mailTitle)
        {
            try
            {

                System.Net.NetworkCredential _Credential = new System.Net.NetworkCredential(SmptSetting.smtpUser, SmptSetting.smptPassword);
                MailMessage mail = new()
                {
                    BodyEncoding = Encoding.UTF8,
                    SubjectEncoding = Encoding.UTF8
                };
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.From = new MailAddress(from == string.Empty ? SmptSetting.noReplyEmail : from, mailTitle);
                mail.IsBodyHtml = true;
                string htmlresult = body;
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlresult, null, System.Net.Mime.MediaTypeNames.Text.Html);
                mail.AlternateViews.Add(htmlView);
                SmtpClient _SmtpClient = new(SmptSetting.smtpHost)
                {
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    Credentials = _Credential,
                    Port = int.Parse(SmptSetting.smtpPort)
                };
                _SmtpClient.Send(mail);
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Helper->SendEmail");
                return Task.FromResult(false);
            }
        }

        public string GetTemplate(TemplatesName templateName)
        {

            return templateName switch
            {
                TemplatesName.PASSWORD_RESET => File.ReadAllText($"{AppSetting.TemplatePath}\\password-reset.html"),
                TemplatesName.PASSWORD_CHANGED => File.ReadAllText($"{AppSetting.TemplatePath}\\password-changed.html"),
                _ => string.Empty,
            };
        }

        public string GetResponseMessage(string key)
        {
            return _configuration[$"Messages:{key}"];
        }

        public string AddInAttachmentStore(IFormFile Attachment, DirectoryDetails directoryDetail)
        {
            try
            {
                if (Attachment != null && !string.IsNullOrEmpty(Attachment.FileName))
                {
                    string fExtension = Path.GetExtension(Attachment.FileName);
                    string fName = Attachment.FileName;
                    var directoryPath = Path.Combine(AppSetting.DocumentPath, directoryDetail.Path);

                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    using FileStream fs = File.Create($"{directoryPath}\\{fName}");
                    Attachment.CopyTo(fs);

                    var path = $"{directoryPath}\\{fName}".Split("Assets\\");
                    return path[1];
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public string AddInAttachmentStore(IFormFileCollection Attachments, DirectoryDetails directoryDetail)
        {
            try
            {
                if (Attachments != null && Attachments.Count > 0)
                {
                    var directoryPath = Path.Combine(AppSetting.DocumentPath, directoryDetail.Path);
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    foreach (var Attachment in Attachments)
                    {
                        string fExtension = Path.GetExtension(Attachment.FileName);
                        string fName = Attachment.FileName;

                        using FileStream fs = File.Create($"{directoryPath}\\{fName}");
                        Attachment.CopyTo(fs);
                    }

                    var path = $"{directoryPath}".Split("Assets\\");
                    return path[1];
                }
                else
                    return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public bool DeleteAllAttachments(DirectoryDetails directoryDetails)
        {
            try
            {
                if (directoryDetails is not null && !string.IsNullOrEmpty(directoryDetails.Path))
                {
                    var directoryPath = Path.Combine(AppSetting.DocumentPath, directoryDetails.Path);
                    DirectoryInfo directory = new(directoryPath);
                    if (directory.Exists)
                        directory.Delete(true);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public static class StringExtensions
    {
        public static string Replace<T>(this string text, T values)
        {

            //how to use 
            //  var parameter1 = DateTime.Now.ToString();
            //var parameter2 = "Hello world!";
            //var retrievedString = "{{parameter2}} Today we're {parameter1}";
            //var r = retrievedString.Replace("{{parameter2}}", parameter2);
            //var result = retrievedString.Replace(new { parameter1, parameter2 });
            var sb = new StringBuilder(text);
            var properties = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToArray();

            var args = properties
                .Select(p => p.GetValue(values, null))
                .ToArray();

            for (var i = 0; i < properties.Length; i++)
            {
                var oldValue = string.Format("{{{0}", properties[i].Name);
                var newValue = string.Format("{{{0}", i);

                sb.Replace(oldValue, newValue);
            }

            var format = sb.ToString();

            return string.Format(format, args);
        }
    }
}
