using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;

namespace yooja
{
    /*
     * Microsoft.VisualBasic 참조 필요
     * - 버전 :10.0.0.0
     * - 경로: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5.2\Microsoft.VisualBasic.dll
     * - 런타임 버전:  v4.0.30319
     */

    internal class yooja_File_Control
    {
        /// <summary>
        /// 파일 존재 확인
        /// </summary>
        /// <param name="path">확일 할 파일</param>
        /// <returns>존재 여부</returns>
        public bool File_Exists(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// 폴더 존재 확인
        /// </summary>
        /// <param name="path">확인 할 폴더</param>
        /// <returns>존재여부</returns>
        public bool Folder_Exists(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// 파일 이름이 중복을 방지하기 위해 사용함 <br/>
        /// 이름이 중복되면 파일 이름 뒤에 숫자 ex (1) 을 붙임
        /// </summary>
        /// <param name="file_name">중복 검사를 할 파일이름</param>
        /// <returns>중복 검사를 끝내고 수정된 파일이름</returns>
        public string  File_OverLap(string file_name)
        {
            int sameNameFiles_count = 0;
            string return_FileName = file_name;
            FileInfo rf =new FileInfo(return_FileName);
            
            
            while (true)
            {
                if (rf.Exists)
                {
                    sameNameFiles_count++;
                    rf = new FileInfo(rf.DirectoryName +@"\"+ 
                        Path.GetFileNameWithoutExtension(file_name) + "(" + sameNameFiles_count + ")" + rf.Extension);
                    
                }
                else
                {
                    break;
                }
            }
            return rf.FullName;
        }

        /// <summary>
        /// folder 경로에 쓰기 권한이 있는지 확인
        /// </summary>
        /// <param name="folder">확인할 폴더의 경로</param>
        /// <returns>쓰기 권한 존재</returns>
        public bool Folder_Write_Enable(string folder)
        {
            try
            {
                Directory.CreateDirectory(folder + @"\temp");
                Directory.Delete(folder + @"\temp");
                return true;
            }
            catch(UnauthorizedAccessException e)
            {
                return false;
            }
            catch(IOException e)
            {
                return false;
            }
        }

        /// <summary>
        /// 폴더안에 선택한 확장자 파일의 경로들 반환
        /// </summary>
        /// <param name="Folder">찾아볼 폴더</param>
        /// <param name="extension">찾아볼 확장자</param>
        /// <param name="searchOption">
        /// 검색 옵션<br/>
        /// SearchOption.AllDirectories : 하위 폴더도 검색<br/>
        /// SearchOption.TopDirectoryOnly: 현재 폴더만 검색</param>
        /// <returns>확장자가 일치하는 파일의 경로 배열</returns>
        public string[] Extension_Files(string Folder,string extension,System.IO. SearchOption searchOption)
        {
            IEnumerable<string> all_extensions =
                Directory.EnumerateFiles(Folder, "*.*", searchOption)
                .Where(file => new string[] { "." + extension }
                    .Contains(Path.GetExtension(file)));

            

            return all_extensions.ToArray();
        }

        /// <summary>
        /// 폴더 복사
        /// </summary>
        /// <param name="source">원본</param>
        /// <param name="target">사본</param>
        /// <param name="uioption">
        /// 사용자에게 진행상황을 보여줄지<br/>
        /// AllDialogs: 항상 보여줘<br/>
        /// OnlyErrorDialogs: 에러 있을때만 
        /// </param>
        public void Copy_Folder(string source, string target,UIOption uioption)
        {
            if (!Folder_Exists(target))
            {
                Directory.CreateDirectory(target);
            }
            FileSystem.CopyDirectory(source, target,uioption, UICancelOption.DoNothing);

        }

        /// <summary>
        /// 파일복사
        /// </summary>
        /// <param name="source">원본</param>
        /// <param name="target">사본</param>
        /// <param name="uioption">
        /// 사용자에게 진행상황을 보여줄지<br/>
        /// AllDialogs: 항상 보여줘<br/>
        /// OnlyErrorDialogs: 에러 있을때만 </param>
        public void Copy_File(string source,string target,UIOption uioption)
        {
            FileSystem.CopyFile(source, target, uioption);
        }
    }
}
