using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UpLoadZip.Web.Models;

namespace UpLoadZip.Web.Services
{
    public class ZipServices
    {
        public async Task<List<ZipStructModel>> UpLoadZip(HttpFileCollection PostData)
        {
            List<ZipStructModel> result = new List<ZipStructModel>();
            await Task.Run(() =>
            {
                if (PostData.Count > 0)
                {
                    foreach (string file in PostData)
                    {
                        var postedFile = PostData[file];
                        if (postedFile.FileName.Contains(".zip"))
                        {
                            string pathTemp = Path.GetTempPath();
                            postedFile.SaveAs(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, postedFile.FileName));
                            var path = Path.Combine(pathTemp, postedFile.FileName);
                            postedFile.SaveAs(path);
                            result.Add(GetStruct(path));
                            DeleteFile(path);
                        }
                    }
                }

            });

            return result;
        }

        /*методы GetStruct и DeleteFile - не делал асинхронными, чтобы соблюдалась последовательность действий*/

        public ZipStructModel GetStruct(string path)
        {
            ZipStructModel model = new ZipStructModel();
            using (var fileStream = new FileStream(Path.Combine(path), FileMode.Open))
            {
                model.FileName = fileStream.Name;
                model.Acrchive = new Dictionary<string, int>();
                using (ZipArchive zip = new ZipArchive(fileStream))
                {
                    var foldersCount = zip.Entries.Where(x => x.FullName.Split('/').Length > 1 || x.FullName.EndsWith("/"))
                                                  .Select(f => f.FullName.Split('/')[0]).Distinct();


                    foreach (var folder in foldersCount.OrderBy(x => x))
                    {
                        var listFiles = zip.Entries.Where(e => e.FullName.Contains(folder)).ToList();
                        var cnt = listFiles.Select(a => a.FullName.Substring(0, a.FullName.LastIndexOf('/'))).Distinct().ToList().Count();

                        if (cnt > 0)    /*Каждая папка должна иметь хотя бы одну подпапку*/
                            model.Acrchive.Add(folder, cnt);
                        else
                            throw new Exception("Не корректная структура архива");
                    }

                }
                return model;
            }
        }

        public void DeleteFile(string Path)
        {
            System.IO.File.Delete(Path);
        }
    }
}