using System.IO;
using System.Text;
using ClientManagementAPI.Models;

namespace ClientManagementAPI.Services
{
    public class IniFileService
    {
        private readonly string _filePath;

        public IniFileService(string filePath)
        {
            _filePath = filePath;
        }

        public void SaveToIniFile(ClientData clientData)
        {
            // INI 파일이 없으면 새로 생성
            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Dispose();
            }

            var iniContent = new StringBuilder();

            // 섹션 추가 (ClientType)
            iniContent.AppendLine($"[{clientData.ClientType}]");

            // 키-값 쌍 추가
            iniContent.AppendLine($"IpAddress={clientData.IpAddress}");
            iniContent.AppendLine($"Description={clientData.Description}");
            iniContent.AppendLine($"CreateAt={clientData.CreateAt}");

            // 기존 파일 내용 읽기
            var existingContent = File.ReadAllText(_filePath);

            // 동일 섹션이 존재하면 덮어쓰기, 없으면 추가
            if (existingContent.Contains($"[{clientData.ClientType}]"))
            {
                var sectionStartIndex = existingContent.IndexOf($"[{clientData.ClientType}]");
                var sectionEndIndex = existingContent.IndexOf('[', sectionStartIndex + 1);

                if (sectionEndIndex == -1) sectionEndIndex = existingContent.Length;

                var oldSection = existingContent.Substring(sectionStartIndex, sectionEndIndex - sectionStartIndex);
                existingContent = existingContent.Replace(oldSection, iniContent.ToString());
            }
            else
            {
                existingContent += iniContent.ToString();
            }

            // 변경된 내용을 INI 파일에 저장
            File.WriteAllText(_filePath, existingContent);
        }
    }
}